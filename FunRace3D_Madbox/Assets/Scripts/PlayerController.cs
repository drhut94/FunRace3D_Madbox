using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    #region Public variables

    public PathManager pathManager;
    public Transform target;

    [Header("Movment variables")]
    public float speed;
    public float rotationSpeed;

    #endregion


    #region Private variables

    private Rigidbody rb;
    private bool startGame;

    #endregion


    #region Unity methods

    private void Start() {

        InitPlayer();
    }

    private void FixedUpdate() {

        if (startGame) {

            MovePlayer();
            CheckForNextPoint();
        }
    }

    #endregion


    #region Private methods

    private void InitPlayer() {

        startGame = true;

        rb = GetComponent<Rigidbody>();
        target = pathManager.transform.GetChild(0);
    }

    private void MovePlayer() {

        rb.velocity = new Vector3(transform.forward.x * speed, rb.velocity.y, transform.forward.z * speed);

        Vector3 pointDir = target.position - transform.position;
        pointDir.y = 0;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(pointDir), Time.deltaTime * rotationSpeed);
    }
    
    private void CheckForNextPoint() {

        if(Vector3.Distance(transform.position, target.position) < 1f) {

            target = pathManager.GetNextPoint();

            if(target == null) {

                EndGame();
            }
        }
    }

    private void EndGame() {

        startGame = false;
    }

    #endregion
}
