using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameComponents.Scripts.Menu
{
    public class PauseGame : MonoBehaviour, IMenuToggle
    {
        [SerializeField] private StartMenu _startMenu;
        [SerializeField] private Image _settingsMenu;

        private float _previousTimeScale;

        private void Awake()
        {
            _settingsMenu.gameObject.SetActive(false);
        }
        
        public void OpenMenu()
        {
            _settingsMenu.gameObject.SetActive(true);
            
            _previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }
        
        public void CloseMenu()
        {
            _settingsMenu.gameObject.SetActive(false);
            
            Time.timeScale = _previousTimeScale;
        }
        
        public void ExitToMainMenu()
        {
            Time.timeScale = 1f;
            
            _settingsMenu.gameObject.SetActive(false);
            _startMenu.ShowMainMenu();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}