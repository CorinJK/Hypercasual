using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.Logic
{
    public class LevelController : MonoBehaviour
    {
        public static LevelController Instance;

        [Space]
        [SerializeField] private ParticleSystem _winParticle;
        
        [SerializeField] private Transform _objectsParent;
        public int ObjectsInScene;
        public int TotalObjects;

        [Header("Level Objects & Obstacle")] 
        [SerializeField] private Material _areaMaterial;
        [SerializeField] private SpriteRenderer _areaFrameSprite;
        [SerializeField] private SpriteRenderer _areaSideSprite;
        [Space]
        [SerializeField] private Material _objectMaterial;
        [SerializeField] private Material _obstacleMaterial;
        [Space]
        [SerializeField] private Image _progressFillImage;
        [Space]
        [SerializeField] private SpriteRenderer _backgroundSprite;

        [Header("Level Colors")] 
        [SerializeField] private Color _areaColor;
        [SerializeField] private Color _areaFrameColor;
        [SerializeField] private Color _areaSideColor;
        [Space]
        [SerializeField] private Color _objectColor;
        [SerializeField] private Color _obstacleColor;
        [Space]
        [SerializeField] private Color _progressFillColor;
        [Space]
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private Color _cameraColor;
        
        private void Awake()
        {
            if (Instance == null) 
                Instance = this;
        }

        private void Start()
        {
            CountObjects();
            UpdateLevelColors();
        }

        private void CountObjects()
        {
            TotalObjects = _objectsParent.childCount;
            ObjectsInScene = TotalObjects;
        }

        public void PlayWinParticle()
        {
            _winParticle.Play();
        }
        
        public void LoadNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void UpdateLevelColors()
        {
            _areaMaterial.color = _areaColor;
            _areaFrameSprite.color = _areaFrameColor;
            _areaSideSprite.color = _areaSideColor;
            
            _objectMaterial.color = _objectColor;
            _obstacleMaterial.color = _obstacleColor;

            _progressFillImage.color = _progressFillColor;
            
            _backgroundSprite.color = _backgroundColor;
            Camera.main.backgroundColor = _cameraColor;
        }

        private void OnValidate()
        {
            UpdateLevelColors();
        }
    }
}