using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

namespace Scripts.UI
{
    public class SettingsScreen : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _mixer;
        
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Slider _slider;

        private void Start()
        {
            _toggle.isOn =  PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
            _slider.value =  PlayerPrefs.GetFloat("MasterVolume", 1);
        }

        public void ToggleMusic(bool enabled)
        {
            if (enabled) 
                _mixer.audioMixer.SetFloat("MusicVolume", 0);
            else 
                _mixer.audioMixer.SetFloat("MusicVolume", -80);
            
            PlayerPrefs.SetInt("MusicEnabled", enabled ? 1 : 0);
        }

        public void ChangeVolume(float volume)
        {
            _mixer.audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
            
            PlayerPrefs.SetFloat("MasterVolume", volume);
        }

        public void SetLanguageRu()
        {
            YandexGame.SwitchLanguage("ru");
        }
        
        public void SetLanguageEn()
        {
            YandexGame.SwitchLanguage("en");
        }
        
        public void SetLanguageTr()
        {
            YandexGame.SwitchLanguage("tr");
        }
    }
}