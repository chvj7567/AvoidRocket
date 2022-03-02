using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    GameObject _background;
    GameObject _rocketStartArea;
    GameObject _player;

    public GameObject GetPlayer() { return _player; }

    public GameObject Spawn(Define.GameObjects type, string path, Transform parent = null)
    {
        GameObject go = MasterManager.Resource.Instantiate(path, parent);

        if (go == null)
            return null;

        switch (type)
        {
            case Define.GameObjects.Background:
                _background = go;
                break;
            case Define.GameObjects.RocketStartArea:
                _rocketStartArea = go;
                break;
            case Define.GameObjects.Player:
                _player = go;
                break;
            case Define.GameObjects.Rocket:
                break;
        }

        return go;
    }
}