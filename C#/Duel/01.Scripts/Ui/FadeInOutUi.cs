using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class FadeInOutUi : MonoBehaviour
{
    private UIDocument _fadeInOutPenel;
    private VisualElement _root;
    private VisualElement[] _fades = new VisualElement[2];
    [SerializeField] SoundSO sound;

    private void Awake()
    {
        _fadeInOutPenel = GetComponent<UIDocument>();
        _root = _fadeInOutPenel.rootVisualElement;
        _fades[0] = _root.Q<VisualElement>("FadeUp");
        _fades[1] = _root.Q<VisualElement>("FadeDown");
    }

    private void Start()
    {
        GameManager.Instance.OnFadeIn += FadeIn;
        StartCoroutine(FadeOntStart());
    }

    public void FadeIn(int value) => StartCoroutine(FadeInStart(value));

    private IEnumerator FadeInStart(int sceneValue)
    {
        _fadeInOutPenel.sortingOrder = 100;
        yield return null;
        _fades[0].ToggleInClassList("IsMove");
        yield return new WaitForSeconds(0.3f);
        _fades[1].ToggleInClassList("IsMove");
        SoundManager.Instance.PlaySFX(sound);
        yield return new WaitForSeconds(0.6f);
        GameManager.Instance.OnSettingUi = null;
        SceneManager.LoadScene(sceneValue);
    }

    public IEnumerator FadeOntStart()
    {
        yield return new WaitForSeconds(0.3f);
        _fades[0].RemoveFromClassList("IsMove");
        yield return new WaitForSeconds(0.3f);
        _fades[1].RemoveFromClassList("IsMove");
        SoundManager.Instance.PlaySFX(sound);
        yield return new WaitForSeconds(0.4f);
        _fadeInOutPenel.sortingOrder = -1;
    }
}