using Game.Scripts.Gameplay.ECS.Rigidbody.Components;
using Game.Scripts.Gameplay.ECS.Rigidbody.Systems;
using Game.Scripts.Gameplay.ECS.Slap.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Rigidbody
{
  public class RigidbodyFeature : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
  {
    private EcsWorld _world;
    private EcsSystems _systems;

    public void Init()
    {
      _systems = new EcsSystems(_world);
      _systems
        /*.Add(new MouseAngularVelocitySystem())
        .Add(new ControlMoveVelocitySystem())
        .Add(new MoveVelocitySystem())
        .Add(new AngularVelocitySystem())*/
        .Add(new SlapTargetSystem())
        .Add(new ForceSystem())
        .OneFrame<AddForceEvent>()
        .OneFrame<SlapTargetEvent>()
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