using DG.Tweening;
using UnityEngine;

public abstract class SettingCompo : MonoBehaviour
{
    public virtual void Init()
    {

    }
    public virtual void Open()
    {
        transform.DOScale(new Vector2(0.3f, 0.3f), 0.2f);
    }
    public virtual void Close()
    {
        transform.DOScale(new Vector2(0f, 0f), 0.2f);
    }
}
