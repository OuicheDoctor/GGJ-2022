using System;
using UnityEngine;

public class RadioManager: MonoBehaviour
{
    public static RadioManager Instance { get; private set; }

    private RadioChannel _activeChannel;

    [SerializeField] private AudioSettings _audioSettings;

    public void ChangeChannel(int channelIndex)
    {
        _activeChannel = _audioSettings.RadioChannels[channelIndex];
        Debug.Log(_activeChannel);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
