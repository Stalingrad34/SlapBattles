using Game.Scripts.Gameplay.ECS.Input.Systems;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Input
{
  public class InputFeature : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
  {
    private EcsWorld _world;
    private EcsSystems _systems;

    public void Init()
    {
      _systems = new EcsSystems(_world);
      _systems
        .Add(new AxisSystem())
        .Add(new MouseSystem())
        .Add(new KeySystem())
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