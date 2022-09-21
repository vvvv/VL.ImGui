﻿namespace VL.ImGui.Widgets
{
    [GenerateNode(Name = "Text (Bullet)", Category = "ImGui.Widgets")]
    internal partial class TextBullet : Widget
    {
        public string? Text { private get; set; } = "[Text]";

        internal override void UpdateCore(Context context)
        {
            ImGuiNET.ImGui.BulletText(Text ?? String.Empty);
        }
    }
}