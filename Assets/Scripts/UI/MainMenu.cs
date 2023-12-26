using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _mainMenuScreen;
        [SerializeField] private GameObject _settingsMenuScreen;
        
        public void StartGame()
        {
            int level = PlayerPrefs.GetInt("CurrentLevel");
            
            if (level == 0) 
                SceneManager.LoadScene(1);
            else 
                SceneManager.LoadScene(level);
        }
        
        public void OnShowSettings()
        {
            _mainMenuScreen.SetActive(false);
            _settingsMenuScreen.SetActive(true);
        }

        public void BackToMenu()
        {
            _settingsMenuScreen.SetActive(false);
            _mainMenuScreen.SetActive(true);
        }
        
        public void OnExit()
        {
                Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}