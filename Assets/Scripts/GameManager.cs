using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // handles options, saves, serializing
    public static GameManager Instance;

    private void Awake() {
        if (!Instance) Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void LoadFirstScene(){
        SceneManager.LoadScene(1);
    }

    void StartNextDay(){

    }

    public void FinishedDay(){

    }

    void StartNextSeason(){
        
    }
}
