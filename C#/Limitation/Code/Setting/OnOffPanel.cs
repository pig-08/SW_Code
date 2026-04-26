using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OnOffPanel : SettingCompo
{
    private bool _isppOn = true;
    private bool _isShakeOn = true;

    public void PPButtonOnOff(Image ppButtonImage)
    {
        _isppOn = !_isppOn;
        if (_isppOn)
            ppButtonImage.DOFade(1, 0.2f);
        else
            ppButtonImage.DOFade(0.1f, 0.2f);

    }

    public void ShakeButtonOnOff(Image shakeButtonImage)
    {
        _isShakeOn = !_isShakeOn;
        if (_isShakeOn)
            shakeButtonImage.DOFade(1, 0.2f);
        else
            shakeButtonImage.DOFade(0.1f, 0.2f);
    }
}
