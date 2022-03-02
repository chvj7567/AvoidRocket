using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : BaseController
{
    [SerializeField]
    float _speed;
    Vector3 _direction;

    public override void Init()
    {
        State = Define.State.Alive;
        _speed = 10f;
        if(_player != null)
            _direction = (_player.position - transform.position).normalized;
        else
            _direction = (Vector3.zero - transform.position).normalized;

        GameObjectType = Define.GameObjects.Rocket;
    }

    public override void Move()
    {
        transform.position += _direction * Time.deltaTime * _speed;
        transform.up = _direction;

        if (transform.position.x < -30 || transform.position.x > 30)
            Die();
        if (transform.position.y < -30 || transform.position.y > 30)
            Die();
    }

    public override void Die()
    {
        State = Define.State.Die;
        MasterManager.Game.Despawn(gameObject);
    }

    
}