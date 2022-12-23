using System.Reactive.Disposables;
using VL.Lib.Reactive;

namespace VL.ImGui.Editors
{
    using ImGui = ImGuiNET.ImGui;

    sealed class AbstractObjectEditor : IObjectEditor, IDisposable
    {
        readonly Channel upstreamChannel;
        readonly IDisposable upstreamChannelSubscription;
        readonly IObjectEditorFactory factory;

        readonly SerialDisposable editorOwnership = new SerialDisposable();
        readonly SerialDisposable channelSubscription = new SerialDisposable();
        readonly ObjectEditorContext editorContext;

        Channel? implementingChannel;
        IObjectEditor? currentEditor;
        bool isBusy;

        public AbstractObjectEditor(Channel channel, ObjectEditorContext editorContext)
        {
            this.upstreamChannel = channel;
            this.editorContext = editorContext;
            this.factory = editorContext.Factory;

            HandleUpstreamPush(channel.Object);
            upstreamChannelSubscription = channel.ToObservable().Subscribe(HandleUpstreamPush);
        }

        public void Dispose()
        {
            upstreamChannelSubscription.Dispose();
            channelSubscription.Dispose();
            editorOwnership.Dispose();
        }

        public bool NeedsMoreThanOneLine => currentEditor != null ? currentEditor.NeedsMoreThanOneLine : false;

        void HandleUpstreamPush(object? value)
        {
            if (isBusy)
                return;

            var type = value?.GetType();
            if (type is null || type == typeof(object))
            {
                editorOwnership.Disposable = null;
                currentEditor = null;
                return;
            }

            if (implementingChannel is null || type != implementingChannel.ClrTypeOfValues)
            {
                // Build new channel using the runtime type
                implementingChannel = Channel.CreateChannelOfType(type);
                implementingChannel.Object = value;
                channelSubscription.Disposable = implementingChannel.ToObservable().Subscribe(v =>
                {
                    // Push to upstream channel
                    PushValue(upstreamChannel, v);
                });

                // Select new editor
                currentEditor = factory.CreateObjectEditor(implementingChannel, editorContext);
                editorOwnership.Disposable = currentEditor as IDisposable;
            }

            // Push to private channel
            PushValue(implementingChannel, value!);
        }

        void PushValue(Channel channel, object value)
        {
            if (isBusy)
                return;

            isBusy = true;
            try
            {
                channel.Object = value;
            }
            finally
            {
                isBusy = false;
            }
        }

        public void Draw(Context? context)
        {
            context = context.Validate();
            if (context is null)
                return;

            if (currentEditor != null)
            {
                currentEditor.Draw(context);
            }
            else if (upstreamChannel.Object is not null)
            {
                ImGui.TextUnformatted(upstreamChannel.Object.ToString());
            }
            else
            {
                ImGui.TextUnformatted("NULL");
            }
        }
    }

}
