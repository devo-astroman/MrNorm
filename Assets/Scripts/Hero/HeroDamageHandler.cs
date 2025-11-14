using System;
using System.Collections;
using UnityEngine;


public class HeroDamageHandler : MonoBehaviour
{
    [SerializeField] private CollisionHandler collisionHandler;

    [Header("Health values")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float coolDown = 2;


    public Action OnHealthDown;
    public Action OnDied;

    private int currentHealth;
    private bool allowDamage = true;
    


    private void Start()
    {
        currentHealth = maxHealth;
        collisionHandler.OnCollisionEnter += HandleCollisionEnter;
        collisionHandler.OnTriggerEnter += HandleTriggerEnter;     
    }

    private void HandleCollisionEnter(Collision2D collision, string tag){
        NotifyDamage();
    }

    private void HandleTriggerEnter(Collider2D collision, string tag){
        NotifyDamage();
    }

    private void NotifyDamage()
    {
        if(allowDamage){
            allowDamage = false;
            currentHealth -= 1;

            OnHealthDown?.Invoke();
            if(currentHealth == 0){
                OnDied?.Invoke();
            }else{
                StartCoroutine(SetTimeout(() => {
                    allowDamage=true;
                }, coolDown));
            }
            
        }

    }

    private void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player damaged! Health = " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("PLAYER DEAD");        
    }


    IEnumerator SetTimeout(System.Action callback, float delay)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
