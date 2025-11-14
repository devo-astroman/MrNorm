using UnityEngine;
using UnityEngine.UIElements;

public class Hud : MonoBehaviour
{
    [SerializeField] private UIDocument _document;

    private Label heart1Label;
    private Label heart2Label;
    private Label heart3Label;
    private Label coinsLabel;

    private bool uiLoaded = false;

    void Start()
    {
        if(!uiLoaded){
            GetUIElements();
            uiLoaded = true;
        }
    }

    private void GetUIElements()
    {
        // Initial queries to get every ui element
        var root = _document.rootVisualElement;

        heart1Label = root.Q<Label>("heart1");
        heart2Label = root.Q<Label>("heart2");
        heart3Label = root.Q<Label>("heart3");

        coinsLabel = root.Q<Label>("coinsLabel");
    }

    public void InitHud()
    {
        if(!uiLoaded){
            GetUIElements();
            uiLoaded = true;
        }

        heart1Label.style.display = DisplayStyle.Flex;
        heart2Label.style.display = DisplayStyle.Flex;
        heart3Label.style.display = DisplayStyle.Flex;

        coinsLabel.text = "Coins x 0";
    }

    public void UpdateCoins(int nCoins)
    {
        coinsLabel.text = "Coins x " + nCoins;
    }
    
    public void UpdateHearts(int nHearts)
    {
        if (nHearts == 0)
        {
            heart1Label.style.display = DisplayStyle.None;
            heart2Label.style.display = DisplayStyle.None;
            heart3Label.style.display = DisplayStyle.None;

        }
        else if (nHearts == 1)
        {
            heart1Label.style.display = DisplayStyle.Flex;
            heart2Label.style.display = DisplayStyle.None;
            heart3Label.style.display = DisplayStyle.None;

        }
        else if (nHearts == 2)
        {
            heart1Label.style.display = DisplayStyle.Flex;
            heart2Label.style.display = DisplayStyle.Flex;
            heart3Label.style.display = DisplayStyle.None;

        }
        else if (nHearts == 3)
        {
            heart1Label.style.display = DisplayStyle.Flex;
            heart2Label.style.display = DisplayStyle.Flex;
            heart3Label.style.display = DisplayStyle.Flex;            
        }
    }
}
