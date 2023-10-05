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
            _assetBundleManifestPath = Path.Combine(Application.streamingAssetsPath, "AssetBundles");
            _assetBundlePath =
                Path.Combine(Application.streamingAssetsPath, "AssetBundles", "weaponsassetbundle");
            _assetBundleFolderPath = Path.Combine(Application.streamingAssetsPath, "AssetBundles");
        }

        public GameObject LoadPrefab(string bundleName, string prefabName)
        {
            _assetBundlePath =
                Path.Combine(Application.streamingAssetsPath, "AssetBundles", bundleName);
            _assetBundleFolderPath = Path.Combine(Application.streamingAssetsPath, "AssetBundles");
            
            var assetBundle = LoadFromDisk();
            
            if (assetBundle == null) {
                Debug.Log("Failed to load AssetBundle!");
                return new GameObject();
            }
            
            return LoadPrefab(assetBundle, prefabName);
        }

        private AssetBundle LoadFromDisk()
        {
            LoadDependencies("weaponsassetbundle");
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
    }
}