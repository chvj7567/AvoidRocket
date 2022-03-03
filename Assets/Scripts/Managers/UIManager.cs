using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public GameObject StartUI { get; private set; }
    public GameObject EndUI { get; private set; }
    public GameObject Joystick { get; private set; }
    public GameObject TimeScore { get; private set; }

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
        canvas.planeDistance = 10f;
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

    public void ShowUI(string name, Define.UI type)
    {
        GameObject go = MasterManager.Resource.Instantiate($"UI/{name}");
        go.transform.SetParent(Root.transform);
        SetCanvas(go, true);

        switch(type)
        {
            case Define.UI.StartUI:
                StartUI = go;
                break;
            case Define.UI.EndUI:
                EndUI = go;
                break;
            case Define.UI.Joystick:
                Joystick = go;
                break;
            case Define.UI.TimeScore:
                TimeScore = go;
                break;
        }
    }

    public void DestroyUI(GameObject ui, Define.UI type)
    {
        switch(type)
        {
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
        }

        MasterManager.Resource.Destroy(ui);
    }
}