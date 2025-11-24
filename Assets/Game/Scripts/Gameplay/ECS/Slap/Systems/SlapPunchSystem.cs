using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Dead.Components;
using Game.Scripts.Gameplay.ECS.Rigidbody.Components;
using Game.Scripts.Gameplay.ECS.Slap.Components;
using Game.Scripts.Multiplayer;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Slap.Systems
{
  public class SlapPunchSystem : IEcsRunSystem
  {
    private EcsFilter<IdentifierComponent, RigidbodyComponent, SlapPunchEvent> _eventFilter;
    private EcsWorld _world;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        var entityForce = _world.NewEntity();
        entityForce.Get<RigidbodyComponent>().Rigidbody = _eventFilter.Get2(i).Rigidbody;
        entityForce.Get<AddForceEvent>().Force = _eventFilter.Get3(i).Direction;
        
        var entityGameObject = _world.NewEntity();
        ref var component = ref entityGameObject.Get<GameObjectComponent>();
        component.Id = _eventFilter.Get1(i).Id;
        component.GameObject = _eventFilter.Get2(i).Rigidbody.gameObject;
        
        _eventFilter.GetEntity(i).Get<DeadEvent>();

        var slapInfo = new SlapPunchInfo()
        {
          playerId = _eventFilter.Get1(i).Id,
          force = new Vector3Float()
          {
            x = _eventFilter.Get3(i).Direction.x,
            y = _eventFilter.Get3(i).Direction.y,
            z = _eventFilter.Get3(i).Direction.z,
          }
        };
        
        MultiplayerManager.Instance.SendSlapPunchMessage(slapInfo);
      }
    }
  }
}