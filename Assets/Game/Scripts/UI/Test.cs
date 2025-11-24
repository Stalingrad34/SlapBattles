using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.UI
{
  public class Test : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
  {
    public void OnPointerDown(PointerEventData eventData)
    {
      Debug.Log("OnPointerDown");
    }

    public void OnDrag(PointerEventData eventData)
    {
      Debug.Log("OnDrag");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      Debug.Log("OnPointerUp");
    }
  }
}