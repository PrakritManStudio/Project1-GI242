using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource SoundFX;

        [SerializeField]
        private AudioClip PlayerFireClip, PlayerExplodeClip, EnemyFireClip, EnemyExplodeClip;
        //[SerializeField] private SoundClip[] soundClips;

        public static SoundManager Instance { get; private set; }
        private AudioSource audioSource;
        
        public void Awake()
        {
            //Debug.Assert(soundClips != null && soundClips.Length != 0, "Sound clips need to setup");
            
            //audioSource = GetComponent<AudioSource>();

            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }

        // public void BGMusic()
        // {
        //     audioSource.loop = true;
        //     SoundFX.clip = BGMusicClip;
        //     SoundFX.Play();
        // }
        
        public void PlayerFireSound()
        {
            SoundFX.clip = PlayerFireClip;
            SoundFX.Play();
        }
        
        public void EnemyFireClipSound()
        {
            SoundFX.clip = PlayerExplodeClip;
            SoundFX.Play();
        }
        
        public void PlayerExplodeSound()
        {
            SoundFX.clip = PlayerExplodeClip;
            SoundFX.Play();
        }
        
        public void EnemyFireSound()
        {
            SoundFX.clip = EnemyFireClip;
            SoundFX.Play();
        }
        
        public void EnemyExplodeSound()
        {
            SoundFX.clip = EnemyExplodeClip;
            SoundFX.Play();
        }

        // public enum Sound
        // {
        //     BGMusic,
        //     PlayerFire,
        //     PlayerExplode,
        //     EnemyFire,
        //     EnemyExplode,
        //     
        // }
        
        // [Serializable]
        // public struct SoundClip
        // {
        //     public Sound sounds;
        //     public AudioClip AudioClip;
        //     [Range(0, 1)]public float soundVolume;
        //     
        // }
        //
        // public void Play(AudioSource audioSource, Sound sound)
        // {
        //     var soundClip = GetSoundClip(sound);
        //     audioSource.clip = soundClip.AudioClip;
        //     audioSource.volume = soundClip.soundVolume;
        //     audioSource.Play();
        // }
        //
        // public void PlayBGMusic()
        // {
        //     audioSource.loop = true;
        //     Play(audioSource, Sound.BGMusic);
        // }
        //
        // private SoundClip GetSoundClip(Sound sound)
        // {
        //     foreach (var soundClip in soundClips)
        //     {
        //         if (soundClip.sounds == sound)
        //         {
        //             return soundClip;
        //         }
        //     }
        //
        //     return default(SoundClip);
        // }
    }
}

