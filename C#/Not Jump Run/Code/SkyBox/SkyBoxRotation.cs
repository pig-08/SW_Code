using UnityEngine;
using UnityEngine.UIElements;

public class SkyBoxRotation : MonoBehaviour
{
    [SerializeField] private float moveTiem = 5f;
    private Skybox _skybox;

    private void Awake()
    {
        _skybox = GetComponent<Skybox>();
        RenderSettings.skybox = _skybox.material;
    }

    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", moveTiem * Time.time);
    }
}