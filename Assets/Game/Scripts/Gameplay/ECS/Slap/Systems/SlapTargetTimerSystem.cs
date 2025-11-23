using Game.Scripts.Gameplay.ECS.Slap.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Slap.Systems
{
  public class SlapTargetTimerSystem : IEcsRunSystem
  {
    private EcsFilter<SlapComponent> _filter;
    private EcsWorld _world;
    
    public void Run()
    {
      foreach (var i in _filter)
      {
        if (_filter.Get1(i).PunchTimer > 0)
        {
          _filter.Get1(i).PunchTimer -= Time.deltaTime;
          continue;
        }

        if (_filter.Get1(i).CanPunch)
        {
          _filter.Get1(i).CanPunch = false;
          _filter.GetEntity(i).Get<SlapTargetEvent>();
        }
      }
    }
  }
}