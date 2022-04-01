using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    float _rocketScale;
    [SerializeField]
    float _rocketGen;

    int _boomTime;
    int _boomCycle;
    bool _boomAudio;

    UI_Score _score;

    bool _dangerCheck;
    GameObject _dangerUI;

    void Awake()
    {
        _rocketScale = 0.2f;
        _rocketGen = 1f;
        _boomTime = 20;
        _boomCycle = 20;
        _boomAudio = false;

        _score = MasterManager.UI.Root.GetComponentInChildren<UI_Score>();
        _dangerUI = MasterManager.UI.ShowUI("DangerUI", Define.UI.DangerUI);
        _dangerUI.SetActive(false);
    }
    void Start()
    {
        StartCoroutine(CreateRocket());
    }

    IEnumerator CreateRocket()
    {
        while (true)
        {
            int random = Random.Range(1, 30);
            string randomStr = string.Format("{0:D2}", random);
            Vector3 randPos;

            if (_score.AvoidTime > _boomTime - 1 && _score.AvoidTime < _boomTime + 1)
            {
                _rocketGen = 0f;
                if (!_boomAudio)
                {
                    Debug.Log($"{_boomTime} Boom");
                    _boomAudio = true;
                    MasterManager.Audio.Play("RocketBoom", Define.Audio.RocketBoom);
                    StartCoroutine(Danger());
                }
            }

            if (_score.AvoidTime > _boomTime + 1)
            {
                _boomAudio = false;
                _boomTime += _boomCycle;
                _rocketGen = 0.3f;
            }

            while (true)
            {
                randPos = Random.insideUnitCircle * 30;
                if (randPos.x > Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0f)).x
                    || randPos.x < Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)).x)
                    break;
                if (randPos.y > Camera.main.ScreenToWorldPoint(new Vector2(0f, Screen.height)).y
                    || randPos.y < Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)).y)
                    break;
            }

            GameObject rocket = MasterManager.Resource.Instantiate($"Rocket/Rocket{randomStr}_Red");
            rocket.transform.position = randPos;
            rocket.transform.localScale = new Vector3(_rocketScale, _rocketScale, _rocketScale);

            RocketController controller = rocket.GetComponent<RocketController>();
            if (controller != null)
            {
                controller.Init();
            }
            else
            {
                controller = Util.GetOrAddComponent<RocketController>(rocket);
            }

            Rigidbody2D rb = Util.GetOrAddComponent<Rigidbody2D>(rocket);
            rb.gravityScale = 0;

            CapsuleCollider2D col = Util.GetOrAddComponent<CapsuleCollider2D>(rocket);

            rocket.layer = LayerMask.NameToLayer("Rocket");
            rocket.tag = "Rocket";

            yield return new WaitForSeconds(_rocketGen);
        }
    }

    IEnumerator Danger()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_dangerCheck)
            {
                _dangerCheck = false;
                _dangerUI.SetActive(false);
            }
            else
            {
                _dangerCheck = true;
                _dangerUI.SetActive(true);
            }

            yield return new WaitForSeconds(0.25f);
        }
    }
}
