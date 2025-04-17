using UnityEngine;

namespace GameComponents.Scripts.MusicComponents
{
    public class GameAudio : MonoBehaviour
    {
        [Header("Background Music")]
        [SerializeField] private AudioSource _backgroundMusicAudioSource;
        [SerializeField] private AudioClip _backgroundMusicClip;

        private void Start()
        {
            if (_backgroundMusicAudioSource != null && _backgroundMusicClip != null)
            {
                _backgroundMusicAudioSource.clip = _backgroundMusicClip;
                _backgroundMusicAudioSource.loop = true;
                _backgroundMusicAudioSource.Play();
            }
        }
    }
}