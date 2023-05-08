using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForTime : MonoBehaviour
{
    public IEnumerator DelayedScorePopUp(Btn btn, float time)
    {
        yield return new WaitForSeconds(time);
        btn.Cell.TriggerScorePopUp();
    }
    public IEnumerator ScaleDownWithDelay(Btn btn, float time)
    {
        yield return new WaitForSeconds(time);
        btn.Cell.ScaleDown();
    }
}
