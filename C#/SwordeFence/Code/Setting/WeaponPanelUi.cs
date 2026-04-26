using AJ;
using SW.Code.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SW.Code.Setting
{
    public class WeaponPanelUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] _textList;

        [SerializeField] private Image icon;
        [SerializeField] private Image iconPanel;

        [SerializeField] private Sprite specialPanelImage;

        public void Init(TurretSO turretData)
        {
            if (turretData.Special)
            {
                iconPanel.sprite = specialPanelImage;
                iconPanel.color = Color.yellow;
            }

            icon.sprite = turretData.TurretImage;
            _textList[0].text = turretData.TurretName;
            _textList[1].text = $"공격력 : {turretData.AttackDamage}";
            _textList[2].text = $"가격 : {turretData.TurretPrice}";
            _textList[3].text = $"등급 : {turretData.TurretRating}";
            _textList[4].text = $"사정거리 : {turretData.AttackRange}";

        }


    }
}