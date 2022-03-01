using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
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

    public GameObject ShowUI(string name)
    {
        GameObject go = MasterManager.Resource.Instantiate($"UI/{name}");
        go.transform.SetParent(Root.transform);
        SetCanvas(go, true);

        return go;
    }

}