using DG.Tweening;
using Scripts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class HUDController : MonoBehaviour
    {
        public static HUDController Instance;

        [Header("ProgressBar")] 
        [SerializeField] private int _sceneOffset;
        [SerializeField] private TMP_Text _nextLevelText;
        [SerializeField] private TMP_Text _currentLevelText;
        [SerializeField] private Image _fillProgress;
        
        [Space]
        [SerializeField] private TMP_Text _levelCompletedText;

        [Space] 
        [SerializeField] private Image _fadePanel;
        
        private float _durationShowText = 0.4f; 
        
        [Header("PauseScreen")]
        [SerializeField] private GameObject _pauseScreen;
        
        private void Awake()
        {
            if (Instance == null) 
                Instance = this;
            
            _pauseScreen.SetActive(false);
        }

        private void Start()
        {
            StartFade();
            _fillProgress.fillAmount = 0f;
            SetLevelProgressText();
        }

        private void SetLevelProgressText()
        {
            int level = SceneManager.GetActiveScene().buildIndex + _sceneOffset;
            _currentLevelText.text = level.ToString();
            _nextLevelText.text = (level + 1).ToString();
        }

        public void UpdateLevelProgress()
        {
            LevelController level = LevelController.Instance;
            float value = 1f - ((float)level.ObjectsInScene / level.TotalObjects);
            _fillProgress.DOFillAmount(value, _durationShowText);
        }

        public void ShowLevelCompletedText()
        {
            _levelCompletedText.DOFade(1f, 0.8f).From(0);
        }

        public void ShowPauseScreen()
        {
            Time.timeScale = 0;
            _pauseScreen.SetActive(true);
        }
        
        public void HidePauseScreen()
        {
            Time.timeScale = 1;
            _pauseScreen.SetActive(false);
        }

        public void BackToMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
        
        public void OnReset()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }     
        
        public void OnExit()
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        
        private void StartFade()
        {
            _fadePanel.DOFade(0, 1.3f).From(1f);
        }
    }
}