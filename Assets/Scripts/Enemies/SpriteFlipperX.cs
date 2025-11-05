using UnityEngine;
using UnityEngine.Events;

public class SpriteFlipperX : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;

    public void FlipX(){
        sr.flipX = !sr.flipX;
    }
}
