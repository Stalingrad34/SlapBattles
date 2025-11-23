using Game.Scripts.Gameplay.ECS.Input.Components;
using Game.Scripts.Gameplay.ECS.Slap.Components;
using Game.Scripts.Multiplayer;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Slap.Systems
{
  public class ControlStartSlapSystem : IEcsRunSystem
  {
    private EcsFilter<ControlComponent, SlapComponent> _filter;
    
    public void Run()
    {
      foreach (var i in _filter)
      {
        if (!_filter.Get1(i).MouseLeftClicked || !_filter.Get2(i).CanStartSlap)
          continue;

        _filter.Get2(i).ReloadTimer = _filter.Get2(i).ReloadTime;
        _filter.Get2(i).CanStartSlap = false;
        _filter.Get2(i).CanPunch = true;
        _filter.Get2(i).PunchTimer = _filter.Get2(i).PunchTime;
        _filter.GetEntity(i).Get<StartSlapEvent>();
        MultiplayerManager.Instance.SendStartSlapMessage();
      }
    }
  }
}