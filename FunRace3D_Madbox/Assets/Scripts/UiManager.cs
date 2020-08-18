using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private PlayerController player;


    #region Unity methods

    private void Start() {

        InitUi();
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Space)) {
            player.SetStartGame(true);
            HideUi();
        }
    }

    #endregion


    #region Public methods

    public void HideUi() {

        gameObject.SetActive(false);
    }

    #endregion 


    #region Private methods

    private void InitUi() {

        player = FindObjectOfType<PlayerController>();
    }

    #endregion
}
