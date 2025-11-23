using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Dead.Components;
using Game.Scripts.Gameplay.ECS.Rigidbody.Components;
using Game.Scripts.Gameplay.ECS.Slap.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Slap.Systems
{
  public class SlapPunchSystem : IEcsRunSystem
  {
    private EcsFilter<RigidbodyComponent, SlapPunchEvent> _eventFilter;
    private EcsWorld _world;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        //_eventFilter.GetEntity(i).Get<AddForceEvent>().Force = _eventFilter.Get1(i).Direction;
        var entity = _world.NewEntity();
        entity.Get<RigidbodyComponent>().Rigidbody = _eventFilter.Get1(i).Rigidbody;
        entity.Get<AddForceEvent>().Force = _eventFilter.Get2(i).Direction;
        _eventFilter.GetEntity(i).Get<DeadEvent>();
      }
    }
  }
}