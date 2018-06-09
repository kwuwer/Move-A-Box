using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    public Text Difficulty;

    public void SaveOptions(){
        GameManager.Instance.difficulty = Difficulty.text.ToLower();
        SceneManager.LoadScene("MainMenu");
    }
}
