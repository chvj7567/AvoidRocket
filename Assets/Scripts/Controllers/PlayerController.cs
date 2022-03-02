using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    [SerializeField]
    float _speed;

    public override void Init()
    {
        State = Define.State.Alive;
        _speed = 10f;
        GameObjectType = Define.GameObjects.Player;
    }

    public override void Move()
    {
        if (_joystick.IsMove && State == Define.State.Alive)
        {
            Vector3 direction = _joystick.TouchPos.normalized;
            transform.position += direction * Time.deltaTime * _speed;
        }
    }

    public override void Die()
    {
        State = Define.State.Die;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            return;
        MasterManager.Game.EndGame();
    }
}