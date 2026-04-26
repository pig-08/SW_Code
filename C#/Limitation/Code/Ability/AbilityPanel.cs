using SHS.Abilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PSW.Cord.Ability
{
    public class AbilityPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI numberText;
        [SerializeField] private Image augmentationImage;

        private int number = 1;

        public void Init(AbilitySO abilitySO)
        {
            numberText.text = number.ToString();
            augmentationImage.sprite = abilitySO.Icon;
        }

        public void UpgradeAbility()
        {
            number++;
            numberText.text = number.ToString();
        }

    }
}