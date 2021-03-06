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
    GameObject explosion;
    GameObject spawningPool;

    public bool IsGaming { get; private set; }

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
        }

        MasterManager.Resource.Destroy(go);
    }

    public void StartGame()
    {
        IsGaming = true;
        spawningPool = new GameObject { name = "@Spawning Pool" };
        Util.GetOrAddComponent<SpawningPool>(spawningPool);
        Spawn(Define.GameObjects.Player, "SpaceShip");
    }

    public void EndGame()
    {
        IsGaming = false;
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
        GameObject[] rockets = GameObject.FindGameObjectsWithTag("Rocket");

        foreach (GameObject rocket in rockets)
            Despawn(rocket);

        Despawn(explosion);
        Despawn(spawningPool);
        Despawn(MasterManager.UI.TimeScore);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}