using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : QuangLibrary
{
    [SerializeField] protected Transform holder;
    [SerializeField] protected List<Transform> Prefabs;
    [SerializeField] protected List<Transform> poolObjs;
    //private static Spawner instance;
    //public static Spawner Instance { get => instance; }
    //protected override void Awake()
    //{
    //    base.Awake();
    //    if(instance != null)
    //    {
    //        Debug.LogError("There is more than one Spawner in the scene");
    //    }
    //    else
    //    {
    //        instance = this;
    //    }
    //}
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPrefabs();
        this.LoadHolder();
    }
    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder");
    }
    protected virtual void LoadPrefabs()
    {
        Prefabs = new List<Transform>();
        Transform _prefabs = transform.Find("Prefabs");
        if (_prefabs == null) return;
        foreach(Transform prefab in _prefabs)
        {
            Prefabs.Add(prefab);
            prefab.gameObject.SetActive(false);
        }
    }
    public virtual Transform Spawn(Vector3 position, Quaternion roration)
    {
        Transform prefab = GetPrefab();
        Transform newPrefab = GetObjectFromPool(prefab);
        newPrefab.gameObject.SetActive(true);
        newPrefab.position = position;
        newPrefab.rotation = roration;
        newPrefab.SetParent(holder);
        return newPrefab;

    }
    public virtual void Despawn(Transform obj)
    {
        obj.gameObject.SetActive(false);
        //Debug.Log("Working "+transform.name);
        poolObjs.Add(obj);
    }
    public virtual Transform GetObjectFromPool(Transform _prefab)
    {
        foreach(Transform obj in poolObjs)
        {
            if(obj.name == _prefab.name)
            {
                poolObjs.Remove(obj);
                return obj;
            }
        }
        return Spawn(_prefab);
    }
    Transform Spawn(Transform prefab)
    {
        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        return newPrefab;
    }
    private Transform GetPrefab()
    {
        if(Prefabs.Count == 0)
            return null;
        return Prefabs[0];
    }
}
