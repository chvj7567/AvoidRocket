using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public GameObject Background { get; private set; }
    public GameObject StartUI { get; private set; }
    public GameObject EndUI { get; private set; }
    public GameObject Joystick { get; private set; }
    public GameObject TimeScore { get; private set; }
    public GameObject SettingUI { get; private set; }

    int _order = 1;
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");  

            if (root == null)
            {
                root = new GameObject { name = "@UI_Root" };
                
            }
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceCamera;

        if(go.name == "MainBackground")
            canvas.planeDistance = 15f;
        else if (go.name == "DangerUI")
            canvas.planeDistance = 10f;
        else
            canvas.planeDistance = 5f;

        canvas.worldCamera = Camera.main;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;

        }
    }

    public GameObject ShowUI(string name, Define.UI type = Define.UI.Unknown)
    {
        GameObject go = MasterManager.Resource.Instantiate($"UI/{name}");

        switch(type)
        {
            case Define.UI.Background:
                if (Background != null)
                    return Background;
                Background = go;
                break;
            case Define.UI.StartUI:
                if (StartUI != null)
                    return StartUI;
                StartUI = go;
                break;
            case Define.UI.EndUI:
                if (EndUI != null)
                    return EndUI;
                EndUI = go;
                break;
            case Define.UI.Joystick:
                if (Joystick != null)
                    return Joystick;
                Joystick = go;
                break;
            case Define.UI.TimeScore:
                if (TimeScore != null)
                    return TimeScore;
                TimeScore = go;
                break;
            case Define.UI.SettingUI:
                if (SettingUI != null)
                    return SettingUI;
                SettingUI = go;
                break;
        }

        go.transform.SetParent(Root.transform);
        SetCanvas(go);

        return go;
    }

    public void HideUI(GameObject ui, Define.UI type = Define.UI.Unknown)
    {
        switch(type)
        {
            case Define.UI.Background:
                Background = null;
                break;
            case Define.UI.StartUI:
                StartUI = null;
                break;
            case Define.UI.EndUI:
                EndUI = null;
                break;
            case Define.UI.Joystick:
                Joystick = null;
                break;
            case Define.UI.TimeScore:
                TimeScore = null;
                break;
            case Define.UI.SettingUI:
                SettingUI = null;
                break;
        }

        ui.SetActive(false);
    }
}