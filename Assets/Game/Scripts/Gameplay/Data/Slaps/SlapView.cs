using UnityEngine;

namespace Game.Scripts.Gameplay.Data.Slaps
{
  public class SlapView : MonoBehaviour
  {
    private readonly int _slap = Animator.StringToHash("Slap");
    
    [SerializeField] private Animator animator;

    public void StartSlapAnimation()
    {
      animator.SetTrigger(_slap);
      /*var clips = animator.runtimeAnimatorController.animationClips;
      foreach (AnimationClip clip in clips)
      {
        if (clip.name.Equals("HandAttack"))
        {
          return clip.length;
        }
      }
      
      return 0;*/
    }
  }
}