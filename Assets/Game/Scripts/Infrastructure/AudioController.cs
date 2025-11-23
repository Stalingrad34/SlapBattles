using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Game.Scripts.Infrastructure
{
    public class AudioController : SerializedMonoBehaviour
    {
        public static AudioController Instance;
        
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private List<AudioSource> soundSources;
        [SerializeField] private float volumeDownDuration = 3;
        [SerializeField] private float volumeUpDuration = 3;
        [SerializeField] private float soundPower;
        [SerializeField] private float musicPower;
    
        private Dictionary<string, AudioClip> _soundMap;
        private Dictionary<string, float> _soundTimes = new();
        private Tween _musicVolumeTween;
        private Sequence _musicSequence;
        private Sequence _soundSequence;
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
            _soundMap = GetSounds();
        }
        
        public void Init()
        {
            musicSource.volume = musicPower;
            //ServiceProvider.Get<DatabaseProvider>().MusicOff.Subscribe(OnMusicOffChanged).AddTo(gameObject);
        }

        private void OnMusicOffChanged(bool off)
        {
            musicSource.mute = off;
        }

        public void PlayAudioClipFromSoundMap(string sound, bool loop = false, float volumeRatio = 1, float delay = 0)
        {
            if (string.IsNullOrEmpty(sound))
                return;

            if (!_soundMap.ContainsKey(sound))
            {
                Debug.LogError("SoundMap not contains " + sound);
                return;
            }
        
            /*if (ServiceProvider.Get<DatabaseProvider>().SoundOff.Value)
                return;*/

            if (volumeRatio > 1)
                volumeRatio = 1;
            else if (volumeRatio < 0)
                volumeRatio = 0;

            if (_soundTimes.TryGetValue(sound, out var time) && Time.realtimeSinceStartup - time < 0.1f)
            {
                return;
            }
            _soundTimes[sound] = Time.realtimeSinceStartup;

            var freeSoundSource = soundSources.FirstOrDefault(src => !src.isPlaying);
            if (freeSoundSource == default)
            {
                Debug.LogWarning("All AudioSources is busy for " + sound);
                return;
            }
        
            freeSoundSource.loop = loop;
            freeSoundSource.clip = _soundMap[sound];
            freeSoundSource.volume = soundPower * volumeRatio;
            if (delay > 0)
                freeSoundSource.PlayDelayed(delay);
            else
                freeSoundSource.Play();
        }
        
        public void PlayMusic(string musicName)
        {
            if (!_soundMap.ContainsKey(musicName))
            {
                Debug.LogError("MusicMap not contains " + musicName);
                return;
            }

            if (musicSource.isPlaying)
            {
                if (musicSource.clip == _soundMap[musicName])
                    return;
            
                SmoothlyVolumeDown(musicSource, volumeDownDuration, callback:() =>
                {
                    musicSource.clip = _soundMap[musicName];
                    musicSource.Play();
                    SmoothlyVolumeUp(musicSource, volumeUpDuration, musicPower);
                });
            }
            else
            {
                musicSource.clip = _soundMap[musicName];
                musicSource.volume = 0;
                musicSource.Play();
                SmoothlyVolumeUp(musicSource, volumeUpDuration,musicPower);
            }
        }
    
        public void StopPlayedSound(string sound, bool smoothly = false)
        {
            var playedSources = soundSources.Where(src => src.isPlaying && src.clip == _soundMap[sound]);
            if (smoothly)
            {
                foreach (var playedSource in playedSources)
                {
                    SmoothlyVolumeDown(playedSource, volumeDownDuration);
                }
            }
            else
            {
                foreach (var playedSource in playedSources)
                {
                    playedSource.Stop();
                }
            }
        }

        public void MusicSmoothlyVolumeDown(float volumeRation) =>
            SmoothlyVolumeDown(musicSource, volumeDownDuration, musicPower * volumeRation);
    
        public void MusicSmoothlyVolumeUp() =>
            SmoothlyVolumeUp(musicSource, volumeUpDuration, musicPower);

        private void SmoothlyVolumeDown(AudioSource source, float duration, float endValue = 0, Action callback = null)
        {
            _musicSequence?.Kill();
            _musicSequence = DOTween.Sequence();
            _musicSequence.Append(source.DOFade(endValue, duration)).SetLink(gameObject).onComplete = () => callback?.Invoke();
        }

        private void SmoothlyVolumeUp(AudioSource source, float duration, float endValue = 1)
        {
            _musicSequence?.Kill();
            _musicSequence = DOTween.Sequence();
            _musicSequence.Append(source.DOFade(endValue, duration)).SetLink(gameObject);
        }

        public void PlayMusic()
        {
            
        }
    
        public void PauseMusic()
        {
            if (musicSource.isPlaying)
                musicSource.Pause();
        }
    
        public void ResumeMusic()
        {
            if (!musicSource.isPlaying)
            {
                musicSource.UnPause();
            }
        }

        public void StopMusic(bool isSmootly = true)
        {
            if (!musicSource.isPlaying)
                return;

            if (isSmootly)
                SmoothlyVolumeDown(musicSource, volumeDownDuration, callback: musicSource.Stop);
            else
                musicSource.Stop();
        }
        
        private Dictionary<string, AudioClip> GetSounds()
        {
            var result = new Dictionary<string, AudioClip>();
            var allSounds = AssetProvider.GetAllSounds();
            foreach (var sound in allSounds)
            {
                result.Add(sound.name, sound);
            }

            return result;
        }
    }
}
