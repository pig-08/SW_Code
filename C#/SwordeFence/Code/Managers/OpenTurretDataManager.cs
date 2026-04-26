using Gwamegi.Code.Core.EventSystems;
using Gwamegi.Code.Core.SaveSystem;
using Gwamegi.Code.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace SW.Code.Managers
{
    public class OpenTurretDataManager : MonoBehaviour, IManager
    {
        [SerializeField] private GameEventChannelSO _saveEventChannel;

        private Manager _manager;
        private List<int> _idDataList = new List<int>();

        public void Initailze(Manager manager)
        {
            _manager = manager;
            _saveEventChannel.AddListener<DataLoadEvent>(GetIdList);
        }

        private void GetIdList(DataLoadEvent dataLoadEvent)
        {
            _idDataList = dataLoadEvent.loadData.idList;
        }

        public bool GetOpenTurrent(int id)
        {
            return _idDataList.Contains(id);
        }

    }
}