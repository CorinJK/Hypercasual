using DG.Tweening;
using Scripts.Core;
using Scripts.UI;
using UnityEngine;

namespace Scripts.Logic
{
    public class BottomCollision : MonoBehaviour
    {
        private readonly string _objectTag = "Object";
        private readonly string _obstacleTag = "Obstacle";

        [SerializeField] private AudioClip _catchAudio;
        [SerializeField] private AudioClip _victoryAudio;
        [SerializeField] private AudioClip _lossAudio;
        
        private float _gameOverCooldown = 1f;
        private float _cooldownTimer = Mathf.Infinity;

        private bool _isComplete = false;

        private void Update()
        {
            _cooldownTimer += Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isComplete)
                return;
            
            string tag = other.tag;
            LevelController level = LevelController.Instance;
            AudioController audio = AudioController.Instance;
            HUDController hud = HUDController.Instance;

            Destroy(other.gameObject);
            Magnet.Instance.RemoveFromMagnetField(other.attachedRigidbody);
            
            if (tag.Equals(_objectTag))
            {
                level.ObjectsInScene--;
                hud.UpdateLevelProgress();
                audio.PlaySound(_catchAudio);
                
                if (level.ObjectsInScene == 0)
                {
                    hud.ShowLevelCompletedText();
                    level.PlayWinParticle();
                    audio.PlaySound(_victoryAudio);
                    
                    _isComplete = true;
                    GameStates.isStop = true;
                    Invoke("NextLevel", 2f);
                }
            }
            
            if (tag.Equals(_obstacleTag) && _cooldownTimer > _gameOverCooldown)
            {
                GameStates.isStop = true;
                audio.PlaySound(_lossAudio);
                _cooldownTimer = 0;
                
                if (Camera.main != null)
                    Camera.main.transform
                        .DOShakePosition(1f, 0.1f, 20, 90f)
                        .OnComplete(() => { hud.ShowRestartMenu(); });
            }
        }

        private void NextLevel()
        {
            LevelController.Instance.LoadNextLevel();
        }
    }
}