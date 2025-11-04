using System;
using System.Collections.Generic;
using UnityEngine;

public class TakeableItemManager : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField] private GameObject coinsParent;
    private TakeableItem[] allCoins;
    private readonly List<TakeableItem> coinsTaken = new();

    [Header("Diamond Dash")]
    [SerializeField] private GameObject diamonsDashParent;
    private TakeableItem[] allDiamondsDash;
    private readonly List<TakeableItem> diamondsDashTaken = new();


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
    }

    private void OnDisable()
    {
        foreach (TakeableItem c in allCoins)
        {
            if (c == null) continue;
            c.OnTakeItem -= OnItemTaken; // ✅ Correct: remove event listener
        }
    }

    private void OnItemTaken(TakeableItem item, string type, int id)
    {
        if (item == null) return;

        item.Hide(); // Hide the item

        if (type == "Coin" && !coinsTaken.Contains(item))
        {
            coinsTaken.Add(item);
        }else if(type == "DiamondDash" && !diamondsDashTaken.Contains(item))
        {
            diamondsDashTaken.Add(item);
        }
    }
}
