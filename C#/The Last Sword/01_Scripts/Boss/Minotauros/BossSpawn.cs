using Gwamegi.Code.Entities;
using Gwamegi.Code.Players;
using UnityEngine;
using UnityEngine.Playables;

namespace SW.Code.Boss
{
    public class BossSpawn : MonoBehaviour
    {
        [SerializeField] private EntityFinderSO player;
        private PlayableDirector timeLinePlayer;
        private BoxCollider2D boxCollider;

        private void Awake()
        {
            timeLinePlayer = GetComponent<PlayableDirector>();
            boxCollider = GetComponent<BoxCollider2D>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (player.target.gameObject == collision.gameObject)
            {
                timeLinePlayer.Play();
                EntityMover mover = player.target.GetCompo<EntityMover>();
                mover.CanManualMove = false;
                mover.StopImmediately(false);
                Destroy(boxCollider);
            }
        }
    }
}