using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VL.Core;
using VL.Core.CompilerServices;
using VL.ImGui.Widgets;
using VL.ImGui.Windows;

[assembly: AssemblyInitializer(typeof(VL.ImGui.Initialization))]

namespace VL.ImGui
{
    public sealed class Initialization : AssemblyInitializer<Initialization>
    {
        protected override void RegisterServices(IVLFactory factory)
        {
            factory.RegisterNodeFactory(NodeBuilding.NewNodeFactory(factory, "VL.ImGUI.Nodes", f =>
            {
                var nodes = GetNodes(f).ToImmutableArray();
                return NodeBuilding.NewFactoryImpl(nodes);
            }));
        }

    

        static IEnumerable<IVLNodeDescription> GetNodes(IVLNodeDescriptionFactory factory)
        {
            yield return DemoWindow.GetNodeDescription_RetainedMode(factory);

            yield return ObjectEditor.GetNodeDescription_RetainedMode(factory);

            yield return SkiaWidget.GetNodeDescription_RetainedMode(factory);

            //Slider
            yield return SliderFloat.GetNodeDescription_RetainedMode(factory);
            yield return SliderFloatVertical.GetNodeDescription_RetainedMode(factory);
            yield return SliderInt.GetNodeDescription_RetainedMode(factory);
            yield return SliderIntVertical.GetNodeDescription_RetainedMode(factory);
            yield return SliderVector2.GetNodeDescription_RetainedMode(factory);
            yield return SliderVector3.GetNodeDescription_RetainedMode(factory);
            yield return SliderVector4.GetNodeDescription_RetainedMode(factory);
            //Dropdown
            yield return Combo.GetNodeDescription_RetainedMode(factory);

            // Windows
            yield return Window.GetNodeDescription_RetainedMode(factory);
            yield return ChildWindow.GetNodeDescription_RetainedMode(factory);

            // Buttons
            yield return Button.GetNodeDescription_RetainedMode(factory);
            yield return InvisibleButton.GetNodeDescription_RetainedMode(factory);
            yield return Selectable.GetNodeDescription_RetainedMode(factory);
            yield return ArrowButton.GetNodeDescription_RetainedMode(factory);

            // Checkbox
            yield return Checkbox.GetNodeDescription_RetainedMode(factory);

            // Radiobutton
            yield return RadioButton.GetNodeDescription_RetainedMode(factory);

            // Separator
            yield return Separator.GetNodeDescription_RetainedMode(factory);

            // Dummy
            yield return Dummy.GetNodeDescription_RetainedMode(factory);

            // Input
            yield return InputFloat.GetNodeDescription_RetainedMode(factory);
            yield return InputInt.GetNodeDescription_RetainedMode(factory);

            // Style
            yield return SetStyleColor.GetNodeDescription_RetainedMode(factory);
            yield return SetStyleVarFloat.GetNodeDescription_RetainedMode(factory);
            yield return SetStyleVarVector.GetNodeDescription_RetainedMode(factory);
            yield return Styling.SetFrameStyle.GetNodeDescription_RetainedMode(factory);

            // Layout
            yield return Row.GetNodeDescription_RetainedMode(factory);
            yield return Column.GetNodeDescription_RetainedMode(factory);
            yield return Fold.GetNodeDescription_RetainedMode(factory);
            yield return SameLine.GetNodeDescription_RetainedMode(factory);
            yield return SetIndent.GetNodeDescription_RetainedMode(factory);
            yield return SetWidth.GetNodeDescription_RetainedMode(factory);
            yield return SetNextWindowContentSize.GetNodeDescription_RetainedMode(factory);
            yield return SetID.GetNodeDescription_RetainedMode(factory);
            yield return SetAlignTextToFramePadding.GetNodeDescription_RetainedMode(factory);
            yield return CalcTextSize.GetNodeDescription_RetainedMode(factory);
            yield return CalcItemWidth.GetNodeDescription_RetainedMode(factory);
            yield return GetItemRectSize.GetNodeDescription_RetainedMode(factory);
            yield return GetItemRectMin.GetNodeDescription_RetainedMode(factory);
            yield return GetItemRectMax.GetNodeDescription_RetainedMode(factory);
            yield return GetWindowSize.GetNodeDescription_RetainedMode(factory);
            yield return GetCursorPos.GetNodeDescription_RetainedMode(factory);
            yield return GetContentRegionAvail.GetNodeDescription_RetainedMode(factory);
            yield return GetContentRegionMax.GetNodeDescription_RetainedMode(factory);
            yield return Bullet.GetNodeDescription_RetainedMode(factory);
            yield return SetPosition.GetNodeDescription_RetainedMode(factory);
            yield return Spacing.GetNodeDescription_RetainedMode(factory);

            // Text
            yield return TextWidget.GetNodeDescription_RetainedMode(factory);
            yield return SetTextWrapPosition.GetNodeDescription_RetainedMode(factory);

            // Input
            yield return InputText.GetNodeDescription_RetainedMode(factory);
            yield return InputTextMultiline.GetNodeDescription_RetainedMode(factory);
            yield return InputTextWithHint.GetNodeDescription_RetainedMode(factory);

            //Tooltips
            yield return SetTooltipText.GetNodeDescription_RetainedMode(factory);
            yield return SetTooltipWidget.GetNodeDescription_RetainedMode(factory);

            // Table
            yield return Table.GetNodeDescription_RetainedMode(factory);
            yield return TableNextColumn.GetNodeDescription_RetainedMode(factory);
            yield return TableSetupColumn.GetNodeDescription_RetainedMode(factory);

            // Behaviour
            yield return IsItemClicked.GetNodeDescription_RetainedMode(factory);
            yield return IsItemHovered.GetNodeDescription_RetainedMode(factory);

            // Color
            yield return ColorEdit.GetNodeDescription_RetainedMode(factory);
            yield return ColorPicker.GetNodeDescription_RetainedMode(factory);

            //Plotting
            yield return PlotHistogram.GetNodeDescription_RetainedMode(factory);
            yield return PlotLines.GetNodeDescription_RetainedMode(factory);
            yield return ProgressBar.GetNodeDescription_RetainedMode(factory);




            //////
            yield return ImmediateModeWidget.GetNodeDescription_RetainedMode(factory);
            //////


            //////
            yield return RetainedModeWidget.GetNodeDescription_ImmediateMode(factory);
            //////





            yield return DemoWindow.GetNodeDescription_ImmediateMode(factory);

            yield return ObjectEditor.GetNodeDescription_ImmediateMode(factory);

            yield return SkiaWidget.GetNodeDescription_ImmediateMode(factory);

            //Slider
            yield return SliderFloat.GetNodeDescription_ImmediateMode(factory);
            yield return SliderFloatVertical.GetNodeDescription_ImmediateMode(factory);
            yield return SliderInt.GetNodeDescription_ImmediateMode(factory);
            yield return SliderIntVertical.GetNodeDescription_ImmediateMode(factory);
            yield return SliderVector2.GetNodeDescription_ImmediateMode(factory);
            yield return SliderVector3.GetNodeDescription_ImmediateMode(factory);
            yield return SliderVector4.GetNodeDescription_ImmediateMode(factory);
            //Dropdown
            yield return Combo.GetNodeDescription_ImmediateMode(factory);

            // Windows
            //yield return Window.GetNodeDescription_ImmediateMode(factory);
            //yield return ChildWindow.GetNodeDescription_ImmediateMode(factory);

            // Buttons
            yield return Button.GetNodeDescription_ImmediateMode(factory);
            yield return InvisibleButton.GetNodeDescription_ImmediateMode(factory);
            yield return Selectable.GetNodeDescription_ImmediateMode(factory);
            yield return ArrowButton.GetNodeDescription_ImmediateMode(factory);

            // Checkbox
            yield return Checkbox.GetNodeDescription_ImmediateMode(factory);

            // Radiobutton
            yield return RadioButton.GetNodeDescription_ImmediateMode(factory);

            // Separator
            yield return Separator.GetNodeDescription_ImmediateMode(factory);

            // Dummy
            yield return Dummy.GetNodeDescription_ImmediateMode(factory);

            // Input
            yield return InputFloat.GetNodeDescription_ImmediateMode(factory);
            yield return InputInt.GetNodeDescription_ImmediateMode(factory);

            // Style
            yield return SetStyleColor.GetNodeDescription_ImmediateMode(factory);
            yield return SetStyleVarFloat.GetNodeDescription_ImmediateMode(factory);
            yield return SetStyleVarVector.GetNodeDescription_ImmediateMode(factory);
            yield return Styling.SetFrameStyle.GetNodeDescription_ImmediateMode(factory);

            // Layout
            //yield return Row.GetNodeDescription_ImmediateMode(factory);
            //yield return Column.GetNodeDescription_ImmediateMode(factory);
            //yield return Fold.GetNodeDescription_ImmediateMode(factory);
            yield return SameLine.GetNodeDescription_ImmediateMode(factory);
            yield return SetIndent.GetNodeDescription_ImmediateMode(factory);
            yield return SetWidth.GetNodeDescription_ImmediateMode(factory);
            yield return SetNextWindowContentSize.GetNodeDescription_ImmediateMode(factory);
            yield return SetID.GetNodeDescription_ImmediateMode(factory);
            yield return SetAlignTextToFramePadding.GetNodeDescription_ImmediateMode(factory);
            yield return CalcTextSize.GetNodeDescription_ImmediateMode(factory);
            yield return CalcItemWidth.GetNodeDescription_ImmediateMode(factory);
            yield return GetItemRectSize.GetNodeDescription_ImmediateMode(factory);
            yield return GetItemRectMin.GetNodeDescription_ImmediateMode(factory);
            yield return GetItemRectMax.GetNodeDescription_ImmediateMode(factory);
            yield return GetWindowSize.GetNodeDescription_ImmediateMode(factory);
            yield return GetCursorPos.GetNodeDescription_ImmediateMode(factory);
            yield return GetContentRegionAvail.GetNodeDescription_ImmediateMode(factory);
            yield return GetContentRegionMax.GetNodeDescription_ImmediateMode(factory);
            yield return Bullet.GetNodeDescription_ImmediateMode(factory);
            yield return SetPosition.GetNodeDescription_ImmediateMode(factory);
            yield return Spacing.GetNodeDescription_ImmediateMode(factory);

            // Text
            yield return TextWidget.GetNodeDescription_ImmediateMode(factory);
            yield return SetTextWrapPosition.GetNodeDescription_ImmediateMode(factory);

            // Input
            yield return InputText.GetNodeDescription_ImmediateMode(factory);
            yield return InputTextMultiline.GetNodeDescription_ImmediateMode(factory);
            yield return InputTextWithHint.GetNodeDescription_ImmediateMode(factory);

            //Tooltips
            //yield return SetTooltipText.GetNodeDescription_ImmediateMode(factory);
            //yield return SetTooltipWidget.GetNodeDescription_ImmediateMode(factory);

            // Table
            //yield return Table.GetNodeDescription_ImmediateMode(factory);
            yield return TableNextColumn.GetNodeDescription_ImmediateMode(factory);
            yield return TableSetupColumn.GetNodeDescription_ImmediateMode(factory);

            // Behaviour
            yield return IsItemClicked.GetNodeDescription_ImmediateMode(factory);
            yield return IsItemHovered.GetNodeDescription_ImmediateMode(factory);

            // Color
            yield return ColorEdit.GetNodeDescription_ImmediateMode(factory);
            yield return ColorPicker.GetNodeDescription_ImmediateMode(factory);

            //Plotting
            yield return PlotHistogram.GetNodeDescription_ImmediateMode(factory);
            yield return PlotLines.GetNodeDescription_ImmediateMode(factory);
            yield return ProgressBar.GetNodeDescription_ImmediateMode(factory);
        }
    }

    internal static class NodeBuildingUtils
    {
        public static IVLPinDescription Input<T>(this NodeBuilding.NodeDescriptionBuildContext c, string name, T defaultValue, string? summary = null, string? remarks = null)
        {
            return c.Pin(name, typeof(T), defaultValue, summary, remarks);
        }

        public static IVLPinDescription Output<T>(this NodeBuilding.NodeDescriptionBuildContext c, string name, T? witness = default, string? summary = null, string? remarks = null)
        {
            return c.Pin(name, typeof(T), summary, remarks);
        }
    }
}
