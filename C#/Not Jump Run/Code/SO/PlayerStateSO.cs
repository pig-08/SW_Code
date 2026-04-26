using UnityEngine;

[CreateAssetMenu(fileName ="StateSO",menuName ="SO/State/StateData")]
public class PlayerStateSO : ScriptableObject
{
    public string stateName;
    public string className;
    public string animPatamName;

    public int animayionHash;

    private void OnValidate()
    {
        animayionHash = Animator.StringToHash(animPatamName);
    }

    private void OnEnable()
    {
        animayionHash = Animator.StringToHash(animPatamName);
    }
}
