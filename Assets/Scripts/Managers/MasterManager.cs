using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 싱글턴 패턴 이용
public class MasterManager : MonoBehaviour
{
    static MasterManager m_instance;
    static MasterManager Instance { get { Init(); return m_instance; } }

    GameManager _game = new GameManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    AudioManager _audio = new AudioManager();
    UIManager _ui = new UIManager();

    public static GameManager Game { get { return Instance._game; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static AudioManager Audio { get { return Instance._audio; } }
    public static UIManager UI { get { return Instance._ui; } }

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    static void Init()
    {
        if (m_instance == null)
        {
            GameObject go = GameObject.Find("@MasterManager");
            if (go == null)
            {
                go = new GameObject { name = "@MasterManager" };
                go.AddComponent<MasterManager>();
            }

            DontDestroyOnLoad(go);
            m_instance = go.GetComponent<MasterManager>();
        }

        m_instance._pool.Init();
        m_instance._audio.Init();
    }
}
