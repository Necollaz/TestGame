using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace GameComponents.Scripts.MusicComponents
{
    public class SettingMusic : MonoBehaviour
    {
        private const string AllParam = "AllMusicVolume";
    
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private Slider _generalSlider;

        private VolumeControl _volumeControl;

        private void Awake()
        {
            _volumeControl = new VolumeControl(_mixer);
        }

        private void OnEnable()
        {
            InitSlider(AllParam, _generalSlider, 0.75f);
        }

        private void OnDisable()
        {
            _generalSlider.onValueChanged.RemoveAllListeners();
        }

        private void InitSlider(string param, Slider slider, float defaultVol)
        {
            float saved = PlayerPrefs.GetFloat(param, defaultVol);
            slider.value = saved;
            
            _volumeControl.SetVolume(param, saved);

            slider.onValueChanged.AddListener(v => { _volumeControl.SetVolume(param, v); PlayerPrefs.SetFloat(param, v); PlayerPrefs.Save(); });
        }
    }
}