using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Setting")]
    [SerializeField] float playerSpeed;

    private CharacterController characterController;

    GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        ball = GameObject.Find("Ball");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * playerSpeed);
        }
    }
}
