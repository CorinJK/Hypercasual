﻿using DG.Tweening;
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
        
        private void OnTriggerEnter(Collider other)
        {
            if (GameStates.isGameOver)
                return;
            
            string tag = other.tag;
            LevelController level = LevelController.Instance;
            AudioController audio = AudioController.Instance;
            HUDController hud = HUDController.Instance;
            Magnet magnet = Magnet.Instance;

            Destroy(other.gameObject);
            
            if (tag.Equals(_objectTag))
            {
                level.ObjectsInScene--;
                hud.UpdateLevelProgress();
                magnet.RemoveFromMagnetField(other.attachedRigidbody);
                audio.PlaySound(_catchAudio);
                
                if (level.ObjectsInScene == 0)
                {
                    hud.ShowLevelCompletedText();
                    level.PlayWinParticle();
                    audio.PlaySound(_victoryAudio);
                    
                    GameStates.isStop = true;
                    Invoke("NextLevel", 2f);
                }
            }
            
            if (tag.Equals(_obstacleTag))
            {
                GameStates.isGameOver = true;
                audio.PlaySound(_lossAudio);
                
                if (Camera.main != null)
                    Camera.main.transform
                        .DOShakePosition(1f, 0.1f, 20, 90f)
                        .OnComplete(() => { level.RestartLevel(); });
            }
        }

        private void NextLevel()
        {
            LevelController.Instance.LoadNextLevel();
        }
    }
}