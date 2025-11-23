using Game.Scripts.Gameplay.Data.Units;
using Game.Scripts.Gameplay.ECS.Health.Components;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Converters
{
  public class HealthConverter : MonoBehaviour, IUnitSetup, IConvertToEntity
  {
    [SerializeField] private Image progressBar;
    [SerializeField] private bool lookAt;
    private int _health;
    
    public void Convert(EcsEntity entity)
    {
      ref var health = ref entity.Get<HealthComponent>();
      health.CurrentHealth = _health;
      health.MaxHealth = _health;
      health.ProgressBar = progressBar;
      health.LookAt = lookAt;
    }

    public void Setup(UnitData data)
    {
      _health = data.Health;
    }
  }
}