using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    protected Transform _player;
    protected UI_Joystick _joystick;
    protected Define.State State { get; set; }

    public Define.GameObjects GameObjectType { get; protected set; } = Define.GameObjects.Unknown;

    protected void Awake()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        _joystick = Util.FindChild<UI_Joystick>(MasterManager.UI.Root, "JoystickUI", true);

        if (go != null)
            _player = go.transform;
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        Move();
    }

    public abstract void Init();
    protected virtual void Move() { }
    protected virtual void Die() { }
}
