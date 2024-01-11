using UnityEngine;

namespace Scripts.Logic
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;

        public static AudioController Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != null && Instance != this)
                Destroy(gameObject);
        }

        public void PlaySound(AudioClip sound)
        {
            _source.PlayOneShot(sound);
        }
    }
}