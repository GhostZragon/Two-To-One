using System.Collections;
using UnityEngine;
public class CanvasTranstionManager : QuangLibrary
{
    public GameObject _object;
    public Vector3 _position;
    public float time = 0.3f;
    Vector3 localScale;
    public bool inScreen = false;
    protected override void Awake()
    {
        base.Awake();
        localScale = transform.localScale;
    }
    public IEnumerator PopIn(float time = 0)
    {
        yield return new WaitForSeconds(time);
        PopInCoroutine();

    }
    public IEnumerator PopOut(float time = 0)
    {
        yield return new WaitForSeconds(time);
        PopOutCoroutine();
    }

    protected virtual void PopInCoroutine()
    {
        _object.transform.LeanScale(Vector3.zero, time).setEaseInExpo().setOnComplete(() =>
        {
            _object.SetActive(false);
            inScreen = false;
        } );
        
        //yield return new WaitForSeconds(time);
    }
    protected virtual void PopOutCoroutine()
    {
        //float a = 0.56f;
        //Vector3 vector = new Vector3(a, a, a);
        _object.SetActive(true);

        _object.transform.localScale = Vector3.zero;
        _object.transform.LeanScale(localScale, time).setEaseOutExpo().setOnComplete(() =>
        {
            inScreen = true;
        });
        //yield return new WaitForSeconds(time);
    }
    
}
