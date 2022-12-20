using System.Collections.Immutable;
using System.Reactive.Disposables;
using System.Reflection;
using VL.Core;
using VL.Core.EditorAttributes;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.ImGui.Editors
{
    using ImGui = ImGuiNET.ImGui;

    sealed class ObjectEditor<T> : IObjectEditor, IDisposable
    {
        readonly Dictionary<IVLPropertyInfo, IObjectEditor?> editors = new Dictionary<IVLPropertyInfo, IObjectEditor?>();
        readonly CompositeDisposable subscriptions = new CompositeDisposable();
        readonly Channel<T> channel;
        readonly IObjectEditorFactory factory;
        readonly IVLTypeInfo typeInfo;

        readonly SerialDisposable editorOwnership = new SerialDisposable();
        readonly SerialDisposable channelSubscription = new SerialDisposable();
        readonly ObjectEditorContext editorContext;
        readonly ImmutableArray<IVLTypeInfo> implementingTypes;
        readonly string[]? entries;

        Channel? implementingChannel;
        IObjectEditor? currentEditor;

        public ObjectEditor(Channel<T> channel, ObjectEditorContext editorContext, IVLTypeInfo typeInfo)
        {
            this.channel = channel;
            this.editorContext = editorContext;
            this.factory = editorContext.Factory;
            this.typeInfo = typeInfo;

            if (typeInfo.IsInterface || (!typeInfo.IsPatched && typeInfo.ClrType.IsAbstract))
            {
                this.implementingTypes = GetImplementingTypes(typeInfo).ToImmutableArray();
                this.entries = implementingTypes.Select(GetLabel).ToArray();
            }
        }

        public void Dispose()
        {
            subscriptions.Dispose();
            channelSubscription.Dispose();
            editorOwnership.Dispose();
        }

        public bool NeedsMoreThanOneLine => true;

        public void Draw(Context? context)
        {
            context = context.Validate();
            if (context is null)
                return;

            var typeInfo = this.typeInfo;

            // Type selector?
            if (entries != null)
            {
                var currentItem = GetIndex(channel.Value);
                if (ImGui.Combo($"Type##{GetHashCode()}", ref currentItem, entries, entries.Length))
                {
                    // Kill the current channel
                    channelSubscription.Disposable = null;
                    implementingChannel = null;
                    currentEditor = null;
                    editorOwnership.Disposable = null;

                    // Create a new value of the specified type
                    channel.Value = CreateNewValue(implementingTypes.ElementAtOrDefault(currentItem))!;
                }

                var currentType = implementingTypes.ElementAtOrDefault(currentItem);
                if (currentType != null)
                {
                    if (implementingChannel?.ClrTypeOfValues != currentType.ClrType)
                    {
                        // Build new channel and bind to abstract channel
                        implementingChannel = Channel.CreateChannelOfType(currentType);
                        channelSubscription.Disposable = channel.Merge(implementingChannel.ChannelOfObject);

                        // Select new editor
                        currentEditor = factory.CreateObjectEditor(implementingChannel, editorContext);
                        editorOwnership.Disposable = currentEditor as IDisposable;
                    }
                }
            }

            // Is there a specific editor?
            if (currentEditor != null)
            {
                currentEditor.Draw(context);
                return;
            }

            if (channel.Value is IVLObject instance)
            {
                foreach (var property in typeInfo.Properties)
                {
                    if (!editors.TryGetValue(property, out var editor))
                    {
                        // Setup channel
                        var propertyChannel = Channel.CreateChannelOfType(property.Type);
                        subscriptions.Add(
                            channel.BindTwoWay(
                                propertyChannel.ChannelOfObject,
                                v => property.GetValue((IVLObject)channel.Value),
                                v => (T)property.WithValue((IVLObject)channel.Value, v)));

                        var attributes = property.GetAttributes<Attribute>().ToList();
                        var label = attributes.OfType<LabelAttribute>().FirstOrDefault()?.Label ?? property.OriginalName;
                        var contextForProperty = new ObjectEditorContext(factory, attributes, label);
                        editor = editors[property] = factory.CreateObjectEditor(propertyChannel, contextForProperty);
                    }

                    if (editor != null)
                    {
                        if (editor.NeedsMoreThanOneLine)
                        {
                            if (ImGui.TreeNode(property.OriginalName))
                            {
                                try
                                {
                                    editor.Draw(context);
                                }
                                finally
                                {
                                    ImGui.TreePop();
                                }
                            }
                        }
                        else
                        {
                            editor.Draw(context);
                        }
                    }
                }
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

        static IEnumerable<IVLTypeInfo> GetImplementingTypes(IVLTypeInfo typeInfo)
        {
            var clrType = typeInfo.ClrType;
            var typeRegistry = TypeRegistry.Default;
            foreach (var type in clrType.Assembly.GetTypes())
            {
                if (type.IsAbstract)
                    continue;

                if (!clrType.IsAssignableFrom(type))
                    continue;

                var vlType = typeRegistry.GetTypeInfo(type);
                if (!vlType.IsPatched && !HasDefaultConstructor(vlType))
                    continue;

                yield return vlType;
            }
        }

        static bool HasDefaultConstructor(IVLTypeInfo type) => type.ClrType.GetConstructor(Array.Empty<Type>()) != null;

        static string GetLabel(IVLTypeInfo typeInfo)
        {
            var labelAttribute = typeInfo.ClrType.GetCustomAttribute<LabelAttribute>();
            if (labelAttribute != null)
                return labelAttribute.Label;
            var displayAttribute = typeInfo.ClrType.GetCustomAttribute<Stride.Core.DisplayAttribute>();
            if (displayAttribute != null)
                return displayAttribute.Name;
            var displayAttribute2 = typeInfo.ClrType.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>();
            if (displayAttribute2?.Name != null)
                return displayAttribute2.Name;
            return typeInfo.ToString()!;
        }

        int GetIndex(T? value)
        {
            if (value is null)
                return -1;

            var typeInfo = TypeRegistry.Default.GetTypeInfo(value.GetType());
            return implementingTypes.IndexOf(typeInfo);
        }

        T? CreateNewValue(IVLTypeInfo? typeInfo)
        {
            if (typeInfo is null)
                return default;
            if (typeInfo.IsPatched)
                return (T)typeInfo.CreateInstance(NodeContext.Default);
            else
                return (T)Activator.CreateInstance(typeInfo.ClrType)!;
        }
    }

}
