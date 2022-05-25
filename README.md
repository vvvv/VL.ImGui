# VL.ImGui

A first sketch of a node set for [vvvv](visualprogramming.net) around [ImGui](https://github.com/ocornut/imgui). The mesh produced by ImGui gets rendered with VL.Skia and the input notifications of VL.Skia get translated to ImGui.

## Installation
- Have vvvv 2021.4.8 installed at it's default location
- Clone this repository
- Open the solution file and build once
- Copy `cimgui.dll` from the build output beside the `vvvv.exe`

## Design thoughts
In general the nodes are written in C# and exposed via a node factory. Thinking here was to automate the wrapping process at some point.

### Retained mode
Maybe a little bit of a contradiction, but the current node set does not offer an "immediate" mode. Instead the original API gets wrapped in widget classes which can be connected to each other by the user and will later get traversed by the `ToSkiaLayer` node.
The benefits are that the end-user doesn't need to take care of the state machinge (`Begin`/`End` calls), but on the other hand it takes a way a lot of decisions thereby reducing the overall degree of freedom.

- The widgets are represented as an object graph which gets traversed by `ToSkiaLayer`
- Each node returns a widget and takes a widget. That way widgets can be stacked vertically (following ImGuis default layout mode, which can be thought of like a text cursor doing a new line for each widget).
- Internally such widget chains will be represented to ImGui as a group (having it's own bounds). Subsequent layouting methods will therefor operate on the whole stack.
- To open a horizontal group one can use the (badly named) `Group` node together with a `Cons` node.

#### To be discussed
- Currently the nodes output the modifed values and a boolean - should we change that to a `BehaviourSubject`? Maybe that would better express that the value is not changed immediately, would also follow past UI approaches and allow the value to be modified from down stream / adds more databinding options.
- Is the input/output widget chaining a good approach? Does it feel natural?
- Can we generate most of the code? Or will there be too many exceptions and details so it's maybe not worth the effort? Or maybe wait on the upstream changes in vvvv which will allow to write nodes directly without the node factory indirection.

### Immediate mode
We can also think of a second node set exposing the real immediate mode as regions (taking care of the `Begin`/`End` calls). Those regions could in a first iteration be patched with the custom region API and if need be get replaced by ones generating C# code for best performance.

In any case this approach would follow the original design principles much more closely and therefor would offer a much higher degree of freedom.

#### To be discussed
- How to deal with the `ref` parameters - VL has no concept for this yet.
