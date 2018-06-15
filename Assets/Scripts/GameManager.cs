using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    // Deklaracje zmiennych jak poziom / ilość żyć / szybkość do przodu i na boki
    public string difficulty { get; set; }
    public int lifes { get; set; }
    public int extraLifes { get; set; }
    public float fwSpeed { get; set; }
    public float maxforce { get; set; }
    public float sideSpeed { get; set; }

    // Poniższy kod odpowiada za singleton GameManager-a (robione na pałę)
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


    // Z dowolnego miejca można zrestartować obecną scenę funkcją
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Gra zaczyna się od sceny "Level01"
    public void StartGame(){
        SceneManager.LoadScene("Level01");
    }

    // Załaduj następną scenę (ważna jest kolejność w build settings)
    public void NextLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // No cóż... jedno życie poszło się 'hasać'
    public void LostLife(){
        lifes = lifes - 1;
    }

    // Sprawdza czy już żeśmy się pozbyli wszystkich żyć - Game Over
    public bool IsGameOver() {
        if (lifes == 0)
            return true;
        else return false;
    }


    /* Zależnie od poziomu trudności ustawia parametry
     * 
     * Tutaj trzeba będzie trochę pożonglować parametrami
     */
    public void InitGame(){
        switch (difficulty){
            case "easy":
                fwSpeed = 6.0f;
                maxforce = 100.0f;
                sideSpeed = 0.5f;
                lifes = 4;
                break;
            case "medium":
                fwSpeed = 7.0f;
                maxforce = 120.0f;
                sideSpeed = 0.7f;
                lifes = 3;
                break;
            case "hard":
                fwSpeed = 9.0f;
                maxforce = 150.0f;
                sideSpeed = 1.0f;
                lifes = 2;
                break;
            default:
                fwSpeed = 6.0f;
                maxforce = 100.0f;
                sideSpeed = 0.5f;
                lifes = 4;
                break;
        }
        extraLifes = 0;
    }




}