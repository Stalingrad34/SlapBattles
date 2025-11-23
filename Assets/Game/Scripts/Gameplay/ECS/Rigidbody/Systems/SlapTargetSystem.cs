using Game.Scripts.Gameplay.ECS.Slap.Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Rigidbody.Systems
{
  public class SlapTargetSystem : IEcsRunSystem
  {
    private EcsFilter<SlapComponent, SlapTargetEvent> _eventFilter;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        var position = _eventFilter.Get1(i).SlapPoint.position;
        var radius = _eventFilter.Get1(i).SphereRadius;
        
        var colliders = new Collider[4];
        Physics.OverlapSphereNonAlloc(position, radius, colliders);

        foreach (var collider in colliders)
        {
          if (collider == null)
            continue;

          if (collider.TryGetComponent(out ConvertToEntity converter))
          {
            var entity = converter.TryGetEntity();
            if (entity.HasValue)
            {
              var direction = /*Vector3.up + */_eventFilter.Get1(i).SlapPoint.rotation * Vector3.left;
              entity.Value.Get<SlapPunchEvent>().Direction = direction * _eventFilter.Get1(i).PunchForce;
            }
          }
        }
      }
    }
  }
}