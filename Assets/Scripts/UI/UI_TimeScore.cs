using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TimeScore : UI_Base
{
    enum Texts
    {
        TimeScore,
    }

    Text _score;
    int _avoidTime;

    public override void Init()
    {
        Bind<Text>(typeof(Texts));
        _score = GetText((int)Texts.TimeScore);

        BindEvent(gameObject, AvoidTime);
    }

    public void AvoidTime()
    {
        _avoidTime = int.Parse(Time.time.ToString("F0"));
        _score.text = $"Time : {_avoidTime} second";

        // 충돌 시 이벤트 해제
    }
}
