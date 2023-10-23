using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class LoadFromAssetBundle : MonoBehaviour
    {
        private string _assetBundlePath;
        private string _assetBundleFolderPath;
        private string _assetBundleManifestPath;

        private List<AssetBundle> _loadedAssetBundles;
        private char _separator;

        private void Awake()
        {
            _loadedAssetBundles = new List<AssetBundle>();
            _separator = Path.AltDirectorySeparatorChar;
        }

        private void OnEnable()
        {
            _assetBundleManifestPath =
                $"{Application.streamingAssetsPath}{_separator}AssetsBundle{_separator}AssetsBundle";
            _assetBundlePath =
                $"{Application.streamingAssetsPath}{_separator}AssetsBundle{_separator}weaponsassetbundle";
            _assetBundleFolderPath = $"{Application.streamingAssetsPath}{_separator}AssetsBundle";


            Debug.Log($"_assetBundleManifestPath: {_assetBundleManifestPath}\n" +
                      $"_assetBundlePath: {_assetBundlePath}\n" +
                      $"_assetBundleFolderPath: {_assetBundleFolderPath}");

            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        public GameObject LoadPrefab(string bundleName, string prefabName)
        {
            var assetBundle = FindFromLoaded(bundleName);

            if (assetBundle is null)
            {
                SetupPath(bundleName);
                assetBundle = LoadFromDisk(bundleName);

                if (assetBundle == null)
                {
                    Debug.Log("Failed to load AssetBundle!");
                    return new GameObject();
                }

                _loadedAssetBundles.Add(assetBundle);
            }

            var prefab = LoadPrefab(assetBundle, prefabName);
            return prefab;
        }

        public ScriptableObject LoadScriptableObject(string bundleName, string objectName)
        {
            var assetBundle = FindFromLoaded(bundleName);

            if (assetBundle is null)
            {
                SetupPath(bundleName);
                assetBundle = LoadFromDisk(bundleName);

                if (assetBundle == null)
                {
                    Debug.Log("Failed to load AssetBundle!");
                    return null;
                }
            }

            var scriptable = LoadScriptableObject(assetBundle, objectName);
            // UnloadDependencies();
            return scriptable;
        }

        private AssetBundle LoadFromDisk(string bundleName)
        {
            _loadedAssetBundles.Add(LoadDependencies(bundleName));
            return AssetBundle.LoadFromFile(_assetBundlePath);
        }

        private AssetBundle LoadDependencies(string dependenciesFor)
        {
            var weaponManifest = AssetBundle.LoadFromFile(_assetBundleManifestPath);
            var manifest = weaponManifest.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

            var dependencies = manifest.GetAllDependencies(dependenciesFor);
            foreach (var dependency in dependencies)
            {
                _loadedAssetBundles.Add(AssetBundle.LoadFromFile($"{_assetBundleFolderPath}{_separator}{dependency}"));
            }

            return weaponManifest;
        }

        private void OnSceneUnloaded(Scene unloadedScene)
        {
            UnloadDependencies();
        }

        private void UnloadDependencies()
        {
            foreach (var bundle in _loadedAssetBundles)
            {
                bundle.UnloadAsync(false);
            }

            _loadedAssetBundles = new List<AssetBundle>();
        }

        private AssetBundle FindFromLoaded(string name)
        {
            foreach (var bundle in _loadedAssetBundles)
            {
                if (bundle.name == name) return bundle;
            }

            return null;
        }

        private GameObject LoadPrefab(AssetBundle myLoadedAssetBundle, string fileName)
        {
            return myLoadedAssetBundle.LoadAsset<GameObject>(fileName);
        }

        private ScriptableObject LoadScriptableObject(AssetBundle myLoadedAssetBundle, string fileName)
        {
            return myLoadedAssetBundle.LoadAsset<ScriptableObject>(fileName);
        }

        private void SetupPath(string bundleName)
        {
            _assetBundlePath =
                $"{Application.streamingAssetsPath}{_separator}AssetsBundle{_separator}{bundleName}";
            _assetBundleFolderPath = $"{Application.streamingAssetsPath}{_separator}AssetsBundle";
        }
    }
}