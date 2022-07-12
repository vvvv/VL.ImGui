namespace VL.ImGui.Widgets
{
    [GenerateNode(Category = "ImGui.Widgets")]
    internal sealed partial class Menu : Widget
    {

        public IEnumerable<Widget> Items { get; set; } = Enumerable.Empty<Widget>();

        public string? Label { get; set; }

        public bool Enabled { get; set; } = true;

        internal override void Update(Context context)
        {
            var count = Items.Count(x => x != null);

            if (count > 0)
            {
                if (ImGuiNET.ImGui.BeginMenu(Label ?? string.Empty, Enabled))
                {
                    try
                    {
                        foreach (var item in Items)
                        {
                            if (item is null)
                                continue;
                            else
                                context.Update(item);

                        }
                    }
                    finally
                    {
                        ImGuiNET.ImGui.EndMenu();
                    }
                    
                }
            }
        }
    }
}
