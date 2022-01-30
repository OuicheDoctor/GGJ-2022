using System;
using UnityEngine;
using TMPro;


public class UIRadio: MonoBehaviour
{
    [SerializeField] RectTransform _flashInfoTextBubble;

    public void OnChannelButtonClick(int channelIndex)
    {
        RadioManager.Instance.ChangeChannel(channelIndex);
    }

    public void OnOnOffButtonClick()
    {
        RadioManager.Instance.ToggleOnOff();
    }

    public void DisplayFlashInfoText(WorldEventData worldEvent)
    {
        _flashInfoTextBubble.gameObject.active = true;
        _flashInfoTextBubble.GetComponentInChildren<TextMeshProUGUI>().text = worldEvent.Headline; // TODO change with the radio text.
    }

    public void HideFlashInfoBubble()
    {
        _flashInfoTextBubble.gameObject.active = false;
        _flashInfoTextBubble.GetComponentInChildren<TextMeshProUGUI>().text = "";
    }
}
