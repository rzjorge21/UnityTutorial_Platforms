using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public GameObject[] fruits;

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CheckWin()
    {        
        int counter = 1;

        for (int i = 0; i < fruits.Length; i++)
        {
            if (fruits[i] == null)
            {
                counter++;
            }
        }

        if (counter == fruits.Length)
        {
            Debug.Log("Ganaste");
        }
    }

}
