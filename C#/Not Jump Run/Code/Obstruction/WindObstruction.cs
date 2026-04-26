using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WindObstruction : MonoBehaviour, IObjectComponent
{
    [SerializeField] private Transform soundPoint;
    [SerializeField] private SoundSo fanSound;
    [SerializeField] private float pushPower;
    [SerializeField] private Vector3 checkRange;
    [SerializeField] private Vector3 checkPoint;
    [SerializeField] private LayerMask playerMask;

    private Player _player;
    private ShoesItem _playerShoesItem;

    public void Init(Player player)
    {
        _player = player;
        _playerShoesItem = _player.GetCompo<Items>().GetComp<ShoesItem>();
        SoundManager.sound.PlaySound(fanSound, soundPoint.position);
    }

    private void Update()
    {
        if (Physics.CheckBox(checkPoint + transform.position, checkRange * 0.5f, Quaternion.identity, playerMask)
            && _playerShoesItem.GetPick() == false)
        {
            _player.transform.position += transform.TransformDirection(Vector3.up) * pushPower * Time.deltaTime;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(checkPoint + transform.position, checkRange);
    }

    
}
