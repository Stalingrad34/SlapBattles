using Game.Scripts.Gameplay.ECS.Portal.Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game.Scripts.Gameplay.Data.Environments
{
  public class PortalView : MonoBehaviour
  {
    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent<ConvertToEntity>(out var convert))
      {
        var entity = convert.TryGetEntity();
        entity?.Get<ArenaGameTeleportEvent>();
      }
    }
  }
}