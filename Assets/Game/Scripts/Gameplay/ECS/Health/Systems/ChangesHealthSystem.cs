using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Health.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Health.Systems
{
  public class ChangesHealthSystem : IEcsRunSystem
  {
    private EcsFilter<IdentifierComponent, HealthComponent> _filter;
    private EcsFilter<PlayerChangeEvent> _eventFilter;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        foreach (var ii in _filter)
        {
          if (_filter.Get1(ii).Id != _eventFilter.Get1(i).Id)
            continue;
          
          foreach (var change in _eventFilter.Get1(i).Changes)
          {
            switch (change.Field)
            {
              case "currentHealth":
                _filter.Get2(ii).CurrentHealth = (ushort) change.Value;
                break;
            }
          }
        }
      }
    }
  }
}