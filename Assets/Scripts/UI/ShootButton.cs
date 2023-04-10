using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _isHold = false;

    public bool IsHold => _isHold;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isHold= true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isHold = false;
    }
}
