using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    [SerializeField] private Player player;
    private List<CheckPoint> _checkPointList = new List<CheckPoint>();

    private void Awake()
    {

        _checkPointList = GetComponentsInChildren<CheckPoint>().ToList();
        _checkPointList.ForEach(v =>
        {
            if(PlayerPrefs.GetInt("CheckPointIndex") < _checkPointList.IndexOf(v))
                v.Init(player, _checkPointList.IndexOf(v));
        });

        player.transform.position = _checkPointList[PlayerPrefs.GetInt("CheckPointIndex")].transform.position;
    }
}
