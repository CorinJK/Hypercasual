using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

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
        
        private void Awake()
        {
            if (Instance == null) 
                Instance = this;
        }

        private void Start()
        {
            CountObjects();
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
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            PlayerPrefs.SetInt("CurrentLevel", nextSceneIndex);
            SceneManager.LoadScene(nextSceneIndex);
            
            YandexGame.NewLeaderboardScores("LeaderboardLevel", nextSceneIndex);
        }
        
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}