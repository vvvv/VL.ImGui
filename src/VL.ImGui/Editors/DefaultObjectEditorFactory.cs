﻿using Stride.Core.Mathematics;
using System.Reflection;
using VL.Core;
using VL.Core.EditorAttributes;
using VL.ImGui.Widgets;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.ImGui.Editors
{
    public sealed partial class DefaultObjectEditorFactory : IObjectEditorFactory
    {
        public IObjectEditor? CreateObjectEditor(Channel channel, ObjectEditorContext context)
        {
            var createMethod = typeof(DefaultObjectEditorFactory).GetMethods()
                .First(m => m.IsGenericMethod && m.Name == nameof(CreateObjectEditor))
                .MakeGenericMethod(channel.ClrTypeOfValues);
            return (IObjectEditor?)createMethod.Invoke(this, new object[] { channel, context });
        }

        public IObjectEditor? CreateObjectEditor<T>(Channel<T> channel, ObjectEditorContext context)
        {
            // Is there a widget for exactly that type?
            var widgetType = context.Attributes.OfType<WidgetTypeAttribute>().FirstOrDefault()?.WidgetType ?? GetDefaultWidgetType<T>();
            var widgetClass = typeof(ChannelWidget<T>).Assembly.GetTypes()
                .Where(t => !t.IsAbstract && typeof(ChannelWidget<T>).IsAssignableFrom(t) && t.GetConstructor(Array.Empty<Type>()) != null)
                .OrderBy(t => widgetType == t.GetCustomAttribute<WidgetTypeAttribute>()?.WidgetType ? 0 : 1)
                .FirstOrDefault();
            if (widgetClass != null)
            {
                var widget = (ChannelWidget<T>)Activator.CreateInstance(widgetClass)!;
                widget.Channel = channel;
                var labelProperty = widgetClass.GetProperty(nameof(InputFloat.Label));
                if (labelProperty != null && labelProperty.PropertyType == typeof(string))
                    labelProperty.SetValue(widget, context.Label);
                return new ObjectEditorBasedOnChannelWidget<T>(widget);
            }

            if (channel is Channel<object> objectChannel)
                return new RuntimeObjectEditor(objectChannel, context);

            if (typeof(T).IsEnum)
                return new EnumEditor<T>(channel, context);

            if (typeof(T).IsArray)
                return Activator.CreateInstance(typeof(ArrayEditor<>).MakeGenericType(typeof(T).GetElementType()!), new object[] { channel, context }) as IObjectEditor;

            if (typeof(T).IsConstructedGenericType)
            {
                if (typeof(T).GetGenericTypeDefinition() == typeof(Spread<>))
                    return Activator.CreateInstance(typeof(SpreadEditor<>).MakeGenericType(typeof(T).GenericTypeArguments), new object[] { channel, context }) as IObjectEditor;
                // More collections
            }

            var typeInfo = TypeRegistry.Default.GetTypeInfo(typeof(T));
            return new ObjectEditor<T>(channel, context, typeInfo);
        }

        private static WidgetType GetDefaultWidgetType<T>()
        {
            if (IsNumericType<T>() || IsVectorType<T>())
                return WidgetType.Drag;

            if (typeof(T) == typeof(string))
                return WidgetType.Input;

            return WidgetType.Default;
        }

        private static bool IsNumericType<T>()
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsVectorType<T>()
        {
            return typeof(T) == typeof(Vector2) || typeof(T) == typeof(Vector3) || typeof(T) == typeof(Vector4);
        }
    }
}
