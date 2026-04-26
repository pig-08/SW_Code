using Gwamegi.Code.Core.EventSystems;
using Gwamegi.Code.Core.SaveSystem;
using Gwamegi.Code.Managers;
using System;
using TMPro;
using UnityEngine;

namespace SW.Code.Managers
{
    public class CoinDataManager : MonoBehaviour, IManager
    {
        [SerializeField] private int startCoin;
        [SerializeField] private int startSpecialCoin;

        [SerializeField] private TextMeshProUGUI _coin;
        [SerializeField] private TextMeshProUGUI _specialCoin;

        [SerializeField] private GameEventChannelSO saveEventChannel;

        private int coin;
        private int specialCoin;

        private Manager _manager;

        public static CoinDataManager Instance { get; set; }

        public void Initailze(Manager manager)
        {
            _manager = manager;

            coin = startCoin;
            SetCoinCount();
            //oin = SaveEvents.DataLoadEvent.loadData.coin;

            saveEventChannel.AddListener<DataLoadEvent>(OnDataLoadEvent);
            SetCoinCount();
        }

        private void OnDataLoadEvent(DataLoadEvent evt)
        {
            specialCoin = evt.loadData.specialCoin;
            SetCoinCount();
        }

        public void PlusCoin(int plus, bool isSpecial)
        {
            if (isSpecial)
                specialCoin += plus;
            else
                coin += plus;

            SetCoinCount();
        }

        public void MinusCoin(int minus, bool isSpecial)
        {
            if (isSpecial)
                specialCoin -= minus;
            else
                coin -= minus;

            SetCoinCount();
        }


        private void SetCoinCount()
        {
            _coin.text = "<color=yellow>Coin</color>:" + coin.ToString();
            _specialCoin.text = "<color=#ff00ffff>Special Coin</color>:" + specialCoin.ToString();
        }

        public int GetCoin() => coin;
        public int GetSpecialCoin() => specialCoin;

    }
}