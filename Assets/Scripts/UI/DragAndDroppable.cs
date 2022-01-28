using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDroppable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Graphic _targetGraphic;

    public Action OnPointerDownCallback { get; set; }
    public Action<bool> OnPointerUpCallback { get; set; }
    public Action OnBeginDragCallback { get; set; }
    public Action<bool> OnEndDragCallback { get; set; }

    public bool ValidDrop { get; set; } = false;

    private Vector3 _mouseDeltaOnGrab;
    private bool _wasDragged;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _wasDragged = true;
        _targetGraphic.raycastTarget = false;
        _mouseDeltaOnGrab = transform.position - Input.mousePosition;
        transform.SetAsLastSibling();
        OnBeginDragCallback?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + _mouseDeltaOnGrab;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!ValidDrop)
        {
            _targetGraphic.raycastTarget = true;
        }

        OnEndDragCallback?.Invoke(ValidDrop);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownCallback?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpCallback?.Invoke(_wasDragged);
        _wasDragged = false;
    }
}