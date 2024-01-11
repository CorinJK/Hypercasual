using Scripts.Logic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _mainMenuScreen;
        [SerializeField] private GameObject _settingsMenuScreen;
        
        [SerializeField] private AudioClip _clickAudio;
        
        public void StartGame()
        {
            int level = PlayerPrefs.GetInt("CurrentLevel");
            PlaySoundClick();
            
            if (level == 0) 
                SceneManager.LoadScene(1);
            else 
                SceneManager.LoadScene(level);
        }

        public void PlaySoundClick()
        {
            AudioController.Instance.PlaySound(_clickAudio);
        }
        
        public void OnExit()
        {
            PlaySoundClick();
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}