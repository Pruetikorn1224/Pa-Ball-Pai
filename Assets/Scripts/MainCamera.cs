using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [Header("Camera Configuration")]
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] Vector3 cameraRotation;

    GameObject mainCharacter;

    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = GameObject.FindGameObjectsWithTag("Player")[0];

        gameObject.transform.localPosition = mainCharacter.transform.position + cameraOffset;
        gameObject.transform.localRotation = Quaternion.Euler(cameraRotation);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mainCharacter.transform.position + cameraOffset;
    }
}
