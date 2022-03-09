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
        IsStart = true;
        IsEnd = false;
        MasterManager.UI.HideUI(MasterManager.UI.StartUI, Define.UI.StartUI);
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

    public void SettingGame()
    {
        MasterManager.UI.StartUI.SetActive(false);
        if (MasterManager.UI.SettingUI == null)
        {
            MasterManager.UI.ShowUI("SettingUI", Define.UI.SettingUI);
        }
        else
        {
            MasterManager.UI.SettingUI.SetActive(true);
        }
    }

    public void BackGame(UI_Base ui)
    {
        if (ui is UI_End)
        {
            GameObject[] rockets = GameObject.FindGameObjectsWithTag("Rocket");

            foreach (GameObject rocket in rockets)
                Despawn(rocket);

            Despawn(explosion);
            Despawn(spawningPool);
            MasterManager.UI.HideUI(MasterManager.UI.EndUI, Define.UI.EndUI);
            MasterManager.UI.HideUI(MasterManager.UI.Joystick, Define.UI.Joystick);
            MasterManager.UI.HideUI(MasterManager.UI.TimeScore, Define.UI.TimeScore);
            MasterManager.UI.ShowUI("StartUI", Define.UI.StartUI);
        }
        else if (ui is UI_Setting)
        {
            MasterManager.UI.SettingUI.SetActive(false);
            MasterManager.UI.StartUI.SetActive(true);
        }
    }
}