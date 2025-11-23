using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Damage.Components;
using Game.Scripts.Gameplay.ECS.Health.Components;
using Game.Scripts.Multiplayer;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Health.Systems
{
  public class HealthDamageSystem : IEcsRunSystem
  {
    private EcsFilter<IdentifierComponent, HealthComponent, DamageEvent> _eventFilter;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        var currentHp = Mathf.Max(_eventFilter.Get2(i).CurrentHealth - _eventFilter.Get3(i).Damage, 0);
        _eventFilter.Get2(i).CurrentHealth = currentHp;
        MultiplayerManager.Instance.SendDamageMessage(_eventFilter.Get1(i).Id, _eventFilter.Get3(i).Damage);
      }
    }
  }
}