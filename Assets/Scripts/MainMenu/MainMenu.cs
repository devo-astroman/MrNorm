using System;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private MainMenuUIDocument mainMenuUIDocument;
    [SerializeField] private GameSceneManager gameSceneManager;

    
    
    public Action OnStartButtonClick;

    void Start()
    {
        Debug.Log("Start main menu");
        mainMenuUIDocument.OnStartButtonClick += HandleStartButtonClick;
    }

    public void HandleStartButtonClick()
    {
        //jump to the other scene
        Debug.Log("Start button clicked");
        gameSceneManager.GoPlay();
    }
    
    void Destroy()
    {
        mainMenuUIDocument.OnStartButtonClick -= HandleStartButtonClick;
    }

}
