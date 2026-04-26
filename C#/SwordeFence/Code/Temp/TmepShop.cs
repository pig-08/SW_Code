using UnityEngine;

namespace SW.Code.Temp
{
    public class TmepShop : MonoBehaviour
    {
        [SerializeField] private GameObject shopPanel;

        private void Awake()
        {
            shopPanel.SetActive(true);
            shopPanel.GetComponent<TempShopPanel>().Init();
        }

    }
}