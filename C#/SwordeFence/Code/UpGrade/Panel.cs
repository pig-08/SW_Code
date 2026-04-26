using Gwamegi.Code.Input;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] private GameInputSO gameInputSO;

    [SerializeField] private RectTransform _myRect;
    private Vector2 _offset;
    private bool isCanMove;

    public void MouseDownEvent()
    {
        //오프셋 확인해서 해당 위치값 확인
        isCanMove = true;
        _offset = gameInputSO.mousePosition - _myRect.anchoredPosition;
    }


    public void MouseUpEvent()
    {
        //움직임 막기
        isCanMove = false;
    }

    public void Update()
    {
        if (isCanMove)
        {
            //Vector2 mousePos = Camera.main.ScreenToWorldPoint(gameInputSO.mousePosition);
            Vector2 pos = gameInputSO.mousePosition - _offset;

            //현재 스크린의 사이즈 - 현재 패널의 사이즈 / 2 -> max   
            //현재 패널의 사이즈 / 2 -> min

            pos = new Vector2(
                Mathf.Clamp(pos.x, _myRect.sizeDelta.x / 2, 1920 - _myRect.sizeDelta.x / 2),
                Mathf.Clamp(pos.y, _myRect.sizeDelta.y / 2, 1080 - _myRect.sizeDelta.y / 2));

            _myRect.anchoredPosition = pos;
        }
    }
}
