using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public void HubScene()
    {
        SceneManager.LoadScene("Hub");
    }
    public void GameScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
     public void CloseGame()
    {
        Application.Quit();
    }
}
