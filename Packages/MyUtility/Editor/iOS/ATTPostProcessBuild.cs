#if UNITY_IOS
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace iOS.Editor
{

    public static class ATTPostProcessBuild
    {
        private const string ATT_FRAMEWORK = "AppTrackingTransparency.framework";
        private const string ATT_USAGE = "NSUserTrackingUsageDescription";
        private const string LOCALIZATION_ARRAY_KEY = "CFBundleLocalizations";
        private const string TargetDirectory = "Unity-iPhone Tests";
        private const string TargetFolder = "Unity-iPhone Tests";
        private static readonly string LocalizationFolderPath = Path.Combine(Application.dataPath, "Editor/iOS/Localizations");

        /// <summary>ビルド後処理</summary>
        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
        {
            if (buildTarget != BuildTarget.iOS) { return; } // iOS専用
            #region edit project
            // read Project
            var pbxPath = PBXProject.GetPBXProjectPath(buildPath);
            var project = new PBXProject();
            project.ReadFromFile(pbxPath);
            // ATT
            project.AddFrameworkToProject(project._GetUnityFrameworkTargetGuid(), ATT_FRAMEWORK, true);
            #region localization
            // アセットのローカライズフォルダをプロジェクトへコピーし、リージョン・リストを作る
            var targetMainGuid = project._GetUnityMainTargetGuid();
            var targetFrameworkGuid = project._GetUnityFrameworkTargetGuid();
            var localizationFolders = Directory.GetDirectories(LocalizationFolderPath);
            var regions = new List<string>(); // リージョン・リスト
            var targetPath = Path.Combine(buildPath, TargetDirectory); // コピー先パス
            for (int i = 0; i < localizationFolders.Length; i++)
            {
                var folderName = Path.GetFileName(localizationFolders[i]); // フォルダ名
                var regionName = Path.GetFileNameWithoutExtension(localizationFolders[i]); // リージョン名
                CopyWithoutMeta(localizationFolders[i], targetPath); // アセットのフォルダをプロジェクトへコピー
                regions.Add(regionName); // リストにリージョンを追加
                                         // コピーしたフォルダをプロジェクトに登録
                var guid = project.AddFolderReference(Path.Combine(targetPath, folderName), Path.Combine(TargetFolder, folderName));
                if (targetMainGuid != null)
                {
                    project.AddFileToBuild(targetMainGuid, guid);
                }
                project.AddFileToBuild(targetFrameworkGuid, guid);
            }
            #region edit project with string
            // convert to string
            var pbxstr = project.WriteToString();
            // modify knownRegions
            pbxstr = Regex.Replace(pbxstr, @"(?<!\w)(developmentRegion\s*=\s*)English;", "$1en;", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            pbxstr = Regex.Replace(pbxstr,
                @"(?<!\w)(knownRegions\s*=\s*\()((?:[\s\w\-""]+,)*)?(\s*\)\s*;)",
                $"$1\n{string.Join("\n", regions.ConvertAll(region => $"\t\t\t\t{region},"))}\n$3",
                RegexOptions.Singleline | RegexOptions.IgnoreCase);
            // convert from string
            project.ReadFromString(pbxstr);
            #endregion
            #endregion
            // write Project
            project.WriteToFile(pbxPath);
            #endregion
            #region edit plist
            // read Info.plist
            var plistPath = Path.Combine(buildPath, "Info.plist");
            var plist = new PlistDocument();
            plist.ReadFromFile(plistPath);
            #region localization
            // リージョンを登録
            var lArray = plist.root.CreateArray(LOCALIZATION_ARRAY_KEY);
            foreach (var region in regions)
            {
                lArray.AddString(region);
            }
            #endregion
            // write Info.plist
            plist.WriteToFile(plistPath);
            #endregion
        }

        /// <summary>指定のアセットフォルダを('.meta'を除外して)コピーする</summary>
        /// <param name="spath">コピー元フォルダのフルパス (ノーチェック)</param>
        /// <param name="ddir">コピー先ディレクトリ (ノーチェック)</param>
        private static void CopyWithoutMeta(string spath, string ddir)
        {
            var dpath = Path.Combine(ddir, Path.GetFileName(spath));
            if (!Directory.Exists(dpath))
            {
                Directory.CreateDirectory(dpath);
            }
            foreach (var file in Directory.GetFiles(spath))
            {
                if (!file.EndsWith(".meta"))
                {
                    var dest = Path.Combine(dpath, Path.GetFileName(file));
                    File.Copy(file, dest, true);
                }
            }
        }

        /// <summary>Returns GUID of the framework target in Unity project.</summary>
        private static string _GetUnityFrameworkTargetGuid(this PBXProject project)
        {
#if UNITY_2019_3_OR_NEWER
            return project.GetUnityFrameworkTargetGuid();
#else
        return project.TargetGuidByName (PBXProject.GetUnityTargetName ());
#endif
        }
        /// <summary>Returns GUID of the main target in Unity project.</summary>
        private static string _GetUnityMainTargetGuid(this PBXProject project)
        {
#if UNITY_2019_3_OR_NEWER
            return project.GetUnityMainTargetGuid();
#else
        return null;
#endif
        }
    }
}
#endif