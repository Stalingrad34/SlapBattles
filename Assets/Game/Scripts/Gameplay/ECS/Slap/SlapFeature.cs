using Game.Scripts.Gameplay.ECS.Slap.Components;
using Game.Scripts.Gameplay.ECS.Slap.Systems;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Slap
{
  public class SlapFeature : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
  {
    private EcsWorld _world;
    private EcsSystems _systems;

    public void Init()
    {
      _systems = new EcsSystems(_world);
      _systems
        .Add(new MessageStartSlapSystem())
        .Add(new ControlStartSlapSystem())
        .Add(new ReloadStartSlapSystem())
        .Add(new StartSlapSystem())
        .Add(new SlapTargetTimerSystem())
        .Add(new SlapPunchSystem())
        .OneFrame<StartSlapMessage>()
        .OneFrame<StartSlapEvent>()
        .OneFrame<SlapPunchEvent>()
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