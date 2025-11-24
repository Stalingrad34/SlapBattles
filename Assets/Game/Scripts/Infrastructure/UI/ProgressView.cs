using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
  public class ProgressView : MonoBehaviour
  {
    [SerializeField] private Slider slider;
    [SerializeField] private float tweenSpeed = 1;
    [SerializeField] private bool decreaseTween;

    private Tween _tween;
    private float? _lastValue;

    public void SetValue(int value, int max)
    {
      var progress = value / (float)max;
      if (_lastValue.HasValue && (progress > _lastValue || decreaseTween))
      {
        StartProgressTween(progress);
      }
      else
      {
        SetProgressImmediately(progress);
        _lastValue = progress;
      }
    }

    private void StartProgressTween(float value)
    {
      _tween?.Complete();
      
      var duration = Math.Abs(_lastValue.Value - value) * 10 / Math.Max(tweenSpeed, 1);
      _tween = DOTween
        .To(() => _lastValue.Value, SetProgressImmediately, value, duration)
        .OnComplete(() => _lastValue = value)
        .SetEase(Ease.Linear)
        .SetLink(gameObject);
    }

    private void SetProgressImmediately(float value)
    {
      slider.value = value;
    }

    private void OnDestroy()
    {
      _tween?.Kill();
      _tween = null;
    }
  }
}