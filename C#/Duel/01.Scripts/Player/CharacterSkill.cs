using UnityEngine;

public abstract class CharacterSkill : MonoBehaviour, IPlayerComponents
{
    protected FeedbackPlayer eventFeedbacks;
    protected bool isSkillStart;
    protected Player _player;
    protected StatData _stat;
    protected Health _health;
    public void Initialize(Player player)
    {
        _player = player;
        _stat = _player.StatDataCompo;
        _health = _player.GetCompo<Health>();
        eventFeedbacks = _player.EventFeedbacksCompo;
        
        AwakePlayer();
    }

    public virtual void ActiveSkill()
    {
        //인풋에 구독을 하고 실행했을때 -= 하기
        if (isSkillStart) return;
        isSkillStart = true;
    }

    protected virtual void UpdateCharacterSkill()
    {

    }

    protected virtual void AwakePlayer()
    {
        
    }
    private void Update()
    {
        UpdateCharacterSkill();
    }
}
