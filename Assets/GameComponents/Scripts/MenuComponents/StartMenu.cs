using UnityEngine;

namespace GameComponents.Scripts.MenuComponents
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private Canvas _menuCanvas;
        [SerializeField] private Canvas _gameCanvas;
        [SerializeField] private MonoBehaviour[] _gameplayBehaviours;

        private void Awake()
        {
            _gameCanvas.gameObject.SetActive(false);
            
            foreach(MonoBehaviour behaviour in _gameplayBehaviours)
            {
                behaviour.enabled = false;
            }
        }
        
        public void OnStartButtonPressed()
        {
            ShowGame();
        }
        
        public void ShowMainMenu()
        {
            _gameCanvas.gameObject.SetActive(false);
            _menuCanvas.gameObject.SetActive(true);

            foreach(MonoBehaviour behaviour in _gameplayBehaviours)
            {
                behaviour.enabled = false;
            }
        }
        
        private void ShowGame()
        {
            _menuCanvas.gameObject.SetActive(false);
            _gameCanvas.gameObject.SetActive(true);
            
            foreach (MonoBehaviour behaviour in _gameplayBehaviours)
            {
                behaviour.enabled = true;
            }
        }
    }
}