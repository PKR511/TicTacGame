using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    Coroutine corutine;
   public  GameObject parentObj;
   

    public string bundleUrl = "https://drive.google.com/u/0/uc?id=1xHBHbdno7utGt2cHlsObgN7K9dAxpuYv&export=download";
    public string assetName = "image";

    string imglink = "https://drive.google.com/uc?export=download&id=1xHBHbdno7utGt2cHlsObgN7K9dAxpuYv";
    string scenelink = "https://drive.google.com/uc?export=download&id=1iR5vIk_bz0-FF97RVjNH-GCQ001MyIKH";
    public void PlayGame()
    {
        Debug.Log("Play Game");
        corutine = StartCoroutine(LoadAssets());
    }





    // Start is called before the first frame update
    IEnumerator LoadAssets()
    {
        using (WWW web = new WWW(bundleUrl))
        {
            yield return web;
            AssetBundle remoteAssetBundle = web.assetBundle;
            if (remoteAssetBundle == null)
            {
                Debug.LogError("Failed to download AssetBundle!");
                yield break;
            }
          // Instantiate(remoteAssetBundle.LoadAsset(assetName), parentObj.transform);
            string[] scenePath = remoteAssetBundle.GetAllScenePaths();
            SceneManager.LoadScene(scenePath[0]);
            remoteAssetBundle.Unload(false);
        }
    }



}
