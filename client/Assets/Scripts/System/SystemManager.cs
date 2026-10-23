using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager
{
    private List<ISystem> systemList = new List<ISystem>()
    {
        //EventSystem.Instance,
        AssetSystem.Instance,
        //ConfigSystem.Instance,
        TimeSystem.Instance,
        PlayerPrefsSystem.Instance,
        //LanguageSystem.Instance,
        AudioSystem.Instance,
        //EffectSystem.Instance,
        ModuleSystem.Instance,
        //UISystem.Instance,
    };

    private bool isLoadDone;

    public void Init(MonoBehaviour _mono)
    {
        isLoadDone = false;

        foreach (ISystem sys in systemList)
        {
            sys.Init(_mono);
        }
    }

    public void Run()
    {
        if (isLoadDone)
        {
            return;
        }

        foreach (ISystem sys in systemList)
        {
            sys.InitDone();
        }

        isLoadDone = true;
    }

    public void Update(float _deltaTime, float _unscaledDeltaTime)
    {
        if (!isLoadDone)
        {
            return;
        }

        foreach (ISystem sys in systemList)
        {
            sys.Update(_deltaTime, _unscaledDeltaTime);
        }
    }


}
