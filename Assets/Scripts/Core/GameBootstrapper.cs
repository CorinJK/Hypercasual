using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            if (!GameStates.isStartGame)
            {
                GameStates.isStartGame = true;
                SceneManager.LoadScene(0);
                DontDestroyOnLoad(this);
            }
        }
    }
}