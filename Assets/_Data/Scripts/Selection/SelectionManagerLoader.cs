using UnityEngine;

public class SelectionManagerLoader : QuangLibrary
{
    [SerializeField] protected WaitForTime waitForTime;

    protected override void LoadComponent()
    {
        base.LoadComponent();

        this.LoadSelectionWaitForTime();
    }
    protected virtual void LoadSelectionWaitForTime()
    {
        if (waitForTime != null) return;
        waitForTime = FindObjectOfType<WaitForTime>();
    }



}
