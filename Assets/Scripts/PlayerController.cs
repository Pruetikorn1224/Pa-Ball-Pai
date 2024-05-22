using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Setting")]
    [SerializeField] float playerSpeed;

    GameObject ball;

    LevelControl levelControl;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Ball");
        levelControl = GameObject.Find("Level Manager").GetComponent<LevelControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5f && levelControl.levelState == 0)
        {
            levelControl.levelState = 1;
        }
    }

    private void FixedUpdate()
    {
        if (levelControl.levelState == 0)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            transform.position += move * Time.deltaTime * playerSpeed;

            if (move != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(move, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * playerSpeed);
            }
        }
        
    }
}
