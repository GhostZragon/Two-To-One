using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByTrigger : Despawn
{
    public Spawner spawner;
    protected override void OnEnable()
    {
        base.OnEnable();
        isDespawn = false;
    }
    protected bool isDespawn = false;
    protected override bool CanDespawn()
    {
        return this.isDespawn;
    }
    public override void DespawnObject()
    {
        spawner.Despawn(transform.parent);
    }
    public void SetCanDespawn()
    {
        this.isDespawn = true;
    }
    
}
