using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class TouchController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static event Action<Vector2> OnBeginDragMovement;
    public static event Action<Vector2> OnDragMovement;
    public static event Action<Vector2> OnEndDragMovement;

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragMovement?.Invoke(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragMovement?.Invoke(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragMovement?.Invoke(eventData.position);
    }
}
