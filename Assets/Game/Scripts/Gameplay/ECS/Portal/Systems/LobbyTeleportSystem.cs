using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Input.Components;
using Game.Scripts.Gameplay.ECS.Portal.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Portal.Systems
{
  public class LobbyTeleportSystem : IEcsRunSystem
  {
    private EcsFilter<ControlComponent, LobbyTeleportEvent> _eventFilter;
    private EcsFilter<ArenaGameStateComponent> _stateFilter;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        _stateFilter.Get1(0).GameState.LobbyTeleport();
      }
    }
  }
}