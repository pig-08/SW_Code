
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class KrakenSkill : CharacterSkill
{
    protected override void AwakePlayer()
    {
        _player.GetComponentInChildren<DamageCaster>().OnShoot += ColorChange;
    }

    private void ColorChange(bool isTrigger)
    {
        if (_health.IsInvincibility) return;
        _health.IsInvincibility = true;
        _player.PlayerSpriteRenderer.DOColor(Color.HSVToRGB(0.55f, 0.46f, 1f), 0.2f);
        _player.MaskSpriteRenderer.DOColor(Color.HSVToRGB(0.55f, 0.46f, 1f), 0.2f);
        StartCoroutine(ColorTime());
    }

    private IEnumerator ColorTime()
    {
        yield return new WaitForSeconds(1);
        _player.PlayerSpriteRenderer.DOColor(Color.HSVToRGB(0f, 0f, 1f), 0.2f);
        _player.MaskSpriteRenderer.DOColor(Color.HSVToRGB(0f, 0f, 1f), 0.2f);
        _health.IsInvincibility = false;
    }

    private void OnDisable()
    {
        _player.GetComponentInChildren<DamageCaster>().OnShoot -= ColorChange;
    }
}
