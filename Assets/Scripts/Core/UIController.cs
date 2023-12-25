using Scripts.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.Core
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;

        [Header("ProgressBar")] 
        [SerializeField] private int _sceneOffset;
        [SerializeField] private TMP_Text _nextLevelText;
        [SerializeField] private TMP_Text _currentLevelText;
        [SerializeField] private Image _fillProgress;
        
        [Space]
        [SerializeField] private GameObject _levelCompletedText;
        
        private void Awake()
        {
            if (Instance == null) 
                Instance = this;
        }

        private void Start()
        {
            _fillProgress.fillAmount = 0f;
            _levelCompletedText.SetActive(false);
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
            _fillProgress.fillAmount = value;
        }

        public void ShowLevelCompletedText()
        {
            _levelCompletedText.SetActive(true);
        }
    }
}