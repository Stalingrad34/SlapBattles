using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.Data.Units
{
  public class PlayerView : UnitView
  {
    [SerializeField] private Transform cameraPoint;
    [SerializeField] private Rigidbody rigidbodyTest;
    [SerializeField] private Vector3 forceTest;
    [SerializeField] private ForceMode forceMode;
    
    private Vector3 _force;

    public void SetCameraPoint(CameraView mainCamera)
    {
      mainCamera.transform.SetParent(cameraPoint);
      mainCamera.transform.localRotation = Quaternion.identity;
      mainCamera.transform.localPosition = Vector3.zero;
    }

    private void FixedUpdate()
    {
      if (_force != Vector3.zero)
      {
        rigidbodyTest.AddForce(_force,  forceMode);
        _force = Vector3.zero;
      }
    }

    [ContextMenu("Add Force")]
    public void TestForce()
    {
      _force = forceTest;
    }

    /*private void OnCollisionStay(Collision collision)
    {
      foreach (var contact in collision.contacts)
      {
        if (contact.normal.y > 0.45f)
        {
          var entity = entityConverter.TryGetEntity();
          if (entity.HasValue)
          {
            entity.Value.Get<OnCollisionStayEvent>().Collision = collision;
          }
        }
      }
    }

    private void OnCollisionExit(Collision collision)
    {
      var entity = entityConverter.TryGetEntity();
      if (entity.HasValue)
      {
        entity.Value.Get<OnCollisionExitEvent>().Collision = collision;
      }
    }*/
  }
}