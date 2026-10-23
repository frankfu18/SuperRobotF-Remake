using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMeshProLinkHandler : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI text;
    public System.Action<string> onClickLink;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 pos = new Vector3(eventData.position.x, eventData.position.y, 0);
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, pos, UISystem.Instance.UICamera); //--UI相机
        if (linkIndex > -1)
        {
            TMP_LinkInfo linkInfo = text.textInfo.linkInfo[linkIndex];

            onClickLink?.Invoke(linkInfo.GetLinkID());
        }
    }
}