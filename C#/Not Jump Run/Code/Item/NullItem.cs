using UnityEngine;

public class NullItem : ItemComponent
{
    private Player _player;
    public override void Init(Player player)
    {
        _player = player;
    }

}
