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
