using System;
using VL.Lib.IO.Notifications;
using VL.Skia;
using ImGuiNET;
using System.Numerics;
using SkiaSharp;
using System.Runtime.InteropServices;
using Stride.Core.Mathematics;

using MouseButtons = VL.Lib.IO.MouseButtons;
using Keys = VL.Lib.IO.Keys;

namespace VL.ImGui
{
    using ImGui = ImGuiNET.ImGui;
    using Vector2 = System.Numerics.Vector2;

    internal delegate void WidgetFunc(CallerInfo canvas, SKRect clipBounds);

    public class ToSkiaLayer : IDisposable, ILayer
    {
        readonly struct Handle<T> : IDisposable
            where T : class
        {
            private readonly GCHandle _handle;

            public Handle(T skiaObject)
            {
                _handle = GCHandle.Alloc(skiaObject);
            }

            public T? Target => _handle.Target as T;

            public IntPtr Ptr => GCHandle.ToIntPtr(_handle);

            public void Dispose()
            {
                _handle.Free();
            }
        }

        readonly ImGuiIOPtr _io;

        Widget? _widget;
        private IEnumerable<Widget> MenuBar = Enumerable.Empty<Widget>();
        private bool DockingEnabled = false;
        private bool DefaultWindow = false;

        // OpenGLES rendering (https://github.com/dotnet/Silk.NET/tree/v2.15.0/src/OpenGL/Extensions/Silk.NET.OpenGL.Extensions.ImGui)
        private readonly SkiaContext _context;
        private readonly RenderContext _renderContext;
        private readonly Handle<SKPaint> _fontPaint;

        public ToSkiaLayer()
        {
            _context = new SkiaContext();
            using (_context.MakeCurrent())
            {
                _io = ImGui.GetIO();
            }

            _renderContext = RenderContext.ForCurrentThread();

            var scaling = VL.UI.Core.DIPHelpers.DIPFactor();
            _fontPaint = new Handle<SKPaint>(new SKPaint());
            BuildImFontAtlas(_io.Fonts, _fontPaint, scaling);
            UpdateUIScaling(scaling);
            
        }

        public ILayer Update(Widget widget, IEnumerable<Widget> menuBar, bool dockingEnabled, bool defaultWindow)
        {
            MenuBar = menuBar;
            DockingEnabled = dockingEnabled;
            DefaultWindow = defaultWindow;
            _widget = widget;
            return this;
        }

        public unsafe void Render(CallerInfo caller)
        {
            using (_context.MakeCurrent())
            {
                var bounds = caller.ViewportBounds;
                _io.DisplaySize = new Vector2(bounds.Width, bounds.Height);

                // Enable Docking
                if (DockingEnabled)
                    _io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;

                _context.NewFrame();
                try
                {
                    if (DefaultWindow)
                    {
                        var viewPort = ImGui.GetMainViewport();
                        ImGui.SetNextWindowPos(viewPort.WorkPos);
                        ImGui.SetNextWindowSize(viewPort.WorkSize);

                        ImGui.Begin("", ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoBringToFrontOnFocus | ImGuiWindowFlags.NoNavFocus | ImGuiWindowFlags.NoFocusOnAppearing);
                    }

                    // Add Menu Bar
                    var menuBarCount = MenuBar.Count(x => x != null);

                    if (menuBarCount > 0)
                    {
                        if (ImGui.BeginMainMenuBar())
                        {
                            try
                            {
                                foreach (var item in MenuBar)
                                {
                                    if (item is null)
                                        continue;
                                    else
                                        _context.Update(item);
                                }
                            }
                            finally
                            {
                                ImGui.EndMainMenuBar();
                            }
                        }

                    }
                    // ImGui.ShowDemoWindow();
                    _context.Update(_widget);
                }
                finally
                {
                    if (DefaultWindow)
                    {
                        ImGui.End();
                    }

                    // Render (builds mesh with texture coordinates)
                    ImGui.Render();
                }

                // Render the mesh
                var drawDataPtr = ImGui.GetDrawData();
                Render(caller, drawDataPtr);
            }
        }

        static void BuildImFontAtlas(ImFontAtlasPtr atlas, Handle<SKPaint> paintHandle, float scaling = 1f)
        {
            //atlas.AddFontDefault();

            var fontsfolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            using var defaultTypeFace = SKTypeface.CreateDefault();
            var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                defaultTypeFace.FamilyName.Replace(" ", ""),
                //"arial",
                //"tahoma"
            };
            foreach (var fontPath in Directory.GetFiles(fontsfolder, "*.ttf").Where(p => names.Contains(Path.GetFileNameWithoutExtension(p))))
            {
                ImFontConfig cfg = new ImFontConfig()
                {
                    SizePixels = 16f * scaling,
                    FontDataOwnedByAtlas = 1,
                    EllipsisChar = 0x0085,
                    OversampleH = 1,
                    OversampleV = 1,
                    PixelSnapH = 1,
                    GlyphOffset = new Vector2(0, 0),
                    GlyphMaxAdvanceX = float.MaxValue,
                    RasterizerMultiply = 1.0f
                };
                unsafe
                {
                    atlas.AddFontFromFileTTF(fontPath, cfg.SizePixels, &cfg);
                }
            }

            atlas.GetTexDataAsAlpha8(out IntPtr pixels, out var w, out var h);
            var info = new SKImageInfo(w, h, SKColorType.Alpha8);
            var pmap = new SKPixmap(info, pixels, info.RowBytes);
            var localMatrix = SKMatrix.CreateScale(1.0f / w, 1.0f / h);
            var fontImage = SKImage.FromPixels(pmap);
            // makeShader(SkSamplingOptions(SkFilterMode::kLinear), localMatrix);
            var fontShader = fontImage.ToShader(SKShaderTileMode.Clamp, SKShaderTileMode.Clamp, localMatrix);
            var paint = paintHandle.Target;
            if (paint != null)
            {
                paint.Shader = fontShader;
                paint.Color = SKColors.White;
                atlas.TexID = paintHandle.Ptr;
            }
        }

        static unsafe void UpdateUIScaling(float scaling = 1f)
        {
            var style = ImGui.GetStyle();
            style.ScaleAllSizes(scaling);

            // From https://github.com/ocornut/imgui/discussions/3925
            //ImGuiStyle* p = style;
            //ImGuiStyle styleold = *p; // Backup colors
            //var style = new ImGuiStyle(); // IMPORTANT: ScaleAllSizes will change the original size, so we should reset all style config
            //style.WindowBorderSize = 1.0f;
            //style.ChildBorderSize = 1.0f;
            //style.PopupBorderSize = 1.0f;
            //style.FrameBorderSize = 1.0f;
            //style.TabBorderSize = 1.0f;
            //style.WindowRounding = 0.0f;
            //style.ChildRounding = 0.0f;
            //style.PopupRounding = 0.0f;
            //style.FrameRounding = 0.0f;
            //style.ScrollbarRounding = 0.0f;
            //style.GrabRounding = 0.0f;
            //style.TabRounding = 0.0f;
            //style.ScaleAllSizes(scale);
        }

        // From https://github.com/google/skia/blob/main/tools/viewer/ImGuiLayer.cpp
        // TODO: With net6 we can probably get rid of a lot of allocations in here
        private void Render(CallerInfo caller, ImDrawDataPtr drawData)
        {
            var canvas = caller.Canvas;
            var callerInfo = caller.WithTransformation(SKMatrix.Identity);

            for (int i = 0; i < drawData.CmdListsCount; ++i)
            {
                var drawList = drawData.CmdListsRange[i];

                // De-interleave all vertex data (sigh), convert to Skia types
                //pos.Clear(); uv.Clear(); color.Clear();
                var size = drawList.VtxBuffer.Size;
                var pos = new SKPoint[size];
                var uv = new SKPoint[size];
                var color = new SKColor[size];
                for (int j = 0; j < size; ++j)
                {
                    var vert = drawList.VtxBuffer[j];
                    pos[j] = new SKPoint(vert.pos.X, vert.pos.Y);
                    uv[j] = new SKPoint(vert.uv.X, vert.uv.Y);
                    color[j] = vert.col;
                }

                // ImGui colors are RGBA
                SKSwizzle.SwapRedBlue(MemoryMarshal.AsBytes(color.AsSpan()), MemoryMarshal.AsBytes(color.AsSpan()), color.Length);


                // Draw everything with canvas.drawVertices...
                for (int j = 0; j < drawList.CmdBuffer.Size; ++j)
                {
                    var drawCmd = drawList.CmdBuffer[j];
                    var indexOffset = (int)drawCmd.IdxOffset;
                    var clipRect = new SKRect(drawCmd.ClipRect.X, drawCmd.ClipRect.Y, drawCmd.ClipRect.Z, drawCmd.ClipRect.W);

                    //using var _ = new SKAutoCanvasRestore(canvas, true);
                    canvas.Save();
                    canvas.ResetMatrix();
                    canvas.ClipRect(clipRect);

                    // TODO: Find min/max index for each draw, so we know how many vertices (sigh)
                    if (drawCmd.UserCallback != IntPtr.Zero)
                    {
                        var handle = GCHandle.FromIntPtr(drawCmd.UserCallback);
                        try
                        {
                            if (handle.Target is DrawCallback callback)
                                callback(drawList, drawCmd);
                        }
                        finally
                        {
                            handle.Free();
                        }
                    }
                    else
                    {
                        var idIndex = drawCmd.TextureId.ToInt64();
                        if (idIndex < _context.WidgetFuncs.Count)
                        {
                            // Small image IDs are actually indices into a list of callbacks. We directly
                            // examing the vertex data to deduce the image rectangle, then reconfigure the
                            // canvas to be clipped and translated so that the callback code gets to use
                            // Skia to render a widget in the middle of an ImGui panel.
                            var rectIndex = drawList.IdxBuffer[indexOffset];
                            var tl = pos[rectIndex];
                            var br = pos[rectIndex + 2];
                            var imageClipRect = new SKRect(tl.X, tl.Y, br.X, br.Y);
                            canvas.ClipRect(imageClipRect);
                            canvas.Translate(imageClipRect.Location);

                            _context.WidgetFuncs[(int)idIndex](callerInfo, imageClipRect);
                        }
                        else
                        {
                            var handle = GCHandle.FromIntPtr(drawCmd.TextureId);
                            var paint = handle.Target as SKPaint ?? _fontPaint.Target;

                            var indices = new ushort[drawCmd.ElemCount];
                            for (int k = 0; k < indices.Length; k++)
                                indices[k] = drawList.IdxBuffer[indexOffset + k];

                            canvas.DrawVertices(SKVertexMode.Triangles, pos, uv, color, SKBlendMode.Modulate, indices, paint);
                        }
                    }

                    canvas.Restore();
                }
            }
        }

        public bool Notify(INotification notification, CallerInfo caller)
        {
            using (_context.MakeCurrent())
            {
                if (notification is NotificationBase n)
                {
                    _io.KeyAlt = n.AltKey;
                    _io.KeyCtrl = n.CtrlKey;
                    _io.KeyShift = n.ShiftKey;
                }

                if (notification is KeyNotification keyNotification)
                {
                    if (keyNotification is KeyCodeNotification keyCodeNotification)
                    {
                        _io.AddKeyEvent(ToImGuiKey(keyCodeNotification.KeyCode), keyCodeNotification.IsKeyDown);
                    }
                    else if (keyNotification is KeyPressNotification keyPressNotification)
                    {
                        _io.AddInputCharacter(keyPressNotification.KeyChar);
                    }
                }
                else if (notification is MouseNotification mouseNotification)
                {
                    var button = 0;
                    if (mouseNotification is MouseButtonNotification b)
                    {
                        button = b.Buttons switch
                        {
                            MouseButtons.Left => 0,
                            MouseButtons.Right => 1,
                            MouseButtons.Middle => 2,
                            MouseButtons.XButton1 => 3,
                            MouseButtons.XButton2 => 4,
                            _ => 0
                        };
                    }

                    switch (mouseNotification.Kind)
                    {
                        case MouseNotificationKind.MouseDown:
                            _io.AddMouseButtonEvent(button, true);
                            break;
                        case MouseNotificationKind.MouseUp:
                            _io.AddMouseButtonEvent(button, false);
                            break;
                        case MouseNotificationKind.MouseMove:
                            _io.AddMousePosEvent(mouseNotification.Position.X, mouseNotification.Position.Y);
                            break;
                        case MouseNotificationKind.MouseWheel:
                            if (mouseNotification is MouseWheelNotification wheel)
                                _io.AddMouseWheelEvent(0, wheel.WheelDelta / 120);
                            break;
                        case MouseNotificationKind.MouseHorizontalWheel:
                            if (mouseNotification is MouseHorizontalWheelNotification hWheel)
                                _io.AddMouseWheelEvent(hWheel.WheelDelta / 120, 0);
                            break;
                        case MouseNotificationKind.DeviceLost:
                            _io.ClearInputCharacters();
                            _io.ClearInputKeys();
                            break;
                        default:
                            break;
                    }
                }
                else if (notification is TouchNotification touchNotification)
                {
                    // TODO
                }
                return false;
            }
        }

        static ImGuiKey ToImGuiKey(Keys key)
        {
            switch (key)
            {
                case Keys.Back: return ImGuiKey.Backspace;
                case Keys.Tab: return ImGuiKey.Tab;
                case Keys.Enter: return ImGuiKey.Enter;
                case Keys.ShiftKey: return ImGuiKey.LeftShift;
                case Keys.ControlKey:  return ImGuiKey.LeftCtrl;
                case Keys.Menu:  return ImGuiKey.Menu;
                case Keys.Pause:  return ImGuiKey.Pause;
                case Keys.CapsLock: return ImGuiKey.CapsLock;
                case Keys.Escape: return ImGuiKey.Escape;
                case Keys.Space: return ImGuiKey.Space;
                case Keys.PageUp: return ImGuiKey.PageUp;
                case Keys.PageDown: return ImGuiKey.PageDown;
                case Keys.End: return ImGuiKey.End;
                case Keys.Home: return ImGuiKey.Home;
                case Keys.Left: return ImGuiKey.LeftArrow;
                case Keys.Up:  return ImGuiKey.UpArrow;
                case Keys.Right: return ImGuiKey.RightArrow;
                case Keys.Down: return ImGuiKey.DownArrow;
                case Keys.Snapshot: return ImGuiKey.PrintScreen;
                case Keys.Insert: return ImGuiKey.Insert;
                case Keys.Delete: return ImGuiKey.Delete;
                case Keys.Oem7: return ImGuiKey.Apostrophe;
                case Keys.Oemcomma: return ImGuiKey.Comma;
                case Keys.OemMinus: return ImGuiKey.Minus;
                case Keys.OemPeriod: return ImGuiKey.Period;
                case Keys.Oem2: return ImGuiKey.Slash;
                case Keys.Oem1: return ImGuiKey.Semicolon;
                case Keys.Oemplus: return ImGuiKey.Equal;
                case Keys.Oem4: return ImGuiKey.LeftBracket;
                case Keys.Oem5: return ImGuiKey.Backslash;
                case Keys.Oem6: return ImGuiKey.RightBracket;
                case Keys.Oem3: return ImGuiKey.GraveAccent;
                case Keys.D0: return ImGuiKey._0;
                case Keys.D1: return ImGuiKey._1;
                case Keys.D2: return ImGuiKey._2;
                case Keys.D3: return ImGuiKey._3;
                case Keys.D4: return ImGuiKey._4;
                case Keys.D5: return ImGuiKey._5;
                case Keys.D6: return ImGuiKey._6;
                case Keys.D7: return ImGuiKey._7;
                case Keys.D8: return ImGuiKey._8;
                case Keys.D9: return ImGuiKey._9;
                case Keys.A: return ImGuiKey.A;
                case Keys.B: return ImGuiKey.B;
                case Keys.C: return ImGuiKey.C;
                case Keys.D: return ImGuiKey.D;
                case Keys.E: return ImGuiKey.E;
                case Keys.F: return ImGuiKey.F;
                case Keys.G: return ImGuiKey.G;
                case Keys.H: return ImGuiKey.H;
                case Keys.I: return ImGuiKey.I;
                case Keys.J: return ImGuiKey.J;
                case Keys.K: return ImGuiKey.K;
                case Keys.L: return ImGuiKey.L;
                case Keys.M: return ImGuiKey.M;
                case Keys.N: return ImGuiKey.N;
                case Keys.O: return ImGuiKey.O;
                case Keys.P: return ImGuiKey.P;
                case Keys.Q: return ImGuiKey.Q;
                case Keys.R: return ImGuiKey.R;
                case Keys.S: return ImGuiKey.S;
                case Keys.T: return ImGuiKey.T;
                case Keys.U: return ImGuiKey.U;
                case Keys.V: return ImGuiKey.V;
                case Keys.W: return ImGuiKey.W;
                case Keys.X: return ImGuiKey.X;
                case Keys.Y: return ImGuiKey.Y;
                case Keys.Z: return ImGuiKey.Z;
                case Keys.NumPad0: return ImGuiKey.Keypad0;
                case Keys.NumPad1: return ImGuiKey.Keypad1;
                case Keys.NumPad2: return ImGuiKey.Keypad2;
                case Keys.NumPad3: return ImGuiKey.Keypad3;
                case Keys.NumPad4: return ImGuiKey.Keypad4;
                case Keys.NumPad5: return ImGuiKey.Keypad5;
                case Keys.NumPad6: return ImGuiKey.Keypad6;
                case Keys.NumPad7: return ImGuiKey.Keypad7;
                case Keys.NumPad8: return ImGuiKey.Keypad8;
                case Keys.NumPad9: return ImGuiKey.Keypad9;
                case Keys.Multiply: return ImGuiKey.KeypadMultiply;
                case Keys.Add: return ImGuiKey.KeypadAdd;
                case Keys.Subtract: return ImGuiKey.KeypadSubtract;
                case Keys.Decimal: return ImGuiKey.KeypadDecimal;
                case Keys.Divide: return ImGuiKey.KeypadDivide;
                case Keys.F1: return ImGuiKey.F1;
                case Keys.F2: return ImGuiKey.F2;
                case Keys.F3: return ImGuiKey.F3;
                case Keys.F4: return ImGuiKey.F4;
                case Keys.F5: return ImGuiKey.F5;
                case Keys.F6: return ImGuiKey.F6;
                case Keys.F7: return ImGuiKey.F7;
                case Keys.F8: return ImGuiKey.F8;
                case Keys.F9: return ImGuiKey.F9;
                case Keys.F10: return ImGuiKey.F10;
                case Keys.F11: return ImGuiKey.F11;
                case Keys.F12: return ImGuiKey.F12;
                case Keys.NumLock: return ImGuiKey.NumLock;
                case Keys.Scroll: return ImGuiKey.ScrollLock;
                case Keys.LShiftKey: return ImGuiKey.LeftShift;
                case Keys.RShiftKey: return ImGuiKey.RightShift;
                case Keys.LControlKey: return ImGuiKey.LeftCtrl;
                case Keys.RControlKey: return ImGuiKey.RightCtrl;
                case Keys.LMenu: return ImGuiKey.LeftAlt;
                case Keys.RMenu: return ImGuiKey.RightAlt;
                case Keys.LWin: return ImGuiKey.LeftSuper;
                case Keys.RWin: return ImGuiKey.RightSuper;
                default:
                    return ImGuiKey.None;
            }
        }

        public RectangleF? Bounds => default;

        public void Dispose()
        {
            _renderContext.Dispose();

            _context.Dispose();
        }
    }
}
