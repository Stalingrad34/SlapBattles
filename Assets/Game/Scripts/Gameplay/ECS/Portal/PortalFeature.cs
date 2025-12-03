using Game.Scripts.Gameplay.ECS.Portal.Components;
using Game.Scripts.Gameplay.ECS.Portal.Systems;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Portal
{
  public class PortalFeature : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
  {
    private EcsWorld _world;
    private EcsSystems _systems;

    public void Init()
    {
      _systems = new EcsSystems(_world);
      _systems
        .Add(new LobbyTeleportSystem())
        .Add(new ArenaTeleportSystem())
        .OneFrame<LobbyTeleportEvent>()
        .OneFrame<ArenaGameTeleportEvent>()
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