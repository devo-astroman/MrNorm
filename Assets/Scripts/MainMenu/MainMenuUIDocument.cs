using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUIDocument : MonoBehaviour
{
    [SerializeField] private UIDocument _document;

    private Button startButton;
    public Action OnStartButtonClick;

    void Start()
    {
        // Initial queries to get every ui element
        var root = _document.rootVisualElement;

        startButton = root.Q<Button>("StartButton");

        RegisterCallbacks();
    }

    public void RegisterCallbacks()
    {
        startButton.RegisterCallback<ClickEvent>(evt =>
        {
            OnStartButtonClick?.Invoke();
        });
    }

}
