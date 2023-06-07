public abstract class Despawn : QuangLibrary
{
    private void Update()
    {
        Despawning();
    }
    protected virtual void Despawning()
    {
        if (CanDespawn())
        {
            DespawnObject();
        }
    }
    public abstract void DespawnObject();

    protected abstract bool CanDespawn();
}
