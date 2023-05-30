using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : QuangLibrary
{
    [SerializeField] protected Transform holder;
    [SerializeField] protected List<Transform> Prefabs;
    [SerializeField] protected List<Transform> poolObjs;

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
        foreach(Transform prefab in _prefabs)
        {
            Prefabs.Add(prefab);
            prefab.gameObject.SetActive(false);
        }
    }
    public virtual void Spawn(Vector3 position, Quaternion roration)
    {
        Transform prefab = GetPrefab();
        Transform newPrefab = GetObjectFromPool(prefab);
        newPrefab.gameObject.SetActive(true);
        newPrefab.position = position;
        newPrefab.rotation = roration;
        newPrefab.SetParent(holder);

    }
    public virtual void Despawn(Transform obj)
    {
        obj.gameObject.SetActive(false);
        Debug.Log("Working "+transform.name);
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
