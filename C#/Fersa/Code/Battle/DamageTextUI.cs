using DG.Tweening;
using PSB_Lib.ObjectPool.RunTime;
using PSW.Code.Battle;
using PSW.Code.Text;
using System;
using TMPro;
using UnityEngine;
using YIS.Code.Defines;

namespace SW.Code.Battle
{
    public class DamageTextUI : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public PoolItemSO PoolItem { get; private set; }

        [SerializeField] private DamageColorListSO damageColorList;
        [SerializeField] private TextMeshPro text;
        [SerializeField] private TextAnimator textAnimator;

        [SerializeField] private float upValue = 0.8f;
        [SerializeField] private float upTime = 0.2f;
        [SerializeField] private float delayTime = 0.5f;

        private Pool _myPool;

        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public async void PopUpText(Vector2 startPos, Elemental type, float damage)
        {
            string damageText = String.Empty;
            if (damage <= .0f)
            {
                damageText = "빗나감!";
            }
            else
            {
                float damageValue = MathF.Round(damage, 1);
                damageText = damageValue.ToString();
            }
            
            textAnimator.Appearance(damageText);

            transform.position = startPos;
            text.color = Color.white;
            text.colorGradientPreset = damageColorList.GetColor(type);

            text.DOFade(1, upTime);
            await Awaitable.WaitForSecondsAsync(upTime + delayTime);
            text.DOFade(0, upTime).OnComplete(() => _myPool.Push(this));
        }
        
        public async void PopUpText(Vector2 startPos, float damage)
        {
            string damageText = String.Empty;
            if (damage <= .0f)
            {
                damageText = "빗나감!";
            }
            else
            {
                float damageValue = MathF.Round(damage, 1);
                damageText = damageValue.ToString();
            }
            
            textAnimator.Appearance(damageText);

            transform.position = startPos;
            text.colorGradientPreset = null;
            text.color = Color.green;

            text.DOFade(1, upTime);
            await Awaitable.WaitForSecondsAsync(upTime + delayTime);
            text.DOFade(0, upTime).OnComplete(() => _myPool.Push(this));
        }

        public void ResetItem()
        {
            text.alpha = 0;
        }
        
    }
}