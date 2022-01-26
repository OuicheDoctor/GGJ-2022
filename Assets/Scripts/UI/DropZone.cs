using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public Action<GameObject> OnDropCallback { get; set; }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropCallback?.Invoke(eventData.pointerDrag);
    }
}