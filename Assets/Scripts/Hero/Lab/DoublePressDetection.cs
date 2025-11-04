using UnityEngine;
using UnityEngine.Events;
using System;

public class DoublePressDetection : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Key to detect double press")]
    [SerializeField] private KeyCode keyToDetect = KeyCode.None;

    [Tooltip("Time allowed between presses (in seconds)")]
    [SerializeField] private float doublePressThreshold = 0.25f;

    [Header("Events")]
    [Tooltip("Invoked when the key is double pressed")]
    public UnityEvent OnDoublePress;

    // Optional: Allow subscription via code
    public Action OnDoublePressAction;

    private float lastPressTime = -1f;

    void Update()
    {
        if (Input.GetKeyDown(keyToDetect))
        {
            if (Time.time - lastPressTime <= doublePressThreshold)
            {
                // Double Press Detected
                OnDoublePress?.Invoke();
                OnDoublePressAction?.Invoke();
                lastPressTime = -1f; // reset to avoid triple-trigger
            }
            else
            {
                lastPressTime = Time.time;
            }
        }
    }
}
