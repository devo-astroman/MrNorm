using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;


public class FinalPresenter : MonoBehaviour
{

    [SerializeField] private TakeableItemManager takeableItemManager;

    [SerializeField] private GameObject bronzeMedal;
    [SerializeField] private GameObject silverMedal;
    [SerializeField] private GameObject goldMedal;
    [SerializeField] private GameObject trophy;

    [SerializeField] private GameObject canvas;
    [SerializeField] private TMP_Text messageText;

    [SerializeField] private Collider2D detector;

    private SetTimeoutUtility timeout;


    [Header("Notifiers")]
    public Action OnShowCongrats;

    
    
    /* [Header("Notifiers")]
    public Action<TakeableItem, string, int> OnTakeItem; */

    void Start(){
        timeout = new SetTimeoutUtility(this);
    }

    public void ProcessFinal()
    {
        Debug.Log("Process final!");

        detector.enabled = false;

        timeout.SetTimeout(() => {

            

            OnShowCongrats?.Invoke();

            List<TakeableItem> coinsTaken = takeableItemManager.GetCoinsTaken();

            //print the content of 
            Debug.Log("coinsTaken: " +  coinsTaken.Count);

            int nTotalCoins = takeableItemManager.GetNTotalCoins();
            Debug.Log("total coins: " +  nTotalCoins);


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
        messageText.text = "Congratulations! you have collected " + nCoins + " coins, so you have earnt the " + medal + " medal";
        canvas.SetActive(true);
    }
    
    public void ResetFinalMessage(){

        detector.enabled = true;
        canvas.SetActive(false);
        HideAllPrizes();
    }

    
    
}
