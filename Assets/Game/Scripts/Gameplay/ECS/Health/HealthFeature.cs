using Game.Scripts.Gameplay.ECS.Health.Systems;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Health
{
  public class HealthFeature : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
  {
    private EcsWorld _world;
    private EcsSystems _systems;
    private Camera _mainCamera;

    public void Init()
    {
      _systems = new EcsSystems(_world);
      _systems
        .Add(new ChangesHealthSystem())
        .Add(new HealthDamageSystem())
        .Add(new HealthHUDSystem())
        .Add(new HealthDeadSystem())
        .Inject(_mainCamera)
        .Init();
    }

    public void Run()
    {
      _systems?.Run();
    }

    public void Destroy()
    {
      _systems?.Destroy();
    }
  }
}