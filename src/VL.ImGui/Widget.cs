using System;

namespace VL.ImGui
{
    public abstract class Widget
    {
        internal virtual void Reset() { }
        internal abstract void Update(Context context);
    }
}
