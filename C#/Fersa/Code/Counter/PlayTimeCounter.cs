using UnityEngine;

public class PlayTimeCounter : MonoBehaviour
{
    public static PlayTimeCounter Instance { get; private set; }

    private float _checkStartTime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        StartPlayTime();
    }

    public void StartPlayTime()
    {
        _checkStartTime = Time.time;
    }

    public int GetCurrentTime()
    {
        return Mathf.RoundToInt(Time.time - _checkStartTime);
    }
}
