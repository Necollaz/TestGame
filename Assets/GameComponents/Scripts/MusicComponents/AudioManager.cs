using UnityEngine;
using UnityEngine.Audio;

namespace GameComponents.Scripts.MusicComponents
{
    public class AudioManager : MonoBehaviour
    {
        public const string VolumeKey = "AllMusicVolume";
        
        [SerializeField] private float _defaultVolume = 0.75f;
        [SerializeField] private AudioMixer _audioMixer;
        
        private float _currentVolume;

        public float DefaultVolume => _defaultVolume;
        
        private void Awake()
        {
            float saved = PlayerPrefs.GetFloat(VolumeKey, _defaultVolume);
            
            SetVolume(saved, save: false);
        }

        public void SetVolume(float normalizedVolume, bool save = true)
        {
            _currentVolume = Mathf.Clamp(normalizedVolume, 0.0001f, 1f);
            float db = Mathf.Log10(_currentVolume) * 20f;
            
            _audioMixer.SetFloat(VolumeKey, db);

            if (save)
            {
                PlayerPrefs.SetFloat(VolumeKey, _currentVolume);
                PlayerPrefs.Save();
            }
        }
    }
}
