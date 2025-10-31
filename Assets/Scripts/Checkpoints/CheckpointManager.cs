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
        
        foreach(Checkpoint checkpoint in checkpoints ){

            checkpoint.OnChecked += HandleChecked;
        }

    }

    private void HandleChecked(int id, GameObject checkpointGO)
    {
        Debug.Log("Last " + id);
        lastCheckpointChecked = checkpointGO;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
