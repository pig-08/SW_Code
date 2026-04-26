using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCard : MonoBehaviour
{
    [SerializeField] private Image enemyImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void Init(EnemyDataSO enemyData)
    {
        enemyImage.sprite = enemyData.enemyImage;
        if (enemyData.enemyImageMap != null)
            enemyImage.material = enemyData.enemyImageMap;

        nameText.text = enemyData.enemyName;
        descriptionText.text = enemyData.enemyDescription;
    }

}
