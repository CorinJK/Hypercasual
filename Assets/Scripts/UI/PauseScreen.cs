﻿using DG.Tweening;
using Scripts.Logic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class PauseScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseScreen;
        [SerializeField] private Image _fadePanel;

        [Space] 
        [SerializeField] private AudioClip _clickAudio;

        private void Awake()
        {
            _pauseScreen.SetActive(false);
        }

        private void Start()
        {
            StartFade();
        }

        public void ShowPauseScreen()
        {
            Time.timeScale = 0;
            PlayClickAudio();
            _pauseScreen.SetActive(true);
        }
        
        public void HidePauseScreen()
        {
            Time.timeScale = 1;
            PlayClickAudio();
            _pauseScreen.SetActive(false);
        }

        public void BackToMenu()
        {
            Time.timeScale = 1;
            PlayClickAudio();
            SceneManager.LoadScene(0);
        }
        
        public void OnReset()
        {
            Time.timeScale = 1;
            PlayClickAudio();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }     
        
        public void OnExit()
        {
            AudioController.Instance.PlaySound(_clickAudio);
            
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        
        private void StartFade()
        {
            _fadePanel.DOFade(0, 1f).From(1f);
        }

        private void PlayClickAudio()
        {
            AudioController.Instance.PlaySound(_clickAudio);
        }
    }
}