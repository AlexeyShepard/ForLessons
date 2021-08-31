using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public bool IsPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressed = true;      
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;
    }
}
