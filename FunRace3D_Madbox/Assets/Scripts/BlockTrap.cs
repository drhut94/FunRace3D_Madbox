using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTrap : MonoBehaviour
{
    [SerializeField] private float speed;
    private PlayerController player;

    #region Unity methods

    private void Start() {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update() {

        transform.position += transform.forward * Mathf.Sin(Time.time) * speed * Time.deltaTime;
    }

    #endregion

    private void OnCollisionEnter(Collision collision) {

        if (collision.collider.CompareTag("Player")) {

            player.Die();
        }
    }
}
