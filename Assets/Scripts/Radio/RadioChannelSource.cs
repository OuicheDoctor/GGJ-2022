using System;
using System.Collections.Generic;
using UnityEngine;

public class RadioChannelSource
{
    public RadioChannel RadioChannel { get; }
    public AudioSource AudioSource { get; }
    public List<AudioClip> RemainingClips { get; set;  }

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
        var selectedClip = RemainingClips.PickOneAndRemove();
        this.AudioSource.clip = selectedClip;
    }
}
