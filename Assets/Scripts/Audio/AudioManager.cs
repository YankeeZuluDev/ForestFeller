using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to play music and SFX from corresponding audio sources
/// </summary>
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private List<AudioIDClipPair> audioIDClipPairsList;

    private Dictionary<AudioID, AudioClip> audioIDClipDictionary = new();

    public static AudioManager Instance => instance;

    public void InitializeAudioManagerDictionary()
    {
        // Initialize dictionary from a list of audio ID-Clip pairs
        foreach (AudioIDClipPair audioPair in audioIDClipPairsList)
        {
            audioIDClipDictionary.Add(audioPair.ID, audioPair.clip);
        }
    }

    private void Awake()
    {
        // Singleton // TODO: remove singleton
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        PlayMusic();
    }

    private void PlayMusic()
    {
        // Get corresponding audio from dictionary
        AudioClip musicClip = audioIDClipDictionary[AudioID.Music];

        // Assign the clip to music audio source
        musicSource.clip = musicClip;

        // Play music
        musicSource.Play();
    }

    public void PlaySFX(AudioID audioID)
    {
        // Get corresponding audio from dictionary
        AudioClip SFXClip = audioIDClipDictionary[audioID];

        // Play SFX
        SFXSource.PlayOneShot(SFXClip);
    }

    private void OnValidate()
    {
        // Ensure music source is always looped
        musicSource.loop = true;

        // Ensure SFX source is never looped
        SFXSource.loop = false;

        // Exit if list is empty
        if (audioIDClipPairsList.Count == 0) return;

        // Check if there are duplicates in the list of audio ID-Clip pairs
        if (audioIDClipPairsList.HasAnyDuplicateAudioIDs(out AudioID? duplicateID))
            Debug.LogWarning($"There are duplicates in the list of Audio ID-Clip pairs. Duplicate ID: {duplicateID}");
    }
}
