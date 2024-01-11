using DG.Tweening;
using Scripts.Logic;
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

        private float _durationShowText = 0.4f;

        private void Awake()
        {
            if (Instance == null) 
                Instance = this;
        }

        private void Start()
        {
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
    }
}