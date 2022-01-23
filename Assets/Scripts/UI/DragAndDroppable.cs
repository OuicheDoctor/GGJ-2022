using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDroppable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private bool _dragging = false;
    private Vector3 _mouseDeltaOnGrab;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _mouseDeltaOnGrab = transform.position - Input.mousePosition;
        transform.SetAsLastSibling();
        _dragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + _mouseDeltaOnGrab;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _dragging = false;
    }
}