using Game.Scripts.Gameplay.ECS.Jump.Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Converters
{
  public class JumpableConverter : MonoBehaviour, IConvertToEntity
  {
    [SerializeField] private float jumpForce;
    [SerializeField] private float checkSpereRadius;
    [SerializeField] private LayerMask checkSpereMask;
    [SerializeField] private float lastChanceJumpTime;
    [SerializeField] private float intervalJumpTime;
    
    public void Convert(EcsEntity entity)
    {
      ref var jumpable = ref entity.Get<JumpableComponent>();
      jumpable.JumpForce = jumpForce;
      jumpable.CheckSphereRadius = checkSpereRadius;
      jumpable.CheckSphereMask = checkSpereMask;
      jumpable.LastChanceJumpTime = lastChanceJumpTime;
      jumpable.IntervalJumpTime = intervalJumpTime;
      jumpable.IntervalJumpTimer = intervalJumpTime;
    }
  }
}