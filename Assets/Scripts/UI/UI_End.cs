using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_End : UI_Base
{
    
    Button _retry;
    Text _score;

    enum Buttons
    {
        BackButton,
    }

    enum Texts
    {
        Score,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        _retry = GetButton((int)Buttons.BackButton).gameObject.GetComponent<Button>();
        _score = GetText((int)Texts.Score).gameObject.GetComponent<Text>();
        BindEvent(_retry.gameObject, BackGame, Define.UIEvent.Click);
    }

    public void BackGame(PointerEventData data)
    {
        MasterManager.Game.BackGame();
    }
}