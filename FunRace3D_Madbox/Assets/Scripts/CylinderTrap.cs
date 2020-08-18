using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderTrap : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;


    #region Unity methods

    private void Update() {

        transform.Rotate(transform.up, rotationSpeed);
    }

    #endregion
}
