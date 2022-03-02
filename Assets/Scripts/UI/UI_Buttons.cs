using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Buttons : UI_Base
{
    Button _retry;

    enum Buttons
    {
        Retry,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        _retry = GetButton((int)Buttons.Retry).gameObject.GetComponent<Button>();
        BindEvent(_retry.gameObject, Retry, Define.UIEvent.Click);
    }

    public void Retry(PointerEventData data)
    {
        MasterManager.Game.Retry();
    }
}