using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager
{
    GameObject _background;
    GameObject _rocketStartArea;
    GameObject _player;
    HashSet<GameObject> _rockets = new HashSet<GameObject>();

    GameObject explosion;
    public bool IsEnd { get; private set; }

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
                IsEnd = false;
                break;
            case Define.GameObjects.Rocket:
                _rockets.Add(go);
                break;
        }

        return go;
    }

    public Define.GameObjects GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.GameObjects.Unknown;

        return bc.GameObjectType;
    }

    public void Despawn(GameObject go)
    {
        Define.GameObjects type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.GameObjects.Background:
                _background = null;
                break;
            case Define.GameObjects.RocketStartArea:
                _rocketStartArea = null;
                break;
            case Define.GameObjects.Player:
                _player = null;
                IsEnd = true;
                break;
            case Define.GameObjects.Rocket:
                _rockets.Remove(go);
                break;
        }

        MasterManager.Resource.Destroy(go);
    }

    public void EndGame()
    {
        Explosion();
        Despawn(_player);
        MasterManager.Resource.Instantiate("UI/EndBackground");
    }

    public void Explosion()
    {
        explosion = MasterManager.Resource.Instantiate("Explosion");
        explosion.transform.position = _player.transform.position;
    }

    public void Retry()
    {
        foreach (GameObject go in _rockets)
        {
            Despawn(go);
        }

        Despawn(explosion);
        SceneManager.LoadScene(0);
    }
}