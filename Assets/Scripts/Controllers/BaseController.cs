using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    protected Transform _player;
    protected UI_Images _joystick;
    protected Define.State State { get; set; }

    public Define.GameObjects GameObjectType { get; protected set; } = Define.GameObjects.Unknown;

    private void Awake()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        _joystick = Util.FindChild<UI_Images>(MasterManager.UI.Root, "Joystick", true);

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
    public virtual void Move() { }
    public virtual void Die() { }
}
