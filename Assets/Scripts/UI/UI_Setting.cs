using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Setting : UI_Base
{
    Slider _volume;
    Button _back;

    enum Sliders
    {
        VolumeS,
    }

    enum Buttons
    {
        Back,
    }
    public override void Init()
    {
        Bind<Slider>(typeof(Sliders));
        Bind<Button>(typeof(Buttons));

        _volume = Get<Slider>((int)Sliders.VolumeS).gameObject.GetComponent<Slider>();
        _back = GetButton((int)Buttons.Back).gameObject.GetComponent<Button>();

        BindEvent(_volume.gameObject, SliderVolume);
        BindEvent(_back.gameObject, BackGame, Define.UIEvent.Click);
    }

    public void SliderVolume()
    {
        MasterManager.Audio.SetVolume(_volume.value);
    }

    public void BackGame(PointerEventData data)
    {
        MasterManager.Game.BackGame(this);
    }
}