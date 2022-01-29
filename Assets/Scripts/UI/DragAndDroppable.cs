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
    public Graphic TargetGraphic => _targetGraphic;
    public Vector3 StartPosDrag => _startPosDrag;
    public Transform StartParent => _startParent;
    public Vector3 MouseDeltaOnGrab { get; set; }

    private Vector3 _startPosDrag;
    private Transform _startParent;
    private bool _wasDragged;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _wasDragged = true;
        _targetGraphic.raycastTarget = false;
        _startPosDrag = transform.position;
        MouseDeltaOnGrab = _startPosDrag - Input.mousePosition;
        transform.SetAsLastSibling();
        ValidDrop = false;
        OnBeginDragCallback?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + MouseDeltaOnGrab;
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
        _startParent = transform.parent;
        OnPointerDownCallback?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpCallback?.Invoke(_wasDragged);
        _wasDragged = false;
    }
}