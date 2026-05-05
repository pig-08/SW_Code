using UnityEngine;
using UnityEngine.UI;

namespace PSW.Code.GameOver
{
    public abstract class GameOverSlotCompo : MonoBehaviour
    {
        [SerializeField] protected Image icon;

        public void Init(Sprite iconImage)
        {
            icon.sprite = iconImage;
        }
    }
}