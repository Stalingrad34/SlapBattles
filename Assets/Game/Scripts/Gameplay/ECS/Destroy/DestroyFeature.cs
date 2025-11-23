using Game.Scripts.Gameplay.ECS.Destroy.Components;
using Game.Scripts.Gameplay.ECS.Destroy.Systems;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Destroy
{
  public class DestroyFeature : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
  {
    private EcsWorld _world;
    private EcsSystems _systems;

    public void Init()
    {
      _systems = new EcsSystems(_world);
      _systems
        .Add(new CollisionDestroySystem())
        .OneFrame<DestroyEvent>()
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