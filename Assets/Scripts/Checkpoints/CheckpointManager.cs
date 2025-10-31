using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField] Checkpoint[] checkpoints;

    private GameObject lastCheckpointChecked;


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
        Debug.Log("Last " + id);
        lastCheckpointChecked = checkpointGO;
    }

    public Vector3 GetLastCheckpointPosition(){
        return lastCheckpointChecked.transform.position;
    }

}
