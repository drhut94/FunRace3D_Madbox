using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [Header("Movment variables")]
    public float speed;
    public float rotationSpeed;


    private PathManager pathManager;
    private Transform target;
    private Rigidbody rb;
    private bool startGame;
    private CameraController cam;
    private Vector3 checkPointPos;
    private Quaternion checkPointRot;
    public bool startFromBeggining;
    private UiManager ui;



    #region Getters & Setters

    public bool GetStartGame() => startGame;

    public void SetStartGame(bool value) {

        startGame = value;
    }

    #endregion


    #region Unity methods

    private void Start() {

        checkPointPos = transform.position;
        checkPointRot = transform.rotation;
        startFromBeggining = true;
        InitPlayer();
        ui = FindObjectOfType<UiManager>();
    }

    private void FixedUpdate() {

        if (startGame) {

            if (Input.GetKey(KeyCode.Space)) {

                MovePlayer();
                CheckForNextPoint();
            }
            else {
                StopPlayer();
            }
        }
    }

    #endregion


    #region Public methods

    public void Die() {

        startGame = false;
        rb.constraints = RigidbodyConstraints.None;
        Debug.Log("PlayerDead");
        StartCoroutine(ResetPlayerCheckpoint());
    }

    #endregion


    #region Private methods

    private void InitPlayer() {

        
        cam = FindObjectOfType<CameraController>();
        pathManager = FindObjectOfType<PathManager>();
        rb = GetComponent<Rigidbody>();

        if(startFromBeggining)
        target = pathManager.transform.GetChild(0);
    }

    private void MovePlayer() {

        rb.velocity = new Vector3(transform.forward.x * speed, rb.velocity.y, transform.forward.z * speed);

        Vector3 pointDir = target.position - transform.position;
        pointDir.y = 0;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(pointDir), Time.deltaTime * rotationSpeed);
    }

    private void StopPlayer() {

        rb.velocity = new Vector3(0, rb.velocity.y, 0);
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

        transform.position = checkPointPos;
        transform.rotation = checkPointRot;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        startGame = false;
        cam.ResetCamera();
        pathManager.currentPoint = 0;
        InitPlayer();
        StartCoroutine(ReloadScene());
    }



    #endregion

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("CameraChange")) {
            cam.ChangeCamera(other.transform.parent.transform.position);
        }
        else if (other.CompareTag("ResetCamera")) {
            cam.ResetCamera();
        }
        else if (other.CompareTag("CheckPoint")) {
            checkPointPos = transform.position;
            checkPointRot = transform.rotation;
            startFromBeggining = false;
            Debug.Log("Setcheckpoint");
        }
    }

    IEnumerator ResetPlayerCheckpoint() {

        yield return new WaitForSeconds(3);

        transform.position = checkPointPos;
        transform.rotation = checkPointRot;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        startGame = true;
        cam.ResetCamera();

        if(startFromBeggining)
        pathManager.currentPoint = 0;

        InitPlayer();
    }

    IEnumerator ReloadScene() {

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("SampleScene");
    }
}
