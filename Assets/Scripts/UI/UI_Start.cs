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
        StartGame,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        _start = GetComponent<Button>();
        BindEvent(_start.gameObject, StartGame, Define.UIEvent.Click);
    }

    public void StartGame(PointerEventData data)
    {
        MasterManager.Game.StartGame();
    }
}