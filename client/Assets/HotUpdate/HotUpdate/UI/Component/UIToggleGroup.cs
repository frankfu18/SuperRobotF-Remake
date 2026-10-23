using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggleGroup : MonoBehaviour
{

    private const string ClickSoundId = "common_click";
    
    private List<UIToggle> toggleList = new List<UIToggle> ();
    private UIToggle selectToggle;

    public void AddToggle(UIToggle _toggle)
    {
        toggleList.Add(_toggle);
    }

    public void Select(UIToggle _toggle)
    {
        AudioHandler.Instance.PlayAudioById(ClickSoundId);
        if (selectToggle == _toggle)
        {
            return;
        }
        if (selectToggle != null)
        {
            selectToggle.Set(false);
        }
        selectToggle = _toggle;
    }

    public void CancelAll()
    {
        foreach (var toggle in toggleList)
        {
            toggle.Set(false);
        }
    }

    public void CancelSelect()
    {
        if (selectToggle != null)
        {
            selectToggle.Set(false);
        }
        selectToggle = null;
    }
}
