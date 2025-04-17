using UnityEngine;
using UnityEngine.Audio;

namespace GameComponents.Scripts.MusicComponents
{
    public class AudioPauseOnFocus : MonoBehaviour
    {
        private const string AllParam = "AllMusicVolume";

        [SerializeField] private AudioMixer _audioMixer;
        
        private float _originalDb;

        private void Awake()
        {
            if(!_audioMixer.GetFloat(AllParam, out _originalDb))
            {
                _originalDb = 0f;
            }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            _audioMixer.SetFloat(AllParam, hasFocus ? _originalDb : -80f);
        }

        private void OnApplicationPause(bool isPaused)
        {
            _audioMixer.SetFloat(AllParam, isPaused ? -80f : _originalDb);
        }
    }
}