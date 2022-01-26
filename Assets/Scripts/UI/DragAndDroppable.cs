using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDroppable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Graphic _targetGraphic;

    private Vector3 _mouseDeltaOnGrab;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _targetGraphic.raycastTarget = false;
        _mouseDeltaOnGrab = transform.position - Input.mousePosition;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + _mouseDeltaOnGrab;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _targetGraphic.raycastTarget = true;
    }
}