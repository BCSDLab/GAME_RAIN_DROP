using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    // Audio players components.
    public AudioSource EffectsSource;
    public AudioSource[] MusicSource;
    public AudioClip Background;
    public AudioClip RainDrop;
    public AudioClip RainCol;
    public AudioClip Fire;
    public AudioClip Bomb;

    public int MusicCount = 0;

    // Random pitch adjustment range.
    public Slider BackgroundMusic;
    public float BackgroundValue = 1f;
    public Slider EffectMusic;
    public float EffectValue = 1f;
    public Slider MainBackgroundMusic;
    public Slider MainEffectMusic;
    public float LowPitch = 0.95f;
    public float HighPitch = 1.05f;
    public bool Vib = true;

    public static AudioManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        if (Instance == this)
        {
            UIManager.Instance.HomeBnt();
        }
        DontDestroyOnLoad(gameObject);
    }

    public void CheckSilder()
    {
        //if(BackgroundMusic == null || EffectMusic == null)
        {
            //BackgroundMusic = GameObject.Find("Canvas/option/Panel/bgm").GetComponent<Slider>();
            //EffectMusic = GameObject.Find("Canvas/option/Panel/sfx").GetComponent<Slider>();

            //BackgroundMusic.value = PlayerPrefs.GetFloat("Background", BackgroundValue);
            //EffectMusic.value = PlayerPrefs.GetFloat("Effect", EffectValue);
        }
    }

    public void InitAudioManager()
    {
        //BackgroundMusic = GameObject.Find("Canvas/option/Panel/bgm").GetComponent<Slider>();
        //EffectMusic = GameObject.Find("Canvas/option/Panel/sfx").GetComponent<Slider>();

        //BackgroundMusic.value = PlayerPrefs.GetFloat("Background", BackgroundValue);
        //EffectMusic.value = PlayerPrefs.GetFloat("Effect", EffectValue);
    }

    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        if (MusicCount >= MusicSource.Length)
            return;

        MusicSource[MusicCount].clip = clip;
        MusicSource[MusicCount].Play();

        MusicCount++;
    }

    // Play a random clip from an array, and randomize the pitch slightly.
    public void RandomSoundEffect(AudioClip clips)
    {
        float randomPitch = Random.Range(LowPitch, HighPitch);

        EffectsSource.pitch = randomPitch;
        EffectsSource.clip = clips;
        EffectsSource.Play();
    }

    public void TotalAudioMuteOf()
    {
        foreach (var item in MusicSource)
        {
            item.mute = false;
        }
        EffectsSource.mute = false;
    }

    public void AudioRun()
    {
        CheckSilder();

        foreach (AudioSource item in MusicSource)
        {
            item.volume = BackgroundMusic.value;
            BackgroundValue = BackgroundMusic.value;
            PlayerPrefs.SetFloat("Bakcground", BackgroundValue);
        }
        EffectsSource.volume = EffectMusic.value;
        EffectValue = EffectMusic.value;
        PlayerPrefs.SetFloat("Effect", EffectValue);
    }
}