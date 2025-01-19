using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour {

    void Start() {
        Debug.Log("Start");
        if (!SaveLoader.isAnySaves()) {
            Debug.Log("Scene 1");
            SceneManager.LoadScene("Scene 1");
            //SceneManager.LoadScene("Empty");
        }
        else {
            Debug.Log("Empty");
            SceneManager.LoadScene("Empty");
            SaveLoader.load();
        }
    }
}
