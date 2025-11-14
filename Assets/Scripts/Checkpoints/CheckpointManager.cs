using System;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField] Checkpoint[] checkpoints;

    private GameObject lastCheckpointChecked;

    [Header("Notifiers")]
    public Action OnTouchCheckpoint;


    // Start is called before the first frame update
    void Start()
    {
        checkpoints[0].SetChecked();
        lastCheckpointChecked = checkpoints[0].gameObject;
        
        foreach(Checkpoint checkpoint in checkpoints ){

            checkpoint.OnChecked += HandleChecked;
        }

    }

    private void HandleChecked(int id, GameObject checkpointGO)
    {
        lastCheckpointChecked = checkpointGO;
        OnTouchCheckpoint?.Invoke();
    }

    public Vector3 GetLastCheckpointPosition()
    {
        return lastCheckpointChecked.transform.position;
    }

    public void ResetCheckpoints()
    {
        lastCheckpointChecked = checkpoints[0].gameObject;
        foreach (Checkpoint checkpoint in checkpoints)
        {
            checkpoint.Uncheck();
        }
    }

    void Destroy() { 
        foreach(Checkpoint checkpoint in checkpoints ){

            checkpoint.OnChecked -= HandleChecked;
        }
    }


}
