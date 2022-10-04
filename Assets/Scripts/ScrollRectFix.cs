using UnityEngine;
using UnityEngine.UI;

public class ScrollRectFix : ScrollRect
{
    override protected void LateUpdate()
    {
        base.LateUpdate();
        if (verticalScrollbar)
        {
            verticalScrollbar.size = 0;
        }
    }

    override public void Rebuild(CanvasUpdate executing)
    {
        base.Rebuild(executing);
        if (verticalScrollbar)
        {
            verticalScrollbar.size = 0;
        }
    }
}
