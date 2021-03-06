﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderTrap : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private PlayerController player;


    #region Unity methods

    private void Start() {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update() {

        transform.Rotate(transform.up, rotationSpeed * Time.deltaTime);
    }

    #endregion

    private void OnCollisionEnter(Collision collision) {

        if (collision.collider.CompareTag("Player")) {

            player.Die();
        }
    }
}
