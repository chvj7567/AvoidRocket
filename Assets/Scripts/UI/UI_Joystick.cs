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
        _joystick = GetImage((int)Images.Joystick).gameObject.GetComponent<RectTransform>();
        _lever = GetImage((int)Images.Lever).gameObject.GetComponent<RectTransform>();

        _leverRange = _joystick.rect.width * 0.35f;

        BindEvent(_lever.gameObject, MoveStart, Define.UIEvent.BeginDrag);
        BindEvent(_lever.gameObject, MoveStart, Define.UIEvent.Drag);
        BindEvent(_lever.gameObject, MoveEnd, Define.UIEvent.EndDrag);
    }

    void MoveStart(PointerEventData data)
    {
        IsMove = true;

        Vector2 rectPosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystick, data.position, Camera.main, out rectPosition);

        TouchPos = rectPosition;

        TouchPos = Vector2.ClampMagnitude(TouchPos, _leverRange);

        _lever.anchoredPosition = TouchPos;
    }

    void MoveEnd(PointerEventData data)
    {
        IsMove = false;

        _lever.anchoredPosition = Vector2.zero;
    }
}