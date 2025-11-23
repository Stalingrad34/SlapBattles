using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Move.Components;
using Game.Scripts.Multiplayer;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Move.Systems
{
  public class ChangesMoveSystem : IEcsRunSystem
  {
    private EcsFilter<IdentifierComponent, ServerPlayerComponent, MoveTowardsComponent> _serverPlayers;
    private EcsFilter<ServerPositionChanges> _eventFilter;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        foreach (var ii in _serverPlayers)
        {
          if (_serverPlayers.Get1(ii).Id != _eventFilter.Get1(i).Id)
            continue;
          
          var serverPosition = _eventFilter.Get1(i).Position;
          _serverPlayers.Get3(ii).Destination = serverPosition.ToVector3();
        }
      }
    }
  }
}