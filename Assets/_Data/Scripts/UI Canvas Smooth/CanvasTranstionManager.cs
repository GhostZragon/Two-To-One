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
    public virtual void PopIn()
    {
        PopInCoroutine();
    }
    public virtual void PopOut()
    {
        PopOutCoroutine();
    }
    protected virtual void PopInCoroutine()
    {
        _object.transform.LeanScale(Vector3.zero, time).setEaseInBack().setOnComplete(() =>
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
        _object.transform.localScale = Vector3.zero;
        _object.transform.LeanScale(localScale, time).setEaseOutBack().setOnComplete(() =>
        {
            _object.SetActive(true);
            inScreen = true;
        });
        //yield return new WaitForSeconds(time);
    }
    
}
