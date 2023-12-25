using Scripts.Core;
using UnityEngine;

namespace Scripts.Logic
{
    public class BottomCollision : MonoBehaviour
    {
        private readonly string _objectTag = "Object";
        private readonly string _obstacleTag = "Obstacle";
        
        private void OnTriggerEnter(Collider other)
        {
            if (GameStates.isGameOver)
                return;
            
            string tag = other.tag;
            LevelController level = LevelController.Instance;
            UIController ui = UIController.Instance;
            
            if (tag.Equals(_objectTag))
            {
                level.ObjectsInScene--;
                ui.UpdateLevelProgress();
                Destroy(other.gameObject);

                if (level.ObjectsInScene == 0)
                {
                    ui.ShowLevelCompletedText();
                    Invoke("NextLevel", 2f);
                }
            }
            
            if (tag.Equals(_obstacleTag))
            {
                GameStates.isGameOver = true;
                level.RestartLevel();
            }
        }

        private void NextLevel()
        {
            LevelController.Instance.LoadNextLevel();
        }
    }
}