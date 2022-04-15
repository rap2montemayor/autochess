using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DontDestroyOnLoad))]
public class SceneLoader : MonoBehaviour
{
    // ------------------------------------
    // SINGLETON PATTERN
    // ------------------------------------
    private static SceneLoader instance;

    void Awake(){
        if (instance == null){
            instance = this;
        }else{
            Destroy(this.gameObject);
        } 
    }

    // ------------------------------------
    // FIELDS
    // ------------------------------------

    private int startLocID;

    // ------------------------------------
    // METHODS
    // ------------------------------------

    public static int GetStartLocID(){
        return instance.startLocID;
    }

    public static void LoadScene(string sceneName, int startLocID){
        instance.startLocID = startLocID;
        SceneManager.LoadScene(sceneName);
        /*
        instance.StartCoroutine(instance.ILoadScene(sceneName));
        */

    }

    public static void LoadScene(string sceneName){
        LoadScene(sceneName, 0);
        
    }
    /*
    private IEnumerator ILoadScene(string sceneName){

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone){
             if (asyncOperation.progress >= 0.9f){
                asyncOperation.allowSceneActivation = true;
            } 
            yield return null;
        }
        doOnce = false;

    }
    */
}