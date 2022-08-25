using System;

namespace VL.ImGui
{
    public abstract class Widget
    {
        internal virtual void Reset() { }

        internal abstract void UpdateCore(Context context);

        internal virtual void Update(Context context)
        {
            UpdateCore(context);
        }
    }
}
