using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.Infrastructure.States
{
  public abstract class AbstractGameState
  {
    protected void SetCameraStack()
    {
      UIManager.SetCameraStack(Camera.main);
    }
    
    protected void SetCursorLocked(bool isLocked)
    {
#if UNITY_EDITOR
      Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
      Cursor.visible = !isLocked;
#endif
    }
  }
}