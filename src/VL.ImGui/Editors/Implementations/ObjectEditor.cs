using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Disposables;
using System.Reflection;
using System.Runtime.CompilerServices;
using VL.Core;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.ImGui.Editors
{
    using ImGui = ImGuiNET.ImGui;

    sealed class ObjectEditor<T> : IObjectEditor, IDisposable
    {
        static readonly ConditionalWeakTable<IVLPropertyInfo, VLPropertyDescriptor> propertiesCache = new ConditionalWeakTable<IVLPropertyInfo, VLPropertyDescriptor>();
        readonly Dictionary<VLPropertyDescriptor, IObjectEditor?> editors = new Dictionary<VLPropertyDescriptor, IObjectEditor?>();
        readonly CompositeDisposable subscriptions = new CompositeDisposable();
        readonly Channel<T> channel;
        readonly IObjectEditorFactory factory;
        readonly IVLTypeInfo typeInfo;

        public ObjectEditor(Channel<T> channel, ObjectEditorContext editorContext, IVLTypeInfo typeInfo)
        {
            this.channel = channel;
            this.factory = editorContext.Factory;
            this.typeInfo = typeInfo;
        }

        public void Dispose()
        {
            subscriptions.Dispose();
        }

        public bool NeedsMoreThanOneLine => true;

        public void Draw(Context? context)
        {
            context = context.Validate();
            if (context is null)
                return;

            var typeInfo = this.typeInfo;
            if (channel.Value is IVLObject instance)
            {
                var properties = typeInfo.Properties.Select(GetDescriptor)
                    .Where(d => d.IsBrowsable)
                    .OrderBy(d => d.Order);
                foreach (var property in properties)
                {
                    if (!editors.TryGetValue(property, out var editor))
                    {
                        // Setup channel
                        var propertyChannel = Channel.CreateChannelOfType(property.PropertyType);
                        subscriptions.Add(
                            channel.Merge(
                                propertyChannel.ChannelOfObject,
                                (object v) => property.PropertyInfo.GetValue((IVLObject)channel.Value),
                                v => (T)property.PropertyInfo.WithValue((IVLObject)channel.Value, v),
                                initialization: ChannelMergeInitialization.UseA,
                                pushEagerlyTo: ChannelSelection.ChannelA));

                        propertyChannel.Attributes.Value = property.GetAttributes();
                        var contextForProperty = new ObjectEditorContext(factory, property.DisplayName);
                        editor = editors[property] = factory.CreateObjectEditor(propertyChannel, contextForProperty);
                    }

                    if (editor != null)
                    {
                        if (editor.NeedsMoreThanOneLine)
                        {
                            if (ImGui.TreeNode(property.DisplayName))
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

        VLPropertyDescriptor GetDescriptor(IVLPropertyInfo property)
        {
            return propertiesCache.GetValue(property, p => new VLPropertyDescriptor(p));
        }

        class VLPropertyDescriptor : PropertyDescriptor
        {
            readonly IVLPropertyInfo propertyInfo;
            int? order;
            string? displayName;

            public VLPropertyDescriptor(IVLPropertyInfo property)
                : base(property.NameForTextualCode, property.GetAttributes<Attribute>().ToArray())
            {
                this.propertyInfo = property;
            }

            public IVLPropertyInfo PropertyInfo => propertyInfo;

            public IReadOnlyList<Attribute> GetAttributes() => AttributeArray ?? Array.Empty<Attribute>();

            public override Type ComponentType => propertyInfo.DeclaringType.ClrType;

            public override bool IsReadOnly => false;

            public int Order
            {
                get
                {
                    return order ??= Compute();

                    int Compute()
                    {
                        if (Attributes[typeof(DisplayAttribute)] is DisplayAttribute displayAttribute)
                            return displayAttribute.GetOrder() ?? 10000;
                        return int.MaxValue;
                    }
                }
            }

            public override string DisplayName
            {
                get
                {
                    return displayName ??= Compute();

                    string Compute()
                    {
                        var baseResult = base.DisplayName;
                        if (baseResult != propertyInfo.NameForTextualCode)
                            return baseResult;

                        if (Attributes[typeof(DisplayAttribute)] is DisplayAttribute displayAttribute && displayAttribute.GetName() is string name)
                            return name;
                            
                        return propertyInfo.OriginalName;
                    }
                }
            }

            public override Type PropertyType => propertyInfo.Type.ClrType;

            public override bool CanResetValue(object component)
            {
                return false;
            }

            public override object? GetValue(object? component)
            {
                if (component is IVLObject obj)
                    return propertyInfo.GetValue(obj);
                return null;
            }

            public override void ResetValue(object component)
            {
            }

            public override void SetValue(object? component, object? value)
            {
                if (component is IVLObject obj)
                    propertyInfo.WithValue(obj, value);
            }

            public override bool ShouldSerializeValue(object component)
            {
                return false;
            }
        }
    }

}
