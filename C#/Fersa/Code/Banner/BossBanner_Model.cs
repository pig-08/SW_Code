using UnityEngine;
using Work.PSB.Code.FieldCode;

public class BossBanner_Model : ModelCompo<string>
{
    [SerializeField] private string[] bannerCilpNameList = new string[2];
    [SerializeField] private float delayTime;
    [SerializeField] private float startDelayTime = 0.3f;

    public override string InitModel()
    {
        return "";
    }
    public string GetCilpName(bool isUp) => isUp ? bannerCilpNameList[0] : bannerCilpNameList[1];
    public float GetDelayTime() => delayTime;
    public float GetStartDelayTime() => startDelayTime;
}
