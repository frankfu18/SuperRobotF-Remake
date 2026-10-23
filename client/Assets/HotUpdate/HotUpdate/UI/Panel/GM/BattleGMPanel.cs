using UnityEngine;
using System.Collections.Generic;

public partial class BattleGMPanel : UIPanel
{
    protected override void OnRegisterListener()
    {
        base.OnRegisterListener();

        // AddButtonClick(this.m_Button_AddRes, OnClickAddRes);
    }

    protected override void OnShow()
    {
        base.OnShow();
    }

    // private void OnClickAddRes()
    // {
    //     if(int.TryParse(this.m_TMP_InputField_AddRes.text, out int res))
    //     {
    //         BattleManager.Instance.AddLocalPlanetResourceForTest(res);
    //     }
    // }

}
