using UnityEngine;

public class Test : MonoBehaviour
{
    private CharacterDataUiSet dataUiSet;
    [SerializeField] private CharacterDataSO sO;
    [SerializeField] private CharacterDataSO gun;
    private void Start()
    {
        dataUiSet = GetComponent<CharacterDataUiSet>();
        dataUiSet.UiSet(sO,true);
        dataUiSet.UiSet(gun, false);
    }
}
