using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;

    [SerializeField] private Transform target;
    private PlayerController player;
    private Vector3 initPos;


    #region Unity methods

    private void Start() {

        InitCameraController();
    }



    private void FixedUpdate() {

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed);

        Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    #endregion

    #region Private Methods

    private void InitCameraController() {

        player = FindObjectOfType<PlayerController>();

    }

    private void ResetCamera() {

        target.transform.position = target.transform.parent.position;
    }

    #endregion

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("CameraChange")) {
            target.transform.position = other.transform.position;
        }
        else if (other.CompareTag("ResetCamera")) {
            ResetCamera();
        }
    }
}
