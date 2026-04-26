using UnityEngine;

public abstract class ItemComponent : MonoBehaviour
{
    [field:SerializeField] public Sprite ItemIcon { get; private set; }
    [SerializeField] private SoundSo itemSound;
    protected bool isPickup;

    public bool IsDisposable { get; protected set; }
    public int ItemCount { get; protected set; }

    public abstract void Init(Player player);

    public virtual void PickUp()
    {
        isPickup = true;
        SoundManager.sound.PlaySound(itemSound);
    }

    public virtual void PickDown()
    {
        isPickup = false;
    }

    public bool GetZeroCount()
    {
        return 0 < ItemCount;
    }

}
