#if UNITY_IOS
using System;
using System.Collections;
using Unity.Advertisement.IosSupport;
using UnityEngine;
using UnityEngine.iOS;

namespace MoNo.Utility
{

    public class AppTrackingTransparencyCheck
    {
        public IEnumerator Check()
        {
            // identify iOS version due to avoid less than iOS 14.5 
            var iOSVersion = GetiOSVersion();
            Debug.Log("iOS version : " + iOSVersion);
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
            Debug.Log("Advertising Identifier : " + Device.advertisingIdentifier);
        }


        /// <summary>
        /// To get the devise's iOS version
        /// </summary>
        /// <returns>iOS version</returns>
        static float GetiOSVersion()
        {
            if (float.TryParse(Device.systemVersion, out var version))
            {
                return version;
            }
            return 0;
        }
    }
}

#endif