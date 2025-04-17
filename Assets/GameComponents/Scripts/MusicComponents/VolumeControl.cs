using UnityEngine;
using UnityEngine.Audio;

namespace GameComponents.Scripts.MusicComponents
{
    public class VolumeControl
    {
        private readonly AudioMixer _mixer;

        public VolumeControl(AudioMixer mixer)
        {
            _mixer = mixer;
        }
        
        public void SetVolume(string parameterName, float volume)
        {
            float clamped = Mathf.Clamp(volume, 0.0001f, 1f);
            float dB = Mathf.Log10(clamped) * 20f;
            
            _mixer.SetFloat(parameterName, dB);
        }
    }
}
