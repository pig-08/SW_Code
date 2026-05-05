using PSB.Code.CoreSystem.Events;
using PSW.Code.EventBus;
using UnityEngine;
using Work.PSB.Code.FieldCode.MapSaves;

namespace PSW.MiniGame.CupShuffle
{
    public class CupShuffleGameEnd : MonoBehaviour
    {
        [SerializeField] private NormalPortalEntity normalPortalEntity;

        private void OnEnable()
        {
            Bus<ShuffleGameEnd>.OnEvent += HandleTalkEnd;
        }

        private void OnDisable()
        {
            Bus<ShuffleGameEnd>.OnEvent -= HandleTalkEnd;
        }

        private void HandleTalkEnd(ShuffleGameEnd evt)
        {
            normalPortalEntity.Open();
        }
    }

    public struct ShuffleGameEnd : IEvent {}
}