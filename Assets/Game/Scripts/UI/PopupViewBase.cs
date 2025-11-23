using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.UI
{
  public abstract class PopupViewBase : MonoBehaviour
  {
    public CanvasGroup canvasGroup;
    public CanvasGroup backFadeGroup;
    public RectTransform panel;
    protected CanvasHolder CanvasHolder;
    
    public void SetInputActive(bool isActive)
    {
      CanvasHolder.SetActiveRaycaster(isActive);
    }
    
    public virtual Sequence GetShowTween()
    {
      var sequence = DOTween.Sequence();
            
      canvasGroup.alpha = 0.4f;
      panel.localScale = Vector2.one * 0.5f;

      sequence
        .Append(panel.DOScale(1.07f, 0.3f))
        .Join(GetCanvasGroupTween(canvasGroup, 1, 0.3f))
        .Join(GetCanvasGroupTween(backFadeGroup, 1, 0.3f))
        .Append(panel.DOScale(1f, 0.2f))
        .SetEase(Ease.Linear)
        .SetLink(gameObject);

      return sequence;
    }

    public virtual Sequence GetHideTween()
    {
      var sequence = DOTween.Sequence();
            
      sequence
        .Append(panel.DOScale(1.07f, 0.2f))
        .Append(panel.DOScale(0.5f, 0.4f))
        .Join(GetCanvasGroupTween(canvasGroup, 0, 0.2f))
        .Join(GetCanvasGroupTween(backFadeGroup, 0, 0.2f))
        .SetEase(Ease.Linear)
        .SetLink(gameObject);

      return sequence;
    }

    private Tween GetCanvasGroupTween(CanvasGroup target, float value, float duration)
    {
      if (target == null)
        return DOTween.Sequence();

      return target.DOFade(value, duration);
    }

    public void Destroy()
    {
      Destroy(CanvasHolder.gameObject);
    }
  }
}