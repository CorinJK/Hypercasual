using UnityEngine;
using YG;

namespace ADS
{
    public class RewardAds : MonoBehaviour
    {
        [SerializeField] private YandexGame _sdk;

        public void ShowAd()
        {
            _sdk._RewardedShow(1);
        }
    }
}