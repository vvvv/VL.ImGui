using System;

namespace VL.ImGui
{
    public abstract class Widget
    {
        public Widget Input { get; set; }

        internal virtual void Reset() { }
        internal abstract void Update(Context context);
    }
}
