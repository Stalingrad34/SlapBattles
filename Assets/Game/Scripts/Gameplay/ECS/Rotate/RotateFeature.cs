using Game.Scripts.Gameplay.ECS.Rotate.Systems;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Rotate
{
  public class RotateFeature : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
  {
    private EcsWorld _world;
    private EcsSystems _systems;

    public void Init()
    {
      _systems = new EcsSystems(_world);
      _systems
        .Add(new ChangesRotateAxisYSystem())
        .Add(new ControlRotateAxisXSystem())
        .Add(new ControlRotateAxisYSystem())
        .Add(new RotateAxisXSystem())
        .Add(new RotateAxisYSystem())
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