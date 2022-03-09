using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Start : UI_Base
{
    Button _start;
    Button _setting;

    enum Buttons
    {
        Start,
        Setting,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        _start = GetButton((int)Buttons.Start).gameObject.GetComponent<Button>();
        _setting = GetButton((int)Buttons.Setting).gameObject.GetComponent<Button>();
        BindEvent(_start.gameObject, StartGame, Define.UIEvent.Click);
        BindEvent(_setting.gameObject, SettingGame, Define.UIEvent.Click);
    }

    public void StartGame(PointerEventData data)
    {
        MasterManager.Game.StartGame();
    }

    public void SettingGame(PointerEventData data)
    {
        MasterManager.Game.SettingGame();
    }
}