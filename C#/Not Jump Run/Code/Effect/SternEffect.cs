using UnityEngine;

public class SternEffect : MonoBehaviour
{
    private ParticleSystem sternEffect;

    private void Awake()
    {
        sternEffect = GetComponent<ParticleSystem>();
    }

    public void SetEffect(bool isStart)
    {
        if(isStart)
        {
            sternEffect.Play();
        }
        else
        {
            sternEffect.Stop();
        }
    }
}
