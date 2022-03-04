using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager
{
    GameObject _background;
    GameObject _player;
    HashSet<GameObject> _rockets = new HashSet<GameObject>();

    GameObject explosion;
    GameObject spawningPool;

    public bool IsStart { get; private set; }
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
            case Define.GameObjects.Player:
                _player = go;
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
            case Define.GameObjects.Player:
                _player = null;
                break;
            case Define.GameObjects.Rocket:
                _rockets.Remove(go);
                break;
        }

        MasterManager.Resource.Destroy(go);
    }

    public void StartGame()
    {
        IsStart = true;
        IsEnd = false;
        MasterManager.UI.DestroyUI(MasterManager.UI.StartUI, Define.UI.StartUI);
        MasterManager.UI.ShowUI("JoystickUI", Define.UI.Joystick);
        MasterManager.UI.ShowUI("TimeScoreUI", Define.UI.TimeScore);
        spawningPool = new GameObject { name = "@Spawning Pool" };
        Util.GetOrAddComponent<SpawningPool>(spawningPool);
        Spawn(Define.GameObjects.Player, "SpaceShip");
    }

    public void EndGame()
    {
        IsStart = false;
        IsEnd = true;
        Explosion();
        Despawn(_player);
        MasterManager.UI.ShowUI("EndUI", Define.UI.EndUI);
    }

    public void Explosion()
    {
        explosion = MasterManager.Resource.Instantiate("Explosion");
        explosion.transform.position = _player.transform.position;
    }

    public void BackGame()
    {
        foreach (GameObject go in _rockets)
        {
            Despawn(go);
        }

        Despawn(explosion);
        Despawn(spawningPool);
        SceneManager.LoadScene(0);
    }
}