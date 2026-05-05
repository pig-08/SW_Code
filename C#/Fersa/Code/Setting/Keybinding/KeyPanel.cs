using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyNameText;
    [SerializeField] private TextMeshProUGUI inputKeyName;
    [SerializeField] private string failText = "중복된 키입니다.";
    [SerializeField] private float failEffectTime = 0.2f;

    private string _keyValue;
    private string _key;
    private int _keyIndex = -1;
    private KeybindingInputSO _currentInput;
    private Coroutine _coroutine;

    public void Init(string keyName, InputAction keyValue, KeybindingInputSO inputSO)
    {
        _currentInput = inputSO;
        _keyValue = keyValue.name;
        _key = keyName;
        keyNameText.SetText(_key);

        _currentInput.OnChangeKey += ChangeActionKey;
        _currentInput.OnFail += Fail;

        if (_keyIndex >= 0)
            _key = keyValue.GetBindingDisplayString(_keyIndex);
        else
            _key = keyValue.GetBindingDisplayString();

        inputKeyName.SetText(_key);
    }

    private void OnDestroy()
    {
        _currentInput.OnChangeKey -= ChangeActionKey;
        _currentInput.OnFail -= Fail;
    }

    private void ChangeActionKey(string value, int index,string key)
    {
        if (_keyValue == value && index == _keyIndex)
        {
            inputKeyName.SetText(key);
            _key = key;
        }
    }

    private void Fail(string value, int index)
    {
        if (_keyValue == value && index == _keyIndex)
        {
            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
            
            inputKeyName.SetText(_key);
            _coroutine = StartCoroutine(FailTime());
        }
    }

    private IEnumerator FailTime()
    {
        inputKeyName.SetText(failText);
        yield return new WaitForSeconds(failEffectTime);
        inputKeyName.SetText(_key);
        _coroutine = null;
    }

    public void InitIndex(int keyIndex)
    {
        _keyIndex = keyIndex;
    }

    public void ButtonClick()
    {
        _currentInput.StartRebinding(_keyValue, _keyIndex);
    }
}
