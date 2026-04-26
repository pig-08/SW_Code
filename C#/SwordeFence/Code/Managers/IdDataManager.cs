using AJ;
using AJ.Scripts;
using Gwamegi.Code.Managers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SW.Code.Managers
{
    public partial class IdDataManager : MonoBehaviour, IManager
    {
        [SerializeField] private TurretDataListSO turretDataListSO;
        private List<int> _idSpecialOpenDataList;
        private Dictionary<int, int> towers = new Dictionary<int, int>();
        private Manager _manager;


        public void AddSpecialTowerData(int id) => _idSpecialOpenDataList.Add(id);

        public void AddTowerData(int id)
        {
            if (towers.TryGetValue(id, out int value))
            {
                towers[id] += 1;
            }
            else
                towers.Add(id, 1);
            
            Debug.Log($"AddTowerData : {id} / {towers[id]}");
        }



        public List<int> GetIdDataList() => towers.Keys.ToList();

        public List<int> GetIdSpecialOpenDataList() => _idSpecialOpenDataList;

        public int GetAllPurchaseId(List<TurretSO> idList)
        {
            int count = 0;
            foreach (TurretSO turret in idList)
            {
                if (towers.Keys.ToList().Contains(turret.Id) == false)
                    count++;
            }
            return count;
        }

        public void Initailze(Manager manager)
        {
            _manager = manager;
            _idSpecialOpenDataList = new List<int>();
            AddTowerData(0);
            AddTowerData(0);
            AddTowerData(0);
        }
    }
}