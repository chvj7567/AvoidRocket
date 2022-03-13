using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Start : UI_Base
{
    Button _start;
    Button _setting;
    Button _exit;

    enum Buttons
    {
        Start,
        Setting,
        Exit
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        _start = GetButton((int)Buttons.Start);
        _setting = GetButton((int)Buttons.Setting);
        _exit = GetButton((int)Buttons.Exit);

        BindEvent(_start.gameObject, StartGame, Define.UIEvent.Click);
        BindEvent(_setting.gameObject, SettingGame, Define.UIEvent.Click);
        BindEvent(_exit.gameObject, ExitGame, Define.UIEvent.Click);
    }

    public void StartGame(PointerEventData data)
    {
        MasterManager.UI.HideUI(gameObject);
        MasterManager.UI.ShowUI("JoystickUI", Define.UI.Joystick);
        MasterManager.UI.ShowUI("TimeScoreUI", Define.UI.TimeScore);
        MasterManager.Game.StartGame();
    }

    public void SettingGame(PointerEventData data)
    {
        MasterManager.UI.HideUI(gameObject);
        MasterManager.UI.ShowUI("SettingUI", Define.UI.SettingUI);
    }

    public void ExitGame(PointerEventData data)
    {
        MasterManager.Game.ExitGame();
    }
}