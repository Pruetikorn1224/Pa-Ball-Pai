using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Setting")]
    [SerializeField] float playerSpeed;

    [Header("Camera Configuration")]
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] Vector3 cameraRotation;

    Camera mainCamera;

    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;

        mainCamera.gameObject.transform.localPosition = cameraOffset;
        mainCamera.gameObject.transform.localRotation = Quaternion.Euler(cameraRotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        characterController.Move(move * Time.deltaTime * playerSpeed);
    }
}
