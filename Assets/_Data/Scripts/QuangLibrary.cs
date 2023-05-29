using UnityEngine;

public class QuangLibrary : MonoBehaviour
{
    protected virtual void Reset()
    {
        LoadComponent();
    }
    protected virtual void Awake()
    {
        LoadComponent();
    }
    protected virtual void OnEnable()
    {
        LoadComponent();
    }
    protected virtual void LoadComponent()
    {

    }

    /// <summary>
    /// Return value between 0 and 1 from min and max value
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    protected virtual float ReturnLerpValue(float min, float max, float value)
    {
        float newValue = Mathf.InverseLerp(min, max, value);
        return newValue;
    }
}
