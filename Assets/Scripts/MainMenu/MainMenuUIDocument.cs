using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUIDocument : MonoBehaviour
{
    [SerializeField] private UIDocument _document;

    private Button startButton;
    private Label loadingLabel;
    public Action OnStartButtonClick;

    void Start()
    {
        // Initial queries to get every ui element
        var root = _document.rootVisualElement;

        startButton = root.Q<Button>("StartButton");
        loadingLabel = root.Q<Label>("Loading");

        loadingLabel.style.display = DisplayStyle.None;

        RegisterCallbacks();
    }

    public void RegisterCallbacks()
    {
        startButton.RegisterCallback<ClickEvent>(evt =>
        {
            startButton.style.display = DisplayStyle.None;
            loadingLabel.style.display = DisplayStyle.Flex;
            OnStartButtonClick?.Invoke();
        });
    }

}
