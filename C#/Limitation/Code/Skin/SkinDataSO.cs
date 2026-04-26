using UnityEngine;

[CreateAssetMenu(fileName = "SkinDataSO", menuName = "SO/Skin/SkinSO")]
public class SkinDataSO : ScriptableObject
{
    public Sprite skinImage;
    public Material skinMap;
    public Color color = Color.white;
    public float rotation;
}