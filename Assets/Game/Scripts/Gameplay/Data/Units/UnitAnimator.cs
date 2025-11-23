using UnityEngine;

namespace Game.Scripts.Gameplay.Data.Units
{
  public class UnitAnimator : MonoBehaviour
  {
    private readonly int _grounded = Animator.StringToHash("Grounded");
    private readonly int _speed = Animator.StringToHash("Speed");
    private readonly int _move = Animator.StringToHash("Move");
    private readonly int _dead = Animator.StringToHash("Dead");
    
    [SerializeField] private Animator footAnimator;

    public void SetFlyAnimation(bool isFly)
    {
      footAnimator.SetBool(_grounded, isFly);
    }
    
    public void SetMoveAnimation(float speed)
    {
      footAnimator.SetFloat(_speed, speed);
    }
    
    public void SetMoveAnimation(bool isMove)
    {
      footAnimator.SetBool(_move, isMove);
    }
    
    public void SetDeadAnimation()
    {
      footAnimator.SetTrigger(_dead);
    }
  }
}