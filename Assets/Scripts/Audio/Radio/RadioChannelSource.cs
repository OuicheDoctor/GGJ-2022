using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RadioChannelSource
{
    public RadioChannel RadioChannel { get; }
    public AudioSource AudioSource { get; }
    public List<AudioClip> RemainingClips { get; set;  }

    private (AudioClip clip, float time) _pausedClip = default;
    private List<RadioJingle> _alreadyLaunchedJingles = new List<RadioJingle>();

    public RadioChannelSource(RadioChannel radioChannel, AudioSource audioSource)
    {
        this.RadioChannel = radioChannel;
        this.AudioSource = audioSource;
        this.RemainingClips = new List<AudioClip>();
        ResetRemainingClips();
    }

    public void ResetRemainingClips()
    {
        var musics = new List<AudioClip>(RadioChannel.Musics);
        this.RemainingClips = musics;
    }

    public void ChooseNextMusic()
    {
        if (RemainingClips.Count == 0) ResetRemainingClips();

        var selectedClip = RemainingClips.PickOneAndRemove();
        this.AudioSource.clip = selectedClip;
    }

    public void LaunchJingle(RadioJingle jingle)
    {
        _alreadyLaunchedJingles.Add(jingle);
        _pausedClip = (AudioSource.clip, AudioSource.time);
        AudioSource.clip = jingle.Audio;
        AudioSource.Play();
    }

    public bool IsCurrentClipAJingle()
    {
        return RadioChannel.Jingles.Any(jingle => jingle.Audio == AudioSource.clip);
    }

    public bool IsJingleAlreadyLaunched(RadioJingle jingle)
    {
        return _alreadyLaunchedJingles.Any(j => j == jingle);
    }

    public void LaunchFlashInfo(WorldEventData worldEvent) {
        AudioSource.clip = worldEvent.Announcement;
        AudioSource.Play();
    }

    public bool IsCurrentClipAFlashInfo(WorldEventData worldEvent)
    {
        return worldEvent.Announcement == AudioSource.clip;
    }

    public void RestartPausedClip()
    {
        if (_pausedClip != default)
        {
            AudioSource.clip = _pausedClip.clip;
            AudioSource.time = _pausedClip.time;
            AudioSource.Play();
        }

        _pausedClip = default;
    }
}
