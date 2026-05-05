public interface IView<T>
{
    public void Init(T defaultData);
    public void SetData(T data);
}
