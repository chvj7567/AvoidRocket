using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        MasterManager.Resource.Instantiate("Background");
        MasterManager.UI.ShowUI("StartUI", Define.UI.StartUI);
        GameObject rocketStartArea = MasterManager.Game.Spawn(Define.GameObjects.RocketStartArea, "RocketStartArea");
        GameObject spawningPool = new GameObject { name = "@Spawning Pool" };
        Util.GetOrAddComponent<SpawningPool>(spawningPool);
    }

    public override void Clear()
    {

    }
}
