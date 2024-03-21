using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void LoadGameScene()
    
    {
        int sceneIndex = Random.Range(0, 2);
        string sceneName = sceneIndex == 0 ? "Map1" : "Map2";
        
        SceneManager.LoadScene(sceneName);
    }
    
}
