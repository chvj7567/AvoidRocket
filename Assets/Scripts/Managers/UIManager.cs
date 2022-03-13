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
    public GameObject DangerUI { get; private set; }


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
        GameObject go = null;

        switch(type)
        {
            case Define.UI.Background:
                if (Background != null)
                {
                    Background.SetActive(true);
                    return Background;
                }
                Background = go = MasterManager.Resource.Instantiate($"UI/{name}");
                break;
            case Define.UI.StartUI:
                if (StartUI != null)
                {
                    StartUI.SetActive(true);
                    return StartUI;
                }
                StartUI = go = MasterManager.Resource.Instantiate($"UI/{name}"); ;
                break;
            case Define.UI.EndUI:
                if (EndUI != null)
                {
                    EndUI.SetActive(true);
                    return EndUI;
                }
                EndUI = go = MasterManager.Resource.Instantiate($"UI/{name}"); ;
                break;
            case Define.UI.Joystick:
                if (Joystick != null)
                {
                    Joystick.SetActive(true);
                    return Joystick;
                }
                Joystick = go = MasterManager.Resource.Instantiate($"UI/{name}"); ;
                break;
            case Define.UI.TimeScore:
                if (TimeScore != null)
                {
                    TimeScore.SetActive(true);
                    return TimeScore;
                }
                TimeScore = go = MasterManager.Resource.Instantiate($"UI/{name}"); ;
                break;
            case Define.UI.SettingUI:
                if (SettingUI != null)
                {
                    SettingUI.SetActive(true);
                    return SettingUI;
                }
                SettingUI = go = MasterManager.Resource.Instantiate($"UI/{name}"); ;
                break;
            case Define.UI.DangerUI:
                if (DangerUI != null)
                {
                    DangerUI.SetActive(true);
                    return DangerUI;
                }
                DangerUI = go = MasterManager.Resource.Instantiate($"UI/{name}"); ;
                break;
        }

        go.transform.SetParent(Root.transform);
        SetCanvas(go);

        return go;
    }

    public void HideUI(GameObject ui)
    {
        ui.SetActive(false);
    }
}