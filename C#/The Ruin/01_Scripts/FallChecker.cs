using DG.Tweening;
using UnityEngine;

public class FallChecker : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Vector2 fallCheckerRange;
    [SerializeField] private Vector2 returnPosition;
    [SerializeField] private int _respawnDamage = 10;

    private void Update()
    {
        Collider2D range = Physics2D.OverlapBox(transform.position, fallCheckerRange, 0, playerLayer);
        if (range == null) return;
        AgentRenderer _agentRenderer = range.GetComponentInChildren<AgentRenderer>();
        range.transform.position = returnPosition;

        if (range.TryGetComponent(out Player player))
        {
            Health health = player.GetCompo<Health>();
            health.CastDamage(_respawnDamage, 0);
        }

        _agentRenderer.SpriteRenderer.DOFade(0, 0);
        _agentRenderer.SpriteRenderer.DOFade(1, 3f);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, fallCheckerRange);
    }
}
