using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForTime : MonoBehaviour
{
    public IEnumerator AddScoreForTime(Btn btn, float time)
    {
        yield return new WaitForSeconds(time);
        btn.Cell.AddScore();
    }
    public IEnumerator DownScaleForTime(Btn btn, float time)
    {
        yield return new WaitForSeconds(time);
        btn.Cell.DownScale();
    }
}
