using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    public void SetPanel(bool isOpen)
    {
        if(isOpen)
            _animator.Play("PanelOpen");
        else
            _animator.Play("PanelClose");
    }
}
