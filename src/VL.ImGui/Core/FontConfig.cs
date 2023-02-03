using System.Drawing;
using VL.Lib.Text;

namespace VL.ImGui
{
    /// <summary>
    /// Defines a font configuration.
    /// </summary>
    /// <param name="FamilyName">The font family name.</param>
    /// <param name="FontStyle">The font style.</param>
    /// <param name="Size">The size of the font in device independent hecto pixel (1 = 100 DIP).</param>
    /// <param name="Name">An optional name to use for this configuration.</param>
    public record FontConfig(FontList FamilyName, FontStyle FontStyle = FontStyle.Regular, float Size = 0.16f, string Name = "")
    {
        public static readonly FontConfig? Default;

        static FontConfig()
        {
            if (OperatingSystem.IsWindows())
            {
                using var defaultTypeFace = SystemFonts.MessageBoxFont ?? SystemFonts.DefaultFont;
                Default = new FontConfig(new FontList(defaultTypeFace.FontFamily.Name));
            }
        }

        public override string ToString() => !string.IsNullOrEmpty(Name) ? Name : $"{FamilyName} {FontStyle} {Size}";
    }
}
