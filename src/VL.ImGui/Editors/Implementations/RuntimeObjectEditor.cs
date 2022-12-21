using System.Reactive.Disposables;
using VL.Core;
using VL.Lib.Reactive;

namespace VL.ImGui.Editors
{
    using ImGui = ImGuiNET.ImGui;

    sealed class RuntimeObjectEditor : IObjectEditor, IDisposable
    {
        readonly Channel<object> channel;
        readonly IObjectEditorFactory factory;

        readonly SerialDisposable editorOwnership = new SerialDisposable();
        readonly SerialDisposable channelSubscription = new SerialDisposable();
        readonly ObjectEditorContext editorContext;

        Channel? implementingChannel;
        IObjectEditor? currentEditor;

        public RuntimeObjectEditor(Channel<object> channel, ObjectEditorContext editorContext)
        {
            this.channel = channel;
            this.editorContext = editorContext;
            this.factory = editorContext.Factory;
            SelectCurrentEditor(channel.Value);
        }

        public void Dispose()
        {
            channelSubscription.Dispose();
            editorOwnership.Dispose();
        }

        public bool NeedsMoreThanOneLine => currentEditor != null ? currentEditor.NeedsMoreThanOneLine : false;

        void SelectCurrentEditor(object? value)
        {
            if (value is null)
            {
                editorOwnership.Disposable = null;
                currentEditor = null;
                return;
            }

            var typeInfo = TypeRegistry.Default.GetTypeInfo(value.GetType());
            if (typeInfo.ClrType == typeof(object)) 
            {
                implementingChannel = null;
                channelSubscription.Disposable = null;
                currentEditor = null;
                editorOwnership.Disposable = null;
            }
            else if (typeInfo.ClrType != implementingChannel?.ClrTypeOfValues)
            {
                // Build new channel and bind to public channel
                implementingChannel = Channel.CreateChannelOfType(typeInfo);
                channelSubscription.Disposable = channel.MergeSafe(implementingChannel.ChannelOfObject);

                // Select new editor
                currentEditor = factory.CreateObjectEditor(implementingChannel, editorContext);
                editorOwnership.Disposable = currentEditor as IDisposable;
            }
        }

        public void Draw(Context? context)
        {
            context = context.Validate();
            if (context is null)
                return;

            SelectCurrentEditor(channel.Value);

            if (currentEditor != null)
            {
                currentEditor.Draw(context);
            }
            else if (channel.Value is not null)
            {
                // TODO: General case
                ImGui.TextUnformatted(channel.Value.ToString());
            }
            else
            {
                ImGui.TextUnformatted("NULL");
            }
        }
    }

}
