using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace GameComponents.Scripts.Music
{
    public class AudioPauseOnFocus : MonoBehaviour
    {
        private const string VolumeKey = AudioManager.VolumeKey;

        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private AudioMixer _audioMixer;
        
        private float _originalDb;

        private void Awake()
        {
            LoadAndApplyVolume();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            LoadAndApplyVolume();
        }

        private void LoadAndApplyVolume()
        {
            float defaultVol = _audioManager != null ? _audioManager.DefaultVolume : 0.75f;
            float saved = PlayerPrefs.GetFloat(VolumeKey, defaultVol);
            float clamped = Mathf.Clamp(saved, 0.0001f, 1f);
            _originalDb = Mathf.Log10(clamped) * 20f;

            _audioMixer.SetFloat(VolumeKey, _originalDb);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            _audioMixer.SetFloat(VolumeKey, hasFocus ? _originalDb : -80f);
        }

        private void OnApplicationPause(bool isPaused)
        {
            _audioMixer.SetFloat(VolumeKey, isPaused ? -80f : _originalDb);
        }
    }
}