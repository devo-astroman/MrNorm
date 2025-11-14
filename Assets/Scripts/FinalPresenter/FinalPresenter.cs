using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FinalPresenter : MonoBehaviour
{
    [SerializeField] private TakeableItemManager takeableItemManager;

    [SerializeField] private GameObject bronzeMedal;
    [SerializeField] private GameObject silverMedal;
    [SerializeField] private GameObject goldMedal;
    [SerializeField] private GameObject trophy;

    [SerializeField] private Collider2D detector;


    [SerializeField] private GameObject canvasGo;
    [SerializeField] private TMP_Text topText;
    [SerializeField] private TMP_Text bottomText;

    private SetTimeoutUtility timeout;


    [Header("Notifiers")]
    public Action OnShowCongrats;


    void Start(){
        timeout = new SetTimeoutUtility(this);
    }

    public void ProcessFinal()
    {

        detector.enabled = false;

        timeout.SetTimeout(() => {            

            OnShowCongrats?.Invoke();

            List<TakeableItem> coinsTaken = takeableItemManager.GetCoinsTaken();

            int nTotalCoins = takeableItemManager.GetNTotalCoins();

            canvasGo.SetActive(true);

            if(coinsTaken.Count < nTotalCoins/3){
                showMessage(coinsTaken.Count,"Bronze");
                showBronzeMedal();

            }else if(coinsTaken.Count >= nTotalCoins/3 && coinsTaken.Count < 2*nTotalCoins/3){
                showMessage(coinsTaken.Count,"Silver");
                showSilverMedal();

            }else{
                showMessage(coinsTaken.Count,"Gold");
                showGoldMedal();
            }
        
        
        }, 2f);

        

    }

    private void showBronzeMedal(){
        bronzeMedal.SetActive(true);
    }

    private void showSilverMedal(){
        silverMedal.SetActive(true);
    }

    private void showGoldMedal(){
        goldMedal.SetActive(true);
    }

    private void showTrophy()
    {
        trophy.SetActive(true);
    }

    private void HideAllPrizes()
    {
        bronzeMedal.SetActive(false);
        silverMedal.SetActive(false);
        goldMedal.SetActive(false);
        trophy.SetActive(false);
    }

    private void showMessage(int nCoins, string medal)
    {
        topText.text = $"Congrats! you have collected {nCoins} coins.";
        bottomText.text = $"You have earned the {medal} medal!";
    }
    
    public void ResetFinalMessage(){

        detector.enabled = true;        
        HideAllPrizes();
        canvasGo.SetActive(false);
    }

    
    
}
