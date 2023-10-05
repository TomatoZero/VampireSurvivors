using System.IO;
using UnityEngine;

namespace DefaultNamespace
{
    public class LoadFromAssetBundle : MonoBehaviour
    {
        private string _assetBundlePath;
        private string _assetBundleFolderPath;
        private string _assetBundleManifestPath;

        private void OnEnable()
        {
            _assetBundleManifestPath = $"{Application.streamingAssetsPath}/StreamingAssets";
            _assetBundlePath =
                Path.Combine(Application.streamingAssetsPath, "weaponsassetbundle");
            _assetBundleFolderPath = Path.Combine(Application.streamingAssetsPath);
        }

        public GameObject LoadPrefab(string bundleName, string prefabName)
        {
            SetupPath(bundleName, prefabName);
            
            var assetBundle = LoadFromDisk();
            
            if (assetBundle == null) {
                Debug.Log("Failed to load AssetBundle!");
                return new GameObject();
            }
            
            return LoadPrefab(assetBundle, prefabName);
        }

        public ScriptableObject LoadScriptableObject(string bundleName, string objectName)
        {
            SetupPath(bundleName, objectName);
            var assetBundle = LoadFromDisk();
            
            if (assetBundle == null) {
                Debug.Log("Failed to load AssetBundle!");
                return null;
            }

            return LoadScriptableObject(assetBundle, objectName);
        }

        private AssetBundle LoadFromDisk()
        {
            LoadDependencies("weaponsassetbundle");
            Debug.LogWarning(_assetBundlePath);
            return AssetBundle.LoadFromFile(_assetBundlePath);
        }

        private AssetBundle LoadDependencies(string dependenciesFor)
        {
            var weaponManifest = AssetBundle.LoadFromFile(_assetBundleManifestPath);
            var manifest = weaponManifest.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

            var dependencies = manifest.GetAllDependencies(dependenciesFor);
            foreach (var dependency in dependencies)
            {
                AssetBundle.LoadFromFile(Path.Combine(_assetBundleFolderPath, dependency));
            }

            return weaponManifest;
        }
        
        private GameObject LoadPrefab(AssetBundle myLoadedAssetBundle, string fileName)
        {
            return myLoadedAssetBundle.LoadAsset<GameObject>(fileName);
        }

        private ScriptableObject LoadScriptableObject(AssetBundle myLoadedAssetBundle, string fileName)
        {
            return myLoadedAssetBundle.LoadAsset<ScriptableObject>(fileName);
        }

        private void SetupPath(string bundleName, string prefabName)
        {
            _assetBundlePath = $"{Application.streamingAssetsPath}/{bundleName}";
            _assetBundleFolderPath = $"{Application.streamingAssetsPath}";
        }
    }
}