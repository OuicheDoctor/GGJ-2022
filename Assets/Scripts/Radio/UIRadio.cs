using System;
using UnityEngine;

public class UIRadio: MonoBehaviour
{
    public void OnChannelButtonClick(int channelIndex)
    {
        RadioManager.Instance.ChangeChannel(channelIndex);
    }
}
