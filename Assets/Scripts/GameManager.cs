using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    public string difficulty { get; set; }
    public int lifes { get; set; }
    public float fwSpeed { get; set; }
    public float sideSpeed { get; set; }

    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }



    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(){
        SceneManager.LoadScene("Level01");
    }

    public void NextLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LostLife(){
        lifes = lifes - 1;
    }

    public bool IsGameOver() {
        if (lifes == 0)
            return true;
        else return false;
    }

    public void InitGame(){
        switch (difficulty){
            case "easy":
                fwSpeed = 10.0f;
                sideSpeed = 0.5f;
                lifes = 5;
                break;
            case "medium":
                fwSpeed = 15.0f;
                sideSpeed = 0.6f;
                lifes = 3;
                break;
            case "hard":
                fwSpeed = 18.0f;
                sideSpeed = 0.7f;
                lifes = 3;
                break;
            default:
                fwSpeed = 10.0f;
                sideSpeed = 0.5f;
                lifes = 5;
                break;
        }
    }




}