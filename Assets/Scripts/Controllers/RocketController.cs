using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField]
    float _speed;
    Transform _player;
    Vector3 _direction;

    void Awake()
    {
        _speed = 10f;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _direction = (_player.position - transform.position).normalized;
    }

    void Update()
    {
        transform.position += _direction * Time.deltaTime * _speed;
        transform.up = _direction;

        if(transform.position.x < -30 || transform.position.x > 30)
            MasterManager.Resource.Destroy(gameObject);
        if(transform.position.y < -30 || transform.position.y > 30)
            MasterManager.Resource.Destroy(gameObject);
    }
}