using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndPanel : MonoBehaviour
{
    [SerializeField] private Transform textsTrm;

    private Image _panelImage;

    private void Awake()
    {
        _panelImage = GetComponent<Image>();
    }

    public async void End()
    {
        _panelImage.DOFade(0.8f, 3.5f);
        await Awaitable.WaitForSecondsAsync(3.5f);
        textsTrm.DOLocalMoveY(-28.5f,4.5f);
    }

    public void MoveScenes(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void PickUP(Transform buttonTrm)
    {
        buttonTrm.DOScale(new Vector3(1.8f, 1.8f, 1.8f), 0.2f);
    }

    public void PickDown(Transform buttonTrm)
    {
        buttonTrm.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.2f);
    }
}
