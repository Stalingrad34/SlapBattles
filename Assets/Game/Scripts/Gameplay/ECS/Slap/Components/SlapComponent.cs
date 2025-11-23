using Game.Scripts.Gameplay.Data.Slaps;
using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Slap.Components
{
  public struct SlapComponent
  {
    public SlapView SlapView;
    public Transform SlapPoint;
    public float PunchForce;
    public float SphereRadius;
    public float ReloadTimer;
    public float ReloadTime;
    public float PunchTimer;
    public float PunchTime;
    public bool CanStartSlap;
    public bool CanPunch;
  }
}