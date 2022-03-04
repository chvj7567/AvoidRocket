using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Start : UI_Base
{
    Button _start;

    enum Buttons
    {
        StartButton,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        _start = GetButton((int)Buttons.StartButton).gameObject.GetComponent<Button>();
        BindEvent(_start.gameObject, StartGame, Define.UIEvent.Click);
    }

    public void StartGame(PointerEventData data)
    {
        MasterManager.Game.StartGame();
    }
}