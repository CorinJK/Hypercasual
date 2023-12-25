using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Logic
{
    public class LevelController : MonoBehaviour
    {
        public static LevelController Instance;

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

        public void LoadNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}