using FishNet.Object;

public abstract class OwnerBehaviour : NetworkBehaviour
{
    public override void OnStartClient()
    {
        if (!IsOwner)
        {
            enabled = false;
        }
    }
}
