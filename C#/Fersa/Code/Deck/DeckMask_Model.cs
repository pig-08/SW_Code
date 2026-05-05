using UnityEngine;

public class DeckMask_Model : ModelCompo<Vector2>
{
    [SerializeField] private SizeData posData;
    [SerializeField] private float popTime;

    private bool _isUp;

    public override Vector2 InitModel()
    {
        _isUp = false;
        return posData.Down;
    }

    public float GetPopTime() => popTime;
    public bool GetIsUp() => _isUp;
    public void SetIsUp(bool isUp) => _isUp = isUp;
}
