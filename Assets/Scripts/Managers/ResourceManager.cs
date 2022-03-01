using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if(typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');

            if(index != -1)
            {
                name = name.Substring(index + 1);
            }

            // Original ������Ʈ�� Ǯ�� ����Ǿ� �ִٸ� ��ȯ
            GameObject go = MasterManager.Pool.GetOriginal(name);

            if (go != null)
                return go as T;
        }
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if(original == null)
        {
            Debug.Log($"Failed To Load Prefab : {path}");
            return null;
        }

        // Prefabs ���� �ڽ� ���� ���� �������̸� Ǯ�� ������� ����
        string name = path;
        int index = name.IndexOf('/');

        if (index == 6)
        {
            original.AddComponent<Poolable>();
        }

        // Ǯ�� ����̸� Pop���� �����ش�.
        if (original.GetComponent<Poolable>() != null)
            return MasterManager.Pool.Pop(original, parent).gameObject;

        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;

        return go;
    }

    public void Destroy(GameObject go)
    {
        if(go == null)
            return;

        // Ǯ�� ����̸� �ı����� �ʰ� ���ÿ� Push�Ѵ�.
        if(go.GetComponent<Poolable>() != null)
        {
            MasterManager.Pool.Push(go.GetComponent<Poolable>());
            return;
        }

        Object.Destroy(go);
    }
}