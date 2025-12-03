using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Slap.Components;
using Game.Scripts.Multiplayer;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Slap.Systems
{
  public class MessageSlapPunchSystem : IEcsRunSystem
  {
    private EcsFilter<IdentifierComponent> _filter;
    private EcsFilter<SlapPunchMessage> _messages;
    
    public void Run()
    {
      foreach (var i in _messages)
      {
        foreach (var ii in _filter)
        {
          if (_messages.Get1(i).SlapInfo.PlayerId != _filter.Get1(ii).Id)
            continue;

          _filter.GetEntity(ii).Get<SlapPunchEvent>().Direction = _messages.Get1(i).SlapInfo.Force.ToVector3();
        }
      }
    }
  }
}