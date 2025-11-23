using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Extensions
{
  public static class TweenExtensions
  {
    public static Tween Punch(this Transform transform, float power, Action callback = null)
    {
      return transform
        .DOPunchScale(Vector2.one * power, 0.5f, vibrato: 0)
        .SetEase(Ease.Linear)
        .OnComplete(() => callback?.Invoke())
        .SetLink(transform.gameObject);
    }
  }
}