using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public GameObject ScoreTable;

    public void Start()
    {
        Resolution[] resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        Screen.SetResolution(resolutions[3].width, resolutions[3].height , false);
    }

    public void StarGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void HighScore()
    {
        ScoreTable.SetActive(true);
    }

    public void CloseHighScore()
    {
        ScoreTable.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
