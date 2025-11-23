using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.SendMessage.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.SendMessage.Systems
{
  public class SendRotateAxisYDataSystem : IEcsRunSystem
  {
    private EcsFilter<SendDataComponent, TransformComponent> _filter;
    
    public void Run()
    {
      foreach (var i in _filter)
      {
        _filter.Get1(i).SendData["rotationY"] = _filter.Get2(i).Transform.localEulerAngles.y;
      }
    }
  }
}