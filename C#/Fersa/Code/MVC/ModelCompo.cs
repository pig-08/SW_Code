using UnityEngine;

public abstract class ModelCompo<T> : MonoBehaviour
{
    protected T _currentData;
    public abstract T InitModel();
    public virtual T SetModelData(T data)
    {
        _currentData = data;
        return _currentData;
    }

    public virtual T GetModelData() => _currentData;
}
