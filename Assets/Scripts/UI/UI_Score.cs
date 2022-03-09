using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Score : UI_Base
{
    enum Texts
    {
        TimeScore,
    }

    Text _score;
    float _startTime;
    public float AvoidTime { get; private set; }

    float _record;

    public override void Init()
    {
        _record = -1;
        _startTime = Time.time;

        Bind<Text>(typeof(Texts));
        _score = GetText((int)Texts.TimeScore);

        BindEvent(gameObject, AvoidRecord);
    }

    public void AvoidRecord()
    {
        AvoidTime = float.Parse((Time.time - _startTime).ToString("F0"));
        _score.text = $"Time : {AvoidTime} second";

        if (MasterManager.Game.IsEnd)
        {
            if (_record == -1 || _record > AvoidTime)
            {
                _record = AvoidTime;

                Text record = Util.FindChild(MasterManager.UI.Root, "Score", true).GetComponent<Text>();
                record.text = $"{_record} second";
            }
        }
    }
}
