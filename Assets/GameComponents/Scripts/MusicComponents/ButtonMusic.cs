using UnityEngine;

namespace GameComponents.Scripts.MusicComponents
{
    [RequireComponent(typeof(AudioSource))]
    public class ButtonMusic : MonoBehaviour
    {
        [SerializeField] private AudioClip _clickClip;

        private AudioSource _audio;
        
        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
        }

        public void Click()
        {
            if (_clickClip != null)
            {
                _audio.PlayOneShot(_clickClip);
            }
            else
            {
                _audio.Play();
            }
        }
    }
}