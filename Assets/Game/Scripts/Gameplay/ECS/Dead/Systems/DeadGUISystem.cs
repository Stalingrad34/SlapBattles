using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Dead.Components;
using Game.Scripts.Gameplay.ECS.Input.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Dead.Systems
{
  public class DeadGUISystem : IEcsRunSystem
  {
    private EcsFilter<ControlComponent, TransformComponent, DeadEvent> _deadFilter;
    private EcsFilter<ArenaGameStateComponent> _gameSessionFilter;

    public void Run()
    {
      foreach (var i in _deadFilter)
      {
        _gameSessionFilter.Get1(0).GameState.SendRestartMessage(_deadFilter.Get2(i).Transform).Forget();
      }
    }
  }
}