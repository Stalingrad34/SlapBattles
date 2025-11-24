using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Gameplay.Data
{
  public class RotateJoystick : MonoBehaviour, IBeginDragHandler,  IDragHandler, IEndDragHandler
  {
    [SerializeField] private float sensitivity = 1;
    public Vector2 delta;
    private Vector2 _startPoint;

    private void Update()
    {
      delta = Vector2.zero;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
      
    }

    public void OnDrag(PointerEventData eventData)
    {
      delta = eventData.delta * sensitivity;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      
    }
  }
}