using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {
    // Referencja do tekstu
    public Text Difficulty;


    // Zapisanie poziomu trudności i przejście do menu głównego
    public void SaveOptions(){
        GameManager.Instance.difficulty = Difficulty.text.ToLower();
        SceneManager.LoadScene("MainMenu");
    }
}
