using UnityEngine;

public class PlayerRagdoll : MonoBehaviour, IPlayerComponent
{
    private Player _player;
    private Rigidbody[] _rigidbodys;
    public void Init(Player player)
    {
        _player = player;
        _rigidbodys = GetComponentsInChildren<Rigidbody>();
        SetRagdoll(false);
    }

    public void SetRagdoll(bool isRagdoll)
    {
        foreach(Rigidbody rigidbody in _rigidbodys)
        {
            rigidbody.isKinematic = !isRagdoll;
        }
    }
}
