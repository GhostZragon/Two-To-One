using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DespawnByButton))]
public class DespawnByButtonCustom: Editor
{
    private DespawnByButton despawnByButton;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        despawnByButton = (DespawnByButton) target;
        if(GUILayout.Button("Despawn"))
        {
            despawnByButton.OnButtonDown();
        }
    }
}
public class DespawnByButton : Despawn
{
    protected override void OnEnable()
    {
        base.OnEnable();
        isDespawning = false;
    }
    private bool isDespawning = false;
    public void OnButtonDown()
    {
        isDespawning = true;
    }
    protected override bool CanDespawn()
    {
        return this.isDespawning;
    }
    public override void DespawnObject()
    {
        SquareSpawner.Instance.Despawn(transform.parent);
    }

}
