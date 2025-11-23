using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.SendMessage.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.SendMessage.Systems
{
  public class SendMoveSystem : IEcsRunSystem
  {
    private EcsFilter<SendDataComponent, TransformComponent> _filter;
    
    public void Run()
    {
      foreach (var i in _filter)
      {
        var transform = _filter.Get2(i).Transform;
        _filter.Get1(i).SendData["positionX"] = transform.position.x;
        _filter.Get1(i).SendData["positionY"] = transform.position.y;
        _filter.Get1(i).SendData["positionZ"] = transform.position.z;
      }
    }
  }
}