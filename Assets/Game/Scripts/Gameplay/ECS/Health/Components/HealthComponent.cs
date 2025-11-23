using UnityEngine.UI;

namespace Game.Scripts.Gameplay.ECS.Health.Components
{
  public struct HealthComponent
  {
    public int MaxHealth;
    public int CurrentHealth;
    public Image ProgressBar;
    public bool LookAt;
  }
}