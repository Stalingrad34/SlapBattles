using Game.Scripts.Gameplay.Data.Slaps;
using Game.Scripts.Gameplay.ECS.Slap.Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.ECS.Slap.Converters
{
  public class SlapConverter : MonoBehaviour, IConvertToEntity
  {
    [SerializeField] private SlapView slapView;
    [SerializeField] private Transform slapPoint;
    [SerializeField] private float reloadTime;
    [SerializeField] private float punchTime;
    [SerializeField] private float punchForce;
    [SerializeField] private float sphereRadius;
    
    public void Convert(EcsEntity entity)
    {
      ref var component = ref entity.Get<SlapComponent>();
      component.SlapView = slapView;
      component.SlapPoint = slapPoint;
      component.ReloadTime = reloadTime;
      component.PunchTime = punchTime;
      component.PunchForce = punchForce;
      component.SphereRadius = sphereRadius;
      component.CanStartSlap = true;
    }

    private void OnDrawGizmos()
    {
      Gizmos.color = Color.chartreuse;
      Gizmos.DrawWireSphere(slapPoint.position, sphereRadius);
    }
  }
}