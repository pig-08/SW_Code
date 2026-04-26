using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class GameWinUi : MonoBehaviour
{
    [SerializeField] private UIDocument gameMinPanel;
    private VisualElement _root;
    private VisualElement[] _visual = new VisualElement[4];
    private Label _playerName;
    private Button _lobbyButton;
    [SerializeField] private List<Sprite> crownSprites;
    private bool win;
    [SerializeField] private AudioSource source;

    private void Awake()
    {
        _root = gameMinPanel.rootVisualElement;
        _visual[0] = _root.Q<VisualElement>("UpPanel");
        _visual[1] = _root.Q<VisualElement>("DownPanel");
        _visual[2] = _root.Q<VisualElement>("Character");
        _visual[3] = _root.Q<VisualElement>("Crown");
        _playerName = _root.Q<Label>("PlayerName");
        _lobbyButton = _root.Q<Button>("Lobby");

        _lobbyButton.RegisterCallback<ClickEvent>((v) =>
        {
            GameManager.Instance.ResetGame();
            GameManager.Instance.OnFadeIn(1);
        });
        GameManager.Instance.OnFinalWin += WinPanelStart;
    }
    public void WinPanelStart(bool player)
    {
        if (win) return;
        if (!player)
        {
            win=true;
            _visual[3].style.backgroundImage = new StyleBackground(crownSprites[0]);
            _playerName.text = "Blue Win!";
        }
        else
        {
            win = true;
            _visual[3].style.backgroundImage = new StyleBackground(crownSprites[1]);
            _playerName.text = "Red Win!";
        }
        
        source.mute = true;
        StartCoroutine(WinPanelStart());
    }
    private IEnumerator WinPanelStart()
    {
        yield return new WaitForSeconds(1f);
        _visual[0].ToggleInClassList("UpMove");
        yield return new WaitForSeconds(0.3f);
        _visual[1].ToggleInClassList("DownMove");
        yield return new WaitForSeconds(0.5f);
        _visual[2].ToggleInClassList("IsMove");
        yield return new WaitForSeconds(1);
        _visual[3].ToggleInClassList("IsMove");
        yield return new WaitForSeconds(0.8f);
        _playerName.ToggleInClassList("IsMove");
        yield return new WaitForSeconds(0.5f);
        _lobbyButton.ToggleInClassList("IsMove");
    }
}
