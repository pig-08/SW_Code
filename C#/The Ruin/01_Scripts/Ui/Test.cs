using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    [SerializeField] private string nextScene;
    private bool isTextLoedEnd;

    private void Start()
    {
        DOVirtual.DelayedCall(2f, () => isTextLoedEnd = true);
    }

    public void TextTest(string text)
    {
        print(text);

    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && isTextLoedEnd)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    public void AniEnd()
    {
        isTextLoedEnd = true;
    }


}
