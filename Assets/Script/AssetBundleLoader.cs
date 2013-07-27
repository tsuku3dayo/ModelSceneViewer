using UnityEngine;
using System.Collections;

public class AssetBundleLoader : MonoBehaviour {
	
	// AssetBundleが置かれているサーバのURL
	public string assetBundleServerURL;
	// AssetBundle
	private AssetBundle assetBundle = null;

#if USER_WWW_LOADFROMCACHEORDOWNLOAD
	IEnumerator ChangeModel (string modelName) {
		// キャッシュクリア
		Caching.CleanCache();
		// 破棄
		GameObject modelData = GameObject.Find("modelData");
		if (modelData != null) {
			Destroy(modelData);
		}
		// 前のAssetBundleを破棄	
		if (assetBundle != null) {
			assetBundle.Unload(true);
		}
		// サーバーからAssetBunbleをダウンロードしキャッシュに保存
		// キャッシュは 150日間保存される
    	WWW modelDataScene = WWW.LoadFromCacheOrDownload(assetBundleServerURL + modelName + ".unity3d", 0);
	    // ロードが終わるまで待つ
    	yield return modelDataScene;
	    if (modelDataScene.error == null) {
		    assetBundle = modelDataScene.assetBundle;
			// シーンの加算ロード
			Application.LoadLevelAdditive(modelName);
    	}
	}
#else
	// キャッシュなし
	IEnumerator ChangeModel (string modelName) {
		// 破棄
		GameObject modelData = GameObject.Find("modelData");
		if (modelData != null) {
			Destroy(modelData);
		}
		// 前のAssetBundleを破棄	
		if (assetBundle != null) {
			assetBundle.Unload(true);
		}
		// サーバーからAssetBunbleをダウンロード
    	WWW modelDataScene = new WWW(assetBundleServerURL + modelName + ".unity3d");
	    // ロードが終わるまで待つ
    	yield return modelDataScene;
	    if (modelDataScene.error == null) {
		    assetBundle = modelDataScene.assetBundle;
			// シーンの加算ロード
			Application.LoadLevelAdditive(modelName);
    	}
	}
#endif
}
