using Unity.Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomCameraSetBase : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner2D confiner2D;
    [SerializeField] private CinemachineBrain brain;
    private List<BoxCollider2D> colliders = new List<BoxCollider2D>();
    private void Awake()
    {
        GetComponentsInChildren(colliders);
        SetBossRoomCamera(false);
    }

    public void SetBossRoomCamera(bool value)
    {
        confiner2D.enabled = value;
    }
}
