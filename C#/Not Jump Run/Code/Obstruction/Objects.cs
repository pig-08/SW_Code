using System.Linq;
using UnityEngine;

public class Objects : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Start()
    {
        GetComponentsInChildren<IObjectComponent>().ToList().ForEach(v => v.Init(player));
    }
}
