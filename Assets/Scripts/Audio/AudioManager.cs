using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioSettings _audioSettings;
    [SerializeField] AudioSource _bgmAudioSource;

    public void PlayBGM(string name)
    {
        var audioClipInfo = _audioSettings.BackgroundMusics.Find(aci => aci.Name == name);
        _bgmAudioSource.clip = audioClipInfo.AudioFile;
        _bgmAudioSource.Play();
    }

    public void PlaySFX(string name)
    {
        var audioClipInfo = _audioSettings.SoundEffects.Find(aci => aci.Name == name);
        AudioSource.PlayClipAtPoint(audioClipInfo.AudioFile, new Vector3(0, 0, 0));
    }

    public void StopCurrentBGM()
    {
        _bgmAudioSource.Stop();
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
