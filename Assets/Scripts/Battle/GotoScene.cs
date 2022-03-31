using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoScene : MonoBehaviour {

    public string sceneName;

    public void LoadScene(){
        SceneManager.LoadScene(sceneName);
    }
}