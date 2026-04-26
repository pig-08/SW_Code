using UnityEngine;

public class SkinManager : MonoSingleton<SkinManager>
{
    private SkinDataSO _choiceSkinDataSO; //�̰� ���������Ȱ���
    public SkinDataSO GetChoiceSkinDataSO() => _choiceSkinDataSO;
    public void SetChoiceSkinDataSO(SkinDataSO dataSo) => _choiceSkinDataSO = dataSo;


}
