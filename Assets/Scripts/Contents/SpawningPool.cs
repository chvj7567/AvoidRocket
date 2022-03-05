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
    void Awake()
    {
        _rocketScale = 0.2f;
        _rocketGen = 1f;
        _boomTime = 30;
        _boomCycle = 30;
        _boomAudio = false;

    }
    void Start()
    {
        StartCoroutine(CreateRocket());
    }

    IEnumerator CreateRocket()
    {
        while(true)
        {
            int random = Random.Range(1, 30);
            string randomStr = string.Format("{0:D2}", random);
            Vector3 randPos;

            if (Time.time > _boomTime && Time.time < _boomTime + 1)
            {
                _rocketGen = 0f;
                if (!_boomAudio)
                {
                    Debug.Log($"{_boomTime} Boom");
                    _boomAudio = true;
                    MasterManager.Audio.Play("RocketBoom", Define.Audio.RocketBoom);
                }
            }

            if (Time.time > _boomTime + 1)
            {
                _boomAudio = false;
                _boomTime += _boomCycle;
                _rocketGen = 0.3f;
            }

            while (true)
            {
                randPos = Random.insideUnitCircle * 30;

                if (randPos.x > 21 || randPos.x < -21)
                    break;
                if (randPos.y > 11 || randPos.y < -11)
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

            yield return new WaitForSeconds(_rocketGen);
        }
    }
}
