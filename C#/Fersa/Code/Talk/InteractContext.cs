using UnityEngine;

namespace PSW.Code.Talk
{
    public struct InteractContext
    {
        public TalkEntity owner;
        public string talkId;

        public Talk_Controller controller;
        public TalkDataListSO talkData;

        public string targetEnemyId;
        public string rewardKey;

        public bool disableTalkAfterFinished;
        public bool disableObjectAfterFinished;
        public float disableDelay;

        public InteractActionMode actionMode;
        public Vector3 position;
    }
}