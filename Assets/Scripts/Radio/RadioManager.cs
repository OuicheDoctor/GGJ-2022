using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RadioManager: MonoBehaviour
{
    public static RadioManager Instance { get; private set; }

    private RadioChannelSource _activeChannelSource = null;
    private List<RadioChannelSource> _radioChannelSources = new List<RadioChannelSource>();

    [SerializeField] private AudioSettings _audioSettings;
    [SerializeField] private List<AudioSource> _audioSources;

    public void ChangeChannel(int channelIndex)
    {
        if (_activeChannelSource != null) _activeChannelSource.AudioSource.mute = true;
        var newChannelSource = _radioChannelSources[channelIndex];
        newChannelSource.AudioSource.mute = false;
        _activeChannelSource = newChannelSource;
    }

    private void Start()
    {
        InitRadioChannelSources();
        StartAllRadioChannelSources();
    }

    private void Update()
    {
        CheckEndOfPlay();
        CheckJingles();
    }

    private void InitRadioChannelSources()
    {
        foreach (var item in _audioSettings.RadioChannels.Select((value, i) => new { value, i }))
        {
            var radioChannelSource = new RadioChannelSource(item.value, _audioSources[item.i]);
            radioChannelSource.ChooseNextMusic();
            radioChannelSource.AudioSource.mute = true;
            _radioChannelSources.Add(radioChannelSource);
        }
    }

    private void StartAllRadioChannelSources()
    {
        foreach (var radioChannelSource in _radioChannelSources)
        {
            radioChannelSource.AudioSource.Play();
        }
    }

    // Check the end of each audio source clip to start or restart another clip directly
    private void CheckEndOfPlay()
    {
        foreach (var radioChannelSource in _radioChannelSources) {
            if (!radioChannelSource.AudioSource.isPlaying)
            {
                if (radioChannelSource.IsCurrentClipAJingle()) radioChannelSource.RestartPausedClip();
                else {
                    radioChannelSource.ChooseNextMusic();
                    radioChannelSource.AudioSource.time = 0;
                    radioChannelSource.AudioSource.Play();
                }
            }
        }
    }

    // Check if a jingle should be launched
    private void CheckJingles()
    {
        foreach (var radioChannelSource in _radioChannelSources)
        {
            var jingle = radioChannelSource.RadioChannel.Jingles.FirstOrDefault(jingle => jingle.Hour == GameManager.Instance.CurrentHour);
            if (jingle != null && !radioChannelSource.IsJingleAlreadyLaunched(jingle))
            {
                radioChannelSource.LaunchJingle(jingle);
            }
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
