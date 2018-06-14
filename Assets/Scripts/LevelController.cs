using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    // Wszystkie zmienne poniżej to referencje do obiektów, info dalej
    public GameObject FinishPanel;
    public GameObject Player;
    private PlayerController _player;
    public GameObject LevelIndicator;
    public GameObject LostLifePanel;
    public GameObject GameOver;
    public Text LifesText;

    /* Sprawdzamy, czy już wykonaliśmy funkcję... 
     * inaczej będzie się odpalać w nieskończoność
     */
    private bool alreadyDone;

    /* UWAGA! BEHOLD!
     * 
     * Poniższy typ w Unity pozwala nam na stworzenie funkcji, 
     * której działanie możemy opóźnić przez 'yield return new WaitForSeconds(float value);'
     * 
     * Jest to część tzw. Coroutine, co działa trochę jak wielowątkowość...
     * Nie wiem jak to inaczej wyjaśnić, ale wywołujac Coroutine, 
     * możemy wstrzymać wywoływanie na chwilę, nie stopując całej gry(programu)
     */
    IEnumerator LoadNextLevel(){
        yield return new WaitForSeconds(7f);
        GameManager.Instance.NextLevel();
        yield return null;
    }

    IEnumerator ResetLevel(){
        yield return new WaitForSeconds(6.5f);
        GameManager.Instance.RestartLevel();
        yield return null;
    }

    IEnumerator MainMenu(){
        yield return new WaitForSeconds(6.5f);
        SceneManager.LoadScene("MainMenu");
        yield return null;
    }


    // Ta i funkcja LostLife są wywoływane przez Coroutine
    public void FinishedLevel(){
        if (alreadyDone != true)
        {
            alreadyDone = true;
            FinishPanel.SetActive(true);
            LevelIndicator.SetActive(false);
        }
    }

    public void LostLife(){
        if (alreadyDone != true)
        {
            Debug.Log("LostLife");
            GameManager.Instance.LostLife();
            alreadyDone = true;
            if (GameManager.Instance.IsGameOver() == false)
            {
                LevelIndicator.SetActive(false);
                LostLifePanel.SetActive(true);
                LifesText.text = "You have " + GameManager.Instance.lifes.ToString() + " life(s) left";
                StartCoroutine(ResetLevel());
            } else {
                alreadyDone = true;
                GameOver.SetActive(true);
                StartCoroutine(MainMenu());
            }
        }
    }

    // Inicjalizacja...
    private void Start()
    {
        Player = GameObject.Find("Player");
        _player = Player.GetComponent<PlayerController>();
        alreadyDone = false;
    }

    // Sprawdzamy co klatkę czy 'plejer' wtopił, czy może udało się mu skończyć poziom
    // Mam tu trochę rozjazd między finishedLevel a LostLife... do poprawienia -- KW
    private void Update()
    {
        if (_player.finishedLevel == true){
            FinishedLevel();
            StartCoroutine(LoadNextLevel());
        }
        if (_player.lostLife == true)
            LostLife();
    }
}
