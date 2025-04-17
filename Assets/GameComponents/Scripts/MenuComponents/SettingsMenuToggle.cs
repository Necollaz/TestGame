using UnityEngine;
using UnityEngine.UI;

namespace GameComponents.Scripts.MenuComponents
{
    public class SettingsMenuToggle : MonoBehaviour, IMenuToggle
    {
        [SerializeField] private Image _settingsMenuCanvas;

        private void Start()
        {
            CloseMenu();
        }

        public void OpenMenu()
        {
            _settingsMenuCanvas.gameObject.SetActive(true);
        }
        
        public void CloseMenu()
        {
            _settingsMenuCanvas.gameObject.SetActive(false);
        }
    }
}