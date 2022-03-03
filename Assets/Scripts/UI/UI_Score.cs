using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Score : UI_Base
{
    enum Texts
    {
        TimeScore = 0,
    }

    Text _score;
    float _startTime;
    float _avoidTime;

    float _record;

    public override void Init()
    {
        _record = 0;
        _startTime = Time.time;

        Bind<Text>(typeof(Texts));
        _score = GetText((int)Texts.TimeScore);

        BindEvent(gameObject, AvoidTime);
    }

    public void AvoidTime()
    {
        _avoidTime = float.Parse((Time.time - _startTime).ToString("F0"));
        _score.text = $"Time : {_avoidTime} second";

        if (MasterManager.Game.IsEnd)
        {
            if (_record == 0 || _record > _avoidTime)
            {
                _record = _avoidTime;

                UI_Score score = Util.FindChild(MasterManager.UI.Root, "TimeScoreUI", true).GetComponent<UI_Score>();
                Text record = Util.FindChild(MasterManager.UI.Root, "Score", true).GetComponent<Text>();
                if (score != null)
                {
                    record.text = $"{_record} second";
                }

                _avoidTime = Time.time;
            }
        }
    }
}
