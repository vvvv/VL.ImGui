using System.Reactive.Disposables;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.ImGui.Editors
{
    using ImGui = ImGuiNET.ImGui;

    abstract class ListEditorBase<TList, T> : IObjectEditor, IDisposable
                where TList : IReadOnlyList<T>
    {
        private readonly List<IObjectEditor?> editors = new List<IObjectEditor?>();
        private readonly CompositeDisposable subscriptions = new CompositeDisposable();
        private readonly Channel<TList> channel;
        private readonly ObjectEditorContext editorContext;
        private readonly string label;

        public ListEditorBase(Channel<TList> channel, ObjectEditorContext editorContext)
        {
            this.channel = channel;
            this.editorContext = editorContext;
            this.label = $"##{GetHashCode()}";
        }

        public void Dispose()
        {
            subscriptions.Dispose();
        }

        public void Draw(Context? context)
        {
            if (channel.Value.Count == 0)
                return;

            if (ImGui.BeginListBox(label))
            {
                var list = channel.Value;
                for (int i = 0; i < list.Count; i++)
                {
                    var editor = editors.ElementAtOrDefault(i);
                    if (i >= editors.Count)
                    {
                        // Setup channel for item
                        var itemChannel = new Channel<T>();
                        var j = i;
                        subscriptions.Add(channel.BindTwoWay(itemChannel, c => c[j], item => SetItem(channel.Value, j, item)));

                        editor = editorContext.Factory.CreateObjectEditor(itemChannel, editorContext);
                        editors.Add(editor);
                    }

                    ImGui.PushID(i);
                    try
                    {
                        editor?.Draw(context);
                    }
                    finally
                    {
                        ImGui.PopID();
                    }
                }

                ImGui.EndListBox();
            }
        }

        protected abstract TList SetItem(TList list, int i, T item);
    }
}
