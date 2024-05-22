using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    LevelControl levelControl;

    private void Start()
    {
        levelControl = GameObject.Find("Level Manager").GetComponent<LevelControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && levelControl.levelState == 0)
        {
            Vector3 desiredPoint = new Vector3(transform.position.x, 1.125f, transform.position.z);
            levelControl.respawnPosition = desiredPoint;
        }
    }
}
