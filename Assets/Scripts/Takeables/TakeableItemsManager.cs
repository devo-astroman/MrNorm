using System;
using System.Collections.Generic;
using UnityEngine;

public class TakeableItemManager : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField] private GameObject coinsParent;
    private TakeableItem[] allCoins;
    private readonly List<TakeableItem> coinsTaken = new();
    private int nTotalCoins = 0;

    [Header("Diamond Dash")]
    [SerializeField] private GameObject diamonsDashParent;
    private TakeableItem[] allDiamondsDash;
    private readonly List<TakeableItem> diamondsDashTaken = new();

    [Header("Notifiers")]
    public Action OnCoinTake;

    private void Awake()
    {
        // Get all TakeableItem components in children of coinsParent
        allCoins = coinsParent.GetComponentsInChildren<TakeableItem>();
    }

    private void OnEnable()
    {
        int id = 0;
        foreach (TakeableItem c in allCoins)
        {
            if (c == null) continue;
            c.SetId(id);
            c.OnTakeItem += OnItemTaken;
            id++;
        }
        nTotalCoins = id;
    }

    private void OnDisable()
    {
        foreach (TakeableItem c in allCoins)
        {
            if (c == null) continue;
            c.OnTakeItem -= OnItemTaken; 
        }
    }

    private void OnItemTaken(TakeableItem item, string type, int id)
    {
        if (item == null) return;

        item.Hide(); // Hide the item

        if (type == "Coin" && !coinsTaken.Contains(item))
        {
            coinsTaken.Add(item);
            OnCoinTake?.Invoke();

        }else if(type == "DiamondDash" && !diamondsDashTaken.Contains(item))
        {
            diamondsDashTaken.Add(item);
        }
    }

    public List<TakeableItem> GetCoinsTaken()
    {

        return coinsTaken;
    }
    
    public int GetNCoinsTaken(){

        return coinsTaken.Count;
    }

    public int GetNTotalCoins()
    {
        return nTotalCoins;
    }

    public void ResetTakenCoins()
    {
        foreach (TakeableItem c in allCoins)
        {
            c.Show();
        }

        coinsTaken.Clear();

    }
}
