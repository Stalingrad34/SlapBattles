using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Input.Components;
using Game.Scripts.Gameplay.ECS.Portal.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Portal.Systems
{
  public class ArenaTeleportSystem : IEcsRunSystem
  {
    private EcsFilter<ControlComponent, ArenaGameTeleportEvent> _eventFilter;
    private EcsFilter<LobbyGameStateComponent> _stateFilter;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        _stateFilter.Get1(0).LobbyState.ArenaTeleport();
      }
    }
  }
}