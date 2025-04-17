using UnityEngine;
using UnityEngine.UI;

namespace GameComponents.Scripts.Music
{
    public class SettingMusic : MonoBehaviour
    {
        [SerializeField] private Slider _generalSlider;
        [SerializeField] private AudioManager _audioManager;

        private void OnEnable()
        {
            float normalized = PlayerPrefs.GetFloat(AudioManager.VolumeKey, _audioManager.DefaultVolume);
            _generalSlider.value = normalized;
            
            _audioManager.SetVolume(normalized, save: false);
            _generalSlider.onValueChanged.AddListener(OnSliderChanged);
        }

        private void OnDisable()
        {
            _generalSlider.onValueChanged.RemoveListener(OnSliderChanged);
        }

        private void OnSliderChanged(float value)
        {
            _audioManager.SetVolume(value, save: true);
        }
    }
}