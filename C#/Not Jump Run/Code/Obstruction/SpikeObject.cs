using UnityEngine;

public class SpikeObject : MonoBehaviour, IObjectComponent
{
    [SerializeField] private SoundSo spikeSound;

    [SerializeField] private Vector3 checkRange;
    [SerializeField] private Vector3 checkPoint;
    [SerializeField] private LayerMask playerMask;
    
    private Player _player;
    private Animator _animator;
    public void Init(Player player)
    {
        _player = player;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Physics.CheckBox(checkPoint + transform.position, checkRange * 0.5f, Quaternion.identity, playerMask))
        {
            SoundManager.sound.PlaySound(spikeSound);
            _animator.Play("SpikeObject");
            _player.ChangeState("DIE");
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(checkPoint + transform.position, checkRange);
    }
}