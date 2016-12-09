using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{

    #region Variabili
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bullied;
    [SerializeField] private GameObject[] badGuys;

    [SerializeField] private GUISkin pauseSkin;

    bool paused = false;
    bool showPauseMenu = false;
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

    void OnGUI()
    {
        if (showPauseMenu)
        {
            GUI.skin = pauseSkin;
            GUI.color = Color.black;
            GUI.Label(new Rect(Screen.width / 2 - 70, Screen.height / 2 - 30, 200, 300), "PAUSE!");
        }

    }

    #endregion

    #region Funzioni interne

    bool PauseControl () //se il tempo scorre lo blocca e mette pausa a vero e viceversa
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            player.GetComponent<PlayerMovement>().enabled = true;
            bullied.GetComponent<BulliedMovement>().enabled = true;
            for (int i = 0; i < badGuys.Length; i++)
                badGuys[i].GetComponent<BadGuysMovement>().enabled = true;
            showPauseMenu = false;
            return false;
        }
        else
        {
            Time.timeScale = 0f;
            player.GetComponent<PlayerMovement>().enabled = false;
            bullied.GetComponent<BulliedMovement>().enabled = false;
            for (int i = 0; i < badGuys.Length; i++)
                badGuys[i].GetComponent<BadGuysMovement>().enabled = false;
            showPauseMenu = true;
            return true;
        }
    }

    #endregion



}
