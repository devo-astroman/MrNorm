using System;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private MainMenuUIDocument mainMenuUIDocument;
    [SerializeField] private GameSceneManager gameSceneManager;
    
    public Action OnStartButtonClick;

    void Start()
    {
        mainMenuUIDocument.OnStartButtonClick += HandleStartButtonClick;
    }

    public void HandleStartButtonClick()
    {
        gameSceneManager.GoPlay();
    }
    
    void Destroy()
    {
        mainMenuUIDocument.OnStartButtonClick -= HandleStartButtonClick;
    }

}
