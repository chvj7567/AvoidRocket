using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        GameObject background = MasterManager.UI.ShowUI("Background");
        GameObject joystickAndScore = MasterManager.UI.ShowUI("JoyStickAndScore");
        GameObject player = MasterManager.Game.Spawn(Define.GameObjects.Player, "SpaceShip");
        GameObject rocketStartArea = MasterManager.Game.Spawn(Define.GameObjects.RocketStartArea, "RocketStartArea");
        GameObject spawningPool = new GameObject { name = "@Spawning Pool" };
        Util.GetOrAddComponent<SpawningPool>(spawningPool);
    }

    public override void Clear()
    {

    }
}
