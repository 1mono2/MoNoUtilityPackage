#if UNITY_IOS
using System;
using System.Collections;
using Unity.Advertisement.IosSupport;
using UnityEngine;
using UnityEngine.iOS;
using GoogleMobileAds.Placement;
using GoogleMobileAds.Api;

public class Example : MonoBehaviour
{
    private IEnumerator Start()
    {
        // idntify iOS version due to avoid less than iOS 14.5 
        var iOSVersion = GetiOSVersion();
        Debug.Log(iOSVersion);
        if (iOSVersion >= 14.5)
        {
            // まだ許可ダイアログを表示したことがない場合
            if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() ==
             ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
            // 許可ダイアログを表示します
            ATTrackingStatusBinding.RequestAuthorizationTracking();

            // 許可ダイアログで「App にトラッキングしないように要求」か
            // 「トラッキングを許可」のどちらかが選択されるまで待機します
                while (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() ==
                        ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
                {
                    yield return null;
                }
            }
        }

        // IDFA（広告 ID）をログ出力します
        // トラッキングが許可されている場合は IDFA が文字列で出力されます
        // 許可されていない場合は「00000000-0000-0000-0000-000000000000」が出力されます  
        Debug.Log(Device.advertisingIdentifier);


        //var attStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
        //if(ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
        //{
        //    RequestBannerAd();
        //}
        //else
        //{
        //    var builder = new AdRequest.Builder();
        //    builder = builder.AddExtra("npa", "1");
        //}

        RequestBannerAd();
    }

    void RequestBannerAd()
    {
        BannerAdGameObject bannerAd = MobileAds.Instance.GetAd<BannerAdGameObject>("Banner Ad");
        bannerAd.LoadAd();

    }

    /// <summary>
    /// To get the devise's iOS version
    /// </summary>
    /// <returns>iOS version</returns>
    static float GetiOSVersion()
    {
        float version;
        if(float.TryParse(Device.systemVersion, out version))
        {
            return version;
        }
        return 0;
    }
}


#endif