using Game.Scripts.Gameplay.ECS.Slap.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Slap.Systems
{
  public class ReloadStartSlapSystem : IEcsRunSystem
  {
    private EcsFilter<SlapComponent> _filter;
    
    public void Run()
    {
      foreach (var i in _filter)
      {
        if (_filter.Get1(i).ReloadTimer > 0)
          _filter.Get1(i).ReloadTimer -= Time.deltaTime;
        else
          _filter.Get1(i).CanStartSlap = true;
      }
    }
  }
}