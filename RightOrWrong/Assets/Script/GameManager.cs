using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSurces;

    public static int playerScore = 0; //Tiene conto dei punti del player   
    public static bool playerOrientationChanged = false;//Variabile flag per il cambio di stato
    public static void score (string subjectName)//Assegna i punti al player
    {
        if (subjectName == "Bullied")
        {
            if (playerScore > -10)
                playerScore--;
            if (playerScore + 1 == 0)
                playerOrientationChanged = true;
            else
                playerOrientationChanged = false;                
        }
        else if (subjectName == "BadGuy0" || subjectName == "BadGuy1" 
            || subjectName == "BadGuy2" || subjectName == "BadGuy3")
        {
            if (playerScore < 10)
                playerScore++;
            if (playerScore - 1 == -1)
                playerOrientationChanged = true;
            else
                playerOrientationChanged = false;
        }

        Debug.Log("Player score = " + playerScore);
    }

    

    void Awake()
    {
    }

    void Update ()
    {
        if (playerOrientationChanged)
            PlayerOrientationChanged();
    }

    void PlayerOrientationChanged ()
    {

        if (playerScore > -1)
        {
            //Cambio di suono
            audioSurces[1].Stop();
            audioSurces[0].Play();
        }
        else
        {
            //Cambio di suono
            audioSurces[0].Stop();
            audioSurces[1].Play();
        }

        playerOrientationChanged = false;
    }
}
