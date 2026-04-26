using UnityEngine;

public class WinBoss : MonoBehaviour
{
    private Animator winBossAnimator;

    private void Awake()
    {
        winBossAnimator = GetComponent<Animator>();
        winBossAnimator.Play("death");
    }
}
