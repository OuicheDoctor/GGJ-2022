using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RadioManager: MonoBehaviour
{
    public static RadioManager Instance { get; private set; }

    private RadioChannelSource _activeChannelSource = null;
    private List<RadioChannelSource> _radioChannelSources = new List<RadioChannelSource>();
    private bool _isRadioOn = false;
    private bool _isActive = false;

    [SerializeField] private AudioSettings _audioSettings;
    [SerializeField] private List<AudioSource> _audioSources;

    // Prepare radio for a new day
    public void InitState()
    {
        InitRadioChannelSources();
        ToggleAllRadioChannelSources(true);
        ToggleOnOff();
        _isActive = true;
    }

    // Reset the radio to the inital state. Need to Init after this.
    public void ResetState()
    {
        _isActive = false;
        ToggleAllRadioChannelSources(false);
        _radioChannelSources = new List<RadioChannelSource>();
        _activeChannelSource = null;
        _isRadioOn = false;
    }

    public void ChangeChannel(int channelIndex)
    {
        if (_activeChannelSource != null) _activeChannelSource.AudioSource.mute = true;
        var newChannelSource = _radioChannelSources[channelIndex];
        newChannelSource.AudioSource.mute = false;
        _activeChannelSource = newChannelSource;
    }

    public void ToggleOnOff()
    {
        if (_isRadioOn) {
            MuteAllRadioChannelSources();
        } else if (_activeChannelSource != null) {
            _activeChannelSource.AudioSource.mute = false;
        }

        _isRadioOn = !_isRadioOn;
    }

    private void Update()
    {
        if (_isActive)
        {
            CheckEndOfPlay();
            CheckJingles();
        }
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
        _activeChannelSource = _radioChannelSources[0];
    }

    // Play or stop every audio sources
    private void ToggleAllRadioChannelSources(bool play)
    {
        foreach (var radioChannelSource in _radioChannelSources)
        {
            if (play) radioChannelSource.AudioSource.Play();
            else radioChannelSource.AudioSource.Stop();
        }
    }

    private void MuteAllRadioChannelSources()
    {
        foreach (var radioChannelSource in _radioChannelSources)
        {
            radioChannelSource.AudioSource.mute = true;
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
