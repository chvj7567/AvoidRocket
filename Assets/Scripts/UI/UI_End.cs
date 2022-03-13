using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_End : UI_Base
{
    
    Button _back;

    enum Buttons
    {
        Back,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        _back = GetButton((int)Buttons.Back);
        BindEvent(_back.gameObject, BackGame, Define.UIEvent.Click);
    }

    public void BackGame(PointerEventData data)
    {
        MasterManager.Game.BackGame();
        MasterManager.UI.HideUI(MasterManager.UI.EndUI);
        MasterManager.UI.HideUI(MasterManager.UI.Joystick);
        MasterManager.UI.HideUI(MasterManager.UI.TimeScore);
        MasterManager.UI.ShowUI("StartUI", Define.UI.StartUI);
    }
}