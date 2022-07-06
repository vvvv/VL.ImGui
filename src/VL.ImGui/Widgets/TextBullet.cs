using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using VL.Core;
using VL.ImGui;
using Stride.Core.Mathematics;

namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Text (Bullet)", Category = "ImGui.Widgets")]
    internal partial class TextBullet : Widget
    {
        public string? Text { private get; set; }

        internal override void Update(Context context)
        {
            ImGuiNET.ImGui.BulletText(Text ?? String.Empty);
        }
    }
}
