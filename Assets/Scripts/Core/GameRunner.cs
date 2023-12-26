using UnityEngine;

namespace Scripts.Core
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper BootstrapperPrefab;

        private void Awake()
        {
            GameBootstrapper bootstriper = FindObjectOfType<GameBootstrapper>();

            if(bootstriper == null)
                Instantiate(BootstrapperPrefab);
        }
    }
}