using UnityEngine;

public class HeroDamageHandler : MonoBehaviour
{


    [SerializeField] private CollisionHandler collisionHandler;

    [Header("Player Stats")]
    //[SerializeField] private int maxHealth = 5;
    private int currentHealth;


    private void Start()
    {

        collisionHandler.OnCollisionEnter += HandleCollisionEnter;
        collisionHandler.OnTriggerEnter += HandleTriggerEnter;
        
    }

    private void HandleCollisionEnter(Collision2D collision){
        Debug.Log("On collision Enter HeroDamageHandler ");
    }

    private void HandleTriggerEnter(Collider2D collision){
        Debug.Log("On TriggerEnter2D Enter HeroDamageHandler ");
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
        // TODO: call respawn / game over event here
    }
}
