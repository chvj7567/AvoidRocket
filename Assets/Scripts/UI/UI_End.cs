using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_End : UI_Base
{
    Button _retry;

    enum Buttons
    {
        BackButton,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        _retry = GetButton((int)Buttons.BackButton).gameObject.GetComponent<Button>();
        BindEvent(_retry.gameObject, RetryGame, Define.UIEvent.Click);
    }

    public void RetryGame(PointerEventData data)
    {
        MasterManager.Game.RetryGame();
    }
}