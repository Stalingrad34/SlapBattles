using Game.Scripts.Gameplay.ECS.Dead.Components;
using Game.Scripts.Gameplay.ECS.Dead.Systems;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Dead
{
  public class DeadFeature : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
  {
    private EcsWorld _world;
    private EcsSystems _systems;

    public void Init()
    {
      _systems = new EcsSystems(_world);
      _systems
        .Add(new DeadGUISystem())
        .Add(new AnimationDeadSystem())
        .Add(new DestroyGameObjectSystem())
        .Add(new DestroySystem())
        .OneFrame<DestroyGameObjectEvent>()
        .OneFrame<DeadEvent>()
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