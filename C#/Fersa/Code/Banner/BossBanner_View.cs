using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossBanner_View : MonoBehaviour, IView<Vector2>
{
    [SerializeField] private Image bossImage;
    [SerializeField] private TextMeshProUGUI bossNameText;
    [SerializeField] private Animator bossBannerAnimator;

    public void Init(Vector2 defaultData){}

    public void SetData(Vector2 data){}

    public void PlayBannerCilp(string cilpName) => bossBannerAnimator.Play(cilpName, 0, 0);

    public void SetBossData(Sprite bossSprite, string bossName)
    {
        bossImage.sprite = bossSprite;
        bossNameText.SetText(bossName);
    }
}
