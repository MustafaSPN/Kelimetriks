using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;


public class Addressables : MonoBehaviour
{

    [SerializeField] private AssetReferenceSprite letterBackgroundAsset;
    [SerializeField] private AssetReferenceSprite BackgroundAsset;
    [SerializeField] private GameObject letterBackground;
    [SerializeField] private GameObject Background;
    
    void Start()
    {
        UnityEngine.AddressableAssets.Addressables.InitializeAsync().Completed += Addressables_Complated;
    }

    private void Addressables_Complated(AsyncOperationHandle<IResourceLocator> obj)
    {
        BackgroundAsset.LoadAssetAsync<Sprite>().Completed += (asset) =>
        {
            Background.GetComponent<SpriteRenderer>().sprite = BackgroundAsset.Asset as Sprite ;
        };
        
        letterBackgroundAsset.LoadAssetAsync<Sprite>().Completed += (asset) =>
        {
            letterBackground.GetComponent<SpriteRenderer>().sprite = letterBackgroundAsset.Asset as Sprite ;
        };
    }

}
