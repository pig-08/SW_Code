using UnityEngine;
using UnityEngine.UIElements;

public class GunDataUiSet : MonoBehaviour
{
    [SerializeField] private UIDocument DataUiPanel;
    private VisualElement _root;
    private VisualElement[] _gunData = new VisualElement[2];
    private VisualElement[] _visualImages = new VisualElement[2];
    private Label[] leftGunLabel = new Label[6];
    private Label[] rightGunLabel = new Label[6];

    private void OnEnable()
    {
        _root = DataUiPanel.rootVisualElement;
        _gunData[0] = _root.Q<VisualElement>("LeftGunData");
        _gunData[1] = _root.Q<VisualElement>("RightGunData");
        _visualImages[0] = _gunData[0].Q<VisualElement>("VisualImage");
        _visualImages[1] = _gunData[1].Q<VisualElement>("VisualImage");
        LabelSet(ref leftGunLabel, 0);
        LabelSet(ref rightGunLabel, 1);
    }

    private void LabelSet(ref Label[] labels, int number)
    {
        labels[0] = _gunData[number].Q<Label>("Name");
        labels[1] = _gunData[number].Q<Label>("Damage");
        labels[2] = _gunData[number].Q<Label>("MaxBullet");
        labels[3] = _gunData[number].Q<Label>("WantLoad");
        labels[4] = _gunData[number].Q<Label>("Range");
        labels[5] = _gunData[number].Q<Label>("Description");
    }

    public void UiSet(GunDataSO gunDataSO, bool isLeft)
    {
        if (!isLeft)
        {
            _visualImages[0].style.backgroundImage = new StyleBackground(gunDataSO.itemSprite);
            leftGunLabel[0].text = $"< {gunDataSO.itemName} >";
            leftGunLabel[1].text = $"Damage {gunDataSO.damage.ToString()}";
            leftGunLabel[2].text = $"MaxBullet : {gunDataSO.bulletCount.ToString()}";
            leftGunLabel[3].text = $"WantLoad : {gunDataSO.wantLoadCount.ToString()}";
            leftGunLabel[4].text = $"Range : {gunDataSO.range.ToString()}";
            leftGunLabel[5].text = gunDataSO.explanation;
        }
        else
        {
            _visualImages[1].style.backgroundImage = new StyleBackground(gunDataSO.itemSprite);
            rightGunLabel[0].text = $"< {gunDataSO.itemName} >";
            rightGunLabel[1].text = $"Damage {gunDataSO.damage.ToString()}";
            rightGunLabel[2].text = $"MaxBullet : {gunDataSO.bulletCount.ToString()}";
            rightGunLabel[3].text = $"WantLoad : {gunDataSO.wantLoadCount.ToString()}";
            rightGunLabel[4].text = $"Range : {gunDataSO.range.ToString()}";
            rightGunLabel[5].text = gunDataSO.explanation;
        }
    }
}
