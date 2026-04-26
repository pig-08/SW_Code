using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCanvas : MonoBehaviour
{
    [SerializeField] private Transform initPanelTrm;

    private bool _isInitPanelOn;
    private bool _isPerfectionOpne = true;

    public void PickUP(Transform buttonTrm)
    {
        buttonTrm.DOScale(new Vector3(1.8f, 1.8f, 1.8f), 0.2f);
    }

    public void PickDown(Transform buttonTrm)
    {
        buttonTrm.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.2f);
    }

    public void MoveScenes(string scene)
    {
        if (_isInitPanelOn == false)
            SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        if (_isInitPanelOn == false)
            Application.Quit();
    }

    public async void InitPanlePop()
    {
        if (_isPerfectionOpne == false) return;

        _isPerfectionOpne = false;

        if (_isInitPanelOn == false)
        {
            initPanelTrm.DOScale(Vector3.one, 0.3f);
            _isInitPanelOn = true;
        }
        else
        {
            initPanelTrm.DOScale(Vector3.zero, 0.3f);
            _isInitPanelOn = false;
        }

        await Awaitable.WaitForSecondsAsync(0.3f);
        _isPerfectionOpne = true;
    }

    public void ResetData()
    {
        PlayerPrefs.SetInt("CheckPointIndex", 0);
    }
}


