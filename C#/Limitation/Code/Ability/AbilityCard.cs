using DG.Tweening;
using SHS.Abilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PSW.Cord.Ability
{
    public class AbilityCard : MonoBehaviour
    {
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI descriptionText;

        [Header("Image")]
        [SerializeField] private Image cardImage;

        [Header("Value")]
        [SerializeField] private float choicePointValue = -1500f;
        [SerializeField] private float initValue = 500f;
        [SerializeField] private float outValue = 1500;
        [SerializeField] private float timeValue = 0.4f;


        private AbilitySO _abilitySO;

        public void InitCard(AbilitySO ability)
        {
            _abilitySO = ability;
            nameText.text = ability.AbilityName;
            descriptionText.text = ability.Description;
            cardImage.sprite = ability.Icon;
            transform.DOMoveY(initValue, timeValue).SetEase(Ease.OutBack);
        }

        public AbilitySO ChoiceCard() //이거 반환값으로 SO 보내주고
        {
            transform.DOMoveY(choicePointValue, timeValue).SetEase(Ease.InBack);
            return _abilitySO;
        }

        public void OutCard()
        {
            transform.DOMoveY(outValue, timeValue).SetEase(Ease.InBack);
        }

        public void ScaleSet(float scaleValue) => transform.DOScale(new Vector2(1 + scaleValue, 1 + scaleValue),0.2f);

    }
}