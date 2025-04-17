using UnityEngine;
using UnityEngine.Audio;

namespace GameComponents.Scripts.MusicComponents
{
    public class GameAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _backgroundMusicAudioSource;
        [SerializeField] private AudioClip _backgroundMusicClip;
        [SerializeField] private AudioMixerGroup _mixerGroup;
        
        private void Start()
        {
            if(_backgroundMusicAudioSource == null || _backgroundMusicClip == null)
            {
                return;
            }

            if(_mixerGroup != null)
            {
                _backgroundMusicAudioSource.outputAudioMixerGroup = _mixerGroup;
            }

            _backgroundMusicAudioSource.clip = _backgroundMusicClip;
            _backgroundMusicAudioSource.loop = true;
            _backgroundMusicAudioSource.Play();
        }
    }
}