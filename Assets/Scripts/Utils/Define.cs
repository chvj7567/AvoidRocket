using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    // �÷��̾� ����
    public enum State
    {
        Alive,
        Die,
    }

    public enum Audio
    {
        Bgm,
        Explosion,
        RocketBoom,
        MaxCount,
    }
    public enum GameObjects
    {
        Unknown,
        Background,
        Player,
        Rocket,
    }

    public enum UI
    {
        Unknown,
        Background,
        StartUI,
        EndUI,
        Joystick,
        TimeScore,
        SettingUI,
        DangerUI,
    }

    public enum UIEvent
    {
        BeginDrag,
        Drag,
        EndDrag,
        Update,
        Click,
    }
}
