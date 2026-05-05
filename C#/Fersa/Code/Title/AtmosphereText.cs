using PSW.Code.Text;
using System.Collections;
using TMPro;
using UnityEngine;

public class AtmosphereText : MonoBehaviour
{
    [SerializeField] private string[] texts;
    [SerializeField] private float delayTime;
    [SerializeField] private TextMeshProUGUI text;

    private WaitForSeconds _wait;
    private int _index = 0;
    private bool _isPlus = true;

    private void Start()
    {
        _wait = new WaitForSeconds(delayTime);
        StartCoroutine(SetText());
    }

    private IEnumerator SetText()
    {
        text.SetText(texts[_index]);

        if(_isPlus)
        {
            if (_index + 1 >= texts.Length)
                _isPlus = false;
            else
                _index++;
        }
        else
        {
            if (_index - 1 < 0)
                _isPlus = true;
            else
                _index--;
        }

        yield return _wait;
        StartCoroutine(SetText());
    }
}
