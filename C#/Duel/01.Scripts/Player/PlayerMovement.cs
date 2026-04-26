using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour, IPlayerComponents
{
    public event Action OnMove;
    public event Action OnEndMove;
    
    private Player _player;
    private MapInfo _mapInfo;
    [SerializeField] private Tilemap moveTile;
    public bool IsMove { get; set; }
    
    public void Initialize(Player player)
    {
        _player = player;
        _mapInfo = new MapInfo(FindFirstObjectByType<Tilemap>());
    }

    public void SetMovement(Vector2Int moveDir)
    {
        if (IsMove) return;

        Vector2Int v = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        Vector2Int wantTilePos = v + moveDir;
        if (!_mapInfo.CanMove(wantTilePos)) return;
        IsMove = true;
        
        transform.DOMove(_mapInfo.GetCellCenterToWorld(wantTilePos), 0.14f).
            SetEase(Ease.OutExpo).
            OnStart(() => OnMove?.Invoke()).
            OnComplete(() =>
            {
                if(_player.StatDataCompo != null)
                    _player.StatDataCompo.CurLoadCount++;
                OnEndMove?.Invoke();
                IsMove = false;
            });
    }
}
