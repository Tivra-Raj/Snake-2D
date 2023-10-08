using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sounds
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager instance;
        public static SoundManager Instance { get { return instance; } }

        [SerializeField] private AudioSource soundEffect;
        [SerializeField] private AudioSource music;
        [SerializeField] private AudioType[] type;

        public AudioSource SoundEffect { get { return soundEffect; } }

        private void Awake()
        {
            if (instance == null)    
            {
                instance = this;   
                DontDestroyOnLoad(gameObject);   
            }
            else
            {
                Destroy(gameObject);    
            }
        }

        public void PlayMusic(Sounds audio)
        {
            AudioClip clip = GetAudioClip(audio);
            if (clip != null)
            {
                music.clip = clip;
                music.Play();
            }
            else
            {
                Debug.LogError("Clip not found for audio type : " + audio);
            }
        }

        public void StopMusic(Sounds audio)
        {
            AudioClip clip = GetAudioClip(audio);
            if (clip != null)
            {
                music.clip = clip;
                music.Stop();
            }
            else
            {
                Debug.LogError("Clip not found for audio type : " + audio);
            }
        }

        public void PlaySoundEffect(Sounds audio)
        {
            AudioClip clip = GetAudioClip(audio);
            if (clip != null)
            {
                soundEffect.PlayOneShot(clip);
            }
            else
            {
                Debug.LogError("Clip not found for audio type : " + audio);
            }
        }

        private AudioClip GetAudioClip(Sounds audio)
        {
            AudioType Type = Array.Find(type, Audio => Audio.soundType == audio);
            if (Type != null)
                return Type.audioClip;
            return null;
        }

        public void SetMusicVolume(float volume)
        {
            music.volume = volume;
            SaveVolume(volume);
        }

        public void SaveVolume(float volumeSlider)
        {
            PlayerPrefs.SetFloat("musicVolume", volumeSlider);
        }

        public void LoadVolume(Slider volumeSlider)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
    }

    [Serializable]
    public class AudioType
    {
        public Sounds soundType;
        public AudioClip audioClip;
    }
}