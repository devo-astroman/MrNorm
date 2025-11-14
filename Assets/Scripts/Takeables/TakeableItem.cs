using System;
using UnityEngine;

public class TakeableItem : MonoBehaviour
{

    [SerializeField] private int id;
    [SerializeField] private string type;
    [SerializeField] private CollisionHandler collisionHandler;
    [SerializeField] private SpriteRenderer sRenderer;
    
    [Header("Notifiers")]
    public Action<TakeableItem, string, int> OnTakeItem;

    private void Start()
    {
        collisionHandler.OnTriggerEnter += OnPlayerTakeMe;
    }

    public void OnPlayerTakeMe(Collider2D coll2D, string tag){
        OnTakeItem?.Invoke(this, type, id);
    }
    
    public void Hide()
    {
        collisionHandler.InactiveCollisions();
        sRenderer.enabled = false;

    }

    public void Show()
    {
        collisionHandler.ActiveCollisions();
        sRenderer.enabled = true;
    }

    public int GetId()
    {
        return id;
    }
    
    public void SetId(int newId)
    {
        id = newId;
    }
}
