using DG.Tweening;
using TMPro;
using UnityEngine;

public class TutoriaText : MonoBehaviour
{
    [SerializeField] private Vector2 popUpSize;
    [SerializeField] private LayerMask playerLayer;
    private TextMeshPro _turoriaTextMP;
    private bool IsTextOn;
    private void Awake()
    {
        _turoriaTextMP = GetComponent<TextMeshPro>();
        _turoriaTextMP.DOFade(0,0);
    }

    private void Update()
    {
        if(Physics2D.OverlapBox(transform.position,popUpSize,0,playerLayer))
        {
            IsTextOn = true;
            _turoriaTextMP.DOFade(1f, 0.5f);
            
        }
        else if(IsTextOn)
        {
            IsTextOn = false;
            _turoriaTextMP.DOFade(0, 0.5f);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position,popUpSize);
    }
}
