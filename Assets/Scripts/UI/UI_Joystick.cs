using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Joystick : UI_Base
{
    RectTransform _lever;
    RectTransform _joystick;
    float _leverRange;
    public Vector2 TouchPos { get; private set; }
    public bool IsMove { get; private set; }

    enum Images
    {
        Joystick,
        Lever,
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        _lever = GetImage((int)Images.Lever).gameObject.GetComponent<RectTransform>();
        _joystick = GetComponent<RectTransform>();
        _leverRange = 35f;

        BindEvent(_lever.gameObject, MoveStart, Define.UIEvent.BeginDrag);
        BindEvent(_lever.gameObject, MoveStart, Define.UIEvent.Drag);
        BindEvent(_lever.gameObject, MoveEnd, Define.UIEvent.EndDrag);
    }

    void MoveStart(PointerEventData data)
    {
        IsMove = true;

        TouchPos = data.position - _joystick.anchoredPosition;

        if (TouchPos.magnitude > _leverRange)
        {
            TouchPos = TouchPos.normalized * _leverRange;
        }

        _lever.anchoredPosition = TouchPos;
    }

    void MoveEnd(PointerEventData data)
    {
        IsMove = false;

        _lever.anchoredPosition = Vector2.zero;
    }
}