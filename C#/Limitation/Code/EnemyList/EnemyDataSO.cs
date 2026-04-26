using UnityEngine;

[CreateAssetMenu(fileName ="EnemyDataSO",menuName = "SO/Data/Enemy")]
public class EnemyDataSO : ScriptableObject
{
    public Sprite enemyImage;
    public Material enemyImageMap;

    public string enemyName;
    public string enemyDescription;
}
