using UnityEngine;

public class HeroStates : MonoBehaviour
{
    [Header("Hero")]
    [SerializeField] private HeroDamageHandler heroDamageHandler;
    [SerializeField] private HeroJetpackMovement heroJetpackMovement;

    private void HandleDied(){
        heroJetpackMovement.enabled =false;
    }

    void Start(){
        heroDamageHandler.OnDied += HandleDied;
    }

    void Destroy(){
        heroDamageHandler.OnDied -= HandleDied;
    }


}

