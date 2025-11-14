using UnityEngine;

public class NoFrictionAssigner : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D; 
    [SerializeField] private PhysicsMaterial2D pm2D;

    public void AddNoFrictionMaterial(){
        rb2D.sharedMaterial = pm2D;
    }

    public void RemoveNoFrictionMaterial(){
        rb2D.sharedMaterial = null;
    }
}
