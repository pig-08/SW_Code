using UnityEngine;

public class SettingButton : MonoBehaviour
{
    [SerializeField] private SettingPanel settigPanel;
    [SerializeField] private UiInputSO inputSO;
    private Animator _animator;

    private bool isClick;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        inputSO.OnESCEvent += SetButton;
    }

    private void OnDestroy()
    {
        inputSO.OnESCEvent -= SetButton;
    }

    public void SetButton()
    {
        isClick = !isClick;
        settigPanel.SetPanel(isClick);

        if (isClick)
            _animator.Play("ButtonClick");
        else
            _animator.Play("ButtonClickBack");
    }



}
