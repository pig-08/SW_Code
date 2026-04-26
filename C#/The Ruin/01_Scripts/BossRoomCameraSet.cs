using Unity.Cinemachine;
using UnityEngine;

public class BossRoomCameraSet : MonoBehaviour
{
    [SerializeField] private int myValue;
    [SerializeField]private LayerMask playerLayer;
    private BossRoomCameraSetBase setBase;
    private BoxCollider2D boxCollider;
    private bool isOnCamera;
    private void Awake()
    {
        setBase = GetComponentInParent<BossRoomCameraSetBase>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(Physics2D.OverlapBox(transform.position, boxCollider.size,0,playerLayer)&&!isOnCamera)
        {
            setBase.SetBossRoomCamera(true);
            isOnCamera = true;
        }
    }
}
