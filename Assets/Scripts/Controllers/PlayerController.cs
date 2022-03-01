using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed;
    UI_Joystick _joystick;
    
    void Awake()
    {
        _speed = 10f;
        _joystick = Util.FindChild<UI_Joystick>(MasterManager.UI.Root, "Joystick", true);
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (_joystick.IsMove)
        {
            Vector3 direction = _joystick.TouchPos.normalized;
            transform.position += direction * Time.deltaTime * _speed;
        }
    }
}