using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    [SerializeField]
    float _speed;
    float _size;

    public override void Init()
    {
        State = Define.State.Alive;
        _speed = 10f;
        _size = 10f;
        GameObjectType = Define.GameObjects.Player;
    }

    public override void Move()
    {
        if (_joystick.IsMove && State == Define.State.Alive)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

            if (pos.x < _size) pos.x = _size;
            if (pos.x > Screen.width - _size) pos.x = Screen.width - _size;
            if (pos.y < _size) pos.y = _size;
            if (pos.y > Screen.height - _size) pos.y = Screen.height - _size;

            transform.position = Camera.main.ScreenToWorldPoint(pos);

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
        MasterManager.Audio.Play("Explosion", Define.Audio.Explosion);
        MasterManager.Game.EndGame();
    }
}