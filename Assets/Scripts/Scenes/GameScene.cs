using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        MasterManager.UI.ShowUI("MainBackground", Define.UI.Background);
        MasterManager.UI.ShowUI("StartUI", Define.UI.StartUI);
    }

    public override void Clear()
    {

    }
}
