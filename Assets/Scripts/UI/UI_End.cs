using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_End : UI_Base
{
    
    Button _back;
    Text _score;

    enum Buttons
    {
        Back,
    }

    enum Texts
    {
        Score,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        _back = GetButton((int)Buttons.Back).gameObject.GetComponent<Button>();
        _score = GetText((int)Texts.Score).gameObject.GetComponent<Text>();
        BindEvent(_back.gameObject, BackGame, Define.UIEvent.Click);
    }

    public void BackGame(PointerEventData data)
    {
        MasterManager.Game.BackGame(this);
    }
}