using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string targetScene;

    public void ChangeScene()
    {
        if (targetScene == "Quit")
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(targetScene, LoadSceneMode.Single);
        }
    }
}
