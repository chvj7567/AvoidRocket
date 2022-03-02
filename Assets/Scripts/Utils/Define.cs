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

    public enum GameObjects
    {
        Unknown,
        Background,
        RocketStartArea,
        Player,
        Rocket,
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
