using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Action<PointerEventData> OnBeginDragHandler = null;
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnEndDragHandler = null;

    public Action OnUpdateHandler = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(OnBeginDragHandler != null)
        {
            OnBeginDragHandler.Invoke(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragHandler != null)
        {
            OnDragHandler.Invoke(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDragHandler != null)
        {
            OnEndDragHandler.Invoke(eventData);
        }
    }
    
    void Update()
    {
        if (OnUpdateHandler != null)
            OnUpdateHandler.Invoke();
    }
}
