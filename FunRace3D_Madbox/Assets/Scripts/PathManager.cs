using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{

    public int currentPoint;

    private PlayerController player;



    #region Unity methods

    private void Awake() {

        player = FindObjectOfType<PlayerController>();
        InitPath();
    }

    #endregion


    #region Public methods

    public Transform GetNextPoint() {

        currentPoint++;

        if (currentPoint > transform.childCount - 1)
            return null;
        
        return transform.GetChild(currentPoint).transform;
    }

    #endregion


    #region Private methods

    private void InitPath() {

        currentPoint = 0;

    }

    #endregion

}
