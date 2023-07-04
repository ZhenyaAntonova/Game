using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas canvasPauseMenu;
    [SerializeField] private GameObject player;

    public bool isPaused = false;

    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        canvasPauseMenu.gameObject.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        canvasPauseMenu.gameObject.SetActive(false);
        isPaused = false;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadBadEndingScene()
    {
        SceneManager.LoadScene("BadEnding");
    }

    
}
