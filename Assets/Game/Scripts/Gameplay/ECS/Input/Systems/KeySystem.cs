using Game.Scripts.Gameplay.ECS.Input.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Input.Systems
{
  public class KeySystem : IEcsRunSystem
  {
    private EcsFilter<ControlComponent> _filter;
    
    public void Run()
    {
      foreach (var i in _filter)
      {
        _filter.Get1(i).SpaceKeyDown = UnityEngine.Input.GetKeyDown(KeyCode.Space);
        _filter.Get1(i).CtrlKeyDown = UnityEngine.Input.GetKey(KeyCode.LeftControl);
        _filter.Get1(i).Alpha1KeyDown = UnityEngine.Input.GetKey(KeyCode.Alpha1);
        _filter.Get1(i).Alpha2KeyDown = UnityEngine.Input.GetKey(KeyCode.Alpha2);
      }
    }
  }
}