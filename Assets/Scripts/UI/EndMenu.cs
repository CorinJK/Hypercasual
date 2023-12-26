using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.UI
{
    public class EndMenu : MonoBehaviour
    {
        public void BackToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}