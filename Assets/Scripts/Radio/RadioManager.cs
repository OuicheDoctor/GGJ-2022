using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RadioManager: MonoBehaviour
{
    public static RadioManager Instance { get; private set; }

    private RadioChannel _activeChannel;
    private List<RadioChannelSource> _radioChannelSources;

    [SerializeField] private AudioSettings _audioSettings;
    [SerializeField] private List<AudioSource> _audioSources;

    private void Start()
    {
        InitRadioChannelSource();
    }

    public void ChangeChannel(int channelIndex)
    {
        _activeChannel = _audioSettings.RadioChannels[channelIndex];
        Debug.Log(_activeChannel);
    }

    private void InitRadioChannelSource()
    {
        foreach (var item in _audioSettings.RadioChannels.Select((value, i) => new { value, i }))
        {
            var radioChannelSource = new RadioChannelSource(
                item.value,
                _audioSources[item.i],
                item.value.Musics
            );
        }
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
