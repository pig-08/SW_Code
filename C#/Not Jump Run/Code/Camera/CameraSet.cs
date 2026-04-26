using UnityEngine;

public class CameraSet : MonoBehaviour
{
    [SerializeField] private Player player;
    private void Update()
    {
        transform.rotation = player.transform.rotation;
    }
}
