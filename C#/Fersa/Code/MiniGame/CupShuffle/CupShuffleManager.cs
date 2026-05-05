using DG.Tweening;
using PSB.Code.CoreSystem.Events;
using PSW.Code.EventBus;
using PSW.MiniGame.CupShuffle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CupShuffleManager : MonoBehaviour
{
    [SerializeField] private int shuffleCount;
    [SerializeField] private int shuffleSpeedCount;
    
    [SerializeField] private float shuffleFirstMoveTime;
    [SerializeField] private float shuffleSpeedtMoveTime;
    [SerializeField] private float valueBoxViewTime;

    [SerializeField] private string rewardKey;

    [SerializeField] private float circleRange;
    [SerializeField] private Color circleColor;

    private List<Cup> _boxList;

    private void Awake()
    {
        _boxList = GetComponentsInChildren<Cup>().ToList();
        Bus<TalkFinished>.OnEvent += CupShuffleStart;

        float angleStep = 360f / _boxList.Count;
        float rotateOffset = 90 * Mathf.Deg2Rad;

        for (int i = 0; i < _boxList.Count; ++i)
        {
            float angle = i * (angleStep * Mathf.Deg2Rad) + rotateOffset;

            float x = Mathf.Cos(angle) * circleRange;
            float y = Mathf.Sin(angle) * circleRange;
            Vector3 pos = new Vector3(x, y, 0f);

            _boxList[i].transform.localPosition = pos;
        }
    }

    private void OnDestroy()
    {
        Bus<TalkFinished>.OnEvent -= CupShuffleStart;
    }

    private async void CupShuffleStart(TalkFinished evt)
    {
        if(rewardKey == evt.RewardKey)
        {
            await Awaitable.WaitForSecondsAsync(valueBoxViewTime);

            for (int i = 0; i < _boxList.Count; ++i)
            {
                _boxList[i].SetBox(true);
                _boxList[i].BoxSetTalkEnabled(false);
            }

            StartCoroutine(CupShuffle());
        }
    }

    public IEnumerator CupShuffle()
    {
        int cupOneIndex;
        int cupTwoIndex;

        Vector3 cupOnePos;
        Vector3 cupTwoPos;

        float shuffleMoveTime = shuffleFirstMoveTime;

        for (int i = 0; i < _boxList.Count; ++i)
            _boxList[i].BoxSetTalkEnabled(false);
        
        for (int i = 0; i < shuffleCount; ++i)
        {
            cupOneIndex = Random.Range(0,_boxList.Count);
            cupTwoIndex = Random.Range(0,_boxList.Count);

            if (cupOneIndex == cupTwoIndex)
                continue;

            if(i >= shuffleSpeedCount && shuffleMoveTime == shuffleFirstMoveTime)
                shuffleMoveTime = shuffleSpeedtMoveTime;

            cupOnePos = _boxList[cupOneIndex].transform.localPosition;
            cupTwoPos = _boxList[cupTwoIndex].transform.localPosition;

            bool doneA = false;
            bool doneB = false;

            _boxList[cupTwoIndex].transform.DOLocalMove(cupOnePos, shuffleMoveTime).OnComplete(() => doneA = true);
            _boxList[cupOneIndex].transform.DOLocalMove(cupTwoPos, shuffleMoveTime).OnComplete(() => doneB = true);

            yield return new WaitUntil(() => doneA && doneB);
        }

        for (int i = 0; i < _boxList.Count; ++i)
            _boxList[i].SetUpInteractData();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = circleColor;
        Gizmos.DrawWireSphere(transform.position, circleRange);
    }
}
