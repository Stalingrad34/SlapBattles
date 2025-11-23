using Game.Scripts.Gameplay.ECS.Jump.Systems;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Jump
{
  public class JumpFeature : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
  {
    private EcsWorld _world;
    private EcsSystems _systems;

    public void Init()
    {
      _systems = new EcsSystems(_world);
      _systems
        .Add(new OnGroundSystem())
        .Add(new OutGroundSystem())
        .Add(new CheckSphereGroundSystem())
        .Add(new KeyJumpSystem())
        .Add(new JumpAnimationSystem())
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