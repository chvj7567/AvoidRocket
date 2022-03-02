using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Texts : UI_Base
{
    enum Texts
    {
        TimeScore,
    }

    Text _score;
    float _startTime;
    float _avoidTime;
    float _result;
    List<int> _data;

    public override void Init()
    {
        _data = new List<int>();
        _result = 0;
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
            if(_result == 0 || _result > _avoidTime)
            {
                _result = _avoidTime;
                _data.Add((int)_result);
                _avoidTime = Time.time;
            }
        }
    }
}
