using UnityEngine;

public class SideScreen : Screen
{
    private void Start()
    {
        CanvasGroup.alpha = 0f;
        CanvasGroup.blocksRaycasts = false;
    }

    public override void Open()
    {
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.alpha = 1f;
    }

    public override void Close()
    {
        CanvasGroup.alpha = 0f;
        CanvasGroup.blocksRaycasts = false;
    }
}
