using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Vector2 moveSize;

    private Vector3 _moveDir;
    private Vector3 _movePoint;
    private float _moveX;
    private float _moveY;

    private void Start()
    {
        _moveX = moveSize.x / 2;
        _moveY = moveSize.y / 2;
        NewPoint();
    }

    private void NewPoint()
    {
        _movePoint.x = Random.Range(-_moveX, _moveX);
        _movePoint.y = Random.Range(-_moveY, _moveY);

        _moveDir = _movePoint - transform.position;
        _moveDir.Normalize();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _movePoint) >= 0.1f)
            transform.position += _moveDir * moveSpeed * Time.deltaTime;
        else 
            NewPoint();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector2.zero, moveSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawCube(_movePoint, Vector3.one * 2);
    }
}
