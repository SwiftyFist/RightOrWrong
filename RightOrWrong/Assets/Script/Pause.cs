using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{

    #region Variabili
    bool paused = false;
    #endregion

    #region Fuznioni per Unity

    void Awake()
    {

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            paused = PauseControl();
        }
    }

    #endregion

    #region Funzioni interne

    bool PauseControl () //se il tempo scorre lo blocca e mette pausa a vero e viceversa
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return false;
        }
        else
        {
            Time.timeScale = 0f;
            return true;
        }
    }

    #endregion



}
