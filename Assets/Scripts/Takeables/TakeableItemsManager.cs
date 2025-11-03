using System;
using System.Collections.Generic;
using UnityEngine;

public class TakeableItemManager : MonoBehaviour
{

    [Header("Items")]
    [SerializeField] private TakeableItem[] allCoins;
    private readonly List<TakeableItem> coinsTaken = new();

    private void Awake()
    {        
    }

    private void OnEnable()
    {
        
        //loop through al coins and add the method
        int id = 0;
        foreach(TakeableItem c in allCoins){
            c.SetId(id);
            c.OnTakeItem += OnItemTaken;
            id++;
        }
    }

    private void OnDisable()
    {
        //loop through al coins and add the method
        foreach(TakeableItem c in allCoins){
            c.OnTakeItem += OnItemTaken;
        }
    }

    private void OnItemTaken(TakeableItem item, string type, int id)
    {
        if (item == null) return;

        // Hide the item that was taken
        item.Hide();

        // If it's a coin, track it
        if (type == "Coin")
        {
            // Avoid duplicates
            if (!coinsTaken.Contains(item))
                coinsTaken.Add(item);

            // (Optional) If you really need to find by id in allCoins:
            // var coinById = Array.Find(allCoins, c => c != null && c.Id == id);
            // coinById?.Hide();
        }
    }
}
