using DG.Tweening;
using Game.Scripts.Infrastructure.Custom;
using UnityEngine;

namespace Game.Scripts.Widgets
{
  public class NotificationView : MonoBehaviour
  {
    [SerializeField] private CustomText notificationText;
    private Sequence _sequence;
    
    public void Show(string text)
    {
      notificationText.SetText(text);
      _sequence?.Complete();
      _sequence = DOTween.Sequence();
      _sequence
        .OnStart(() =>
        {
          transform.localScale = Vector3.zero;
          gameObject.SetActive(true);
        })
        .Append(transform.DOScale(Vector3.one, 0.1f))
        .AppendInterval(1.5f)
        .SetEase(Ease.Linear)
        .OnComplete(() => gameObject.SetActive(false));
    }

    public void Hide()
    {
      _sequence?.Complete();
    }
  }
}