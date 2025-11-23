using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Rotate.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Rotate.Systems
{
  public class ChangesRotateAxisYSystem : IEcsRunSystem
  {
    private EcsFilter<IdentifierComponent, ServerPlayerComponent, RotateAxisYComponent> _serverPlayers;
    private EcsFilter<ServerRotationChanges> _eventFilter;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        foreach (var ii in _serverPlayers)
        {
          if (_serverPlayers.Get1(ii).Id != _eventFilter.Get1(i).Id)
            continue;
          
          _serverPlayers.Get3(ii).Angle = _eventFilter.Get1(i).RotationY;
        }
      }
    }
  }
}