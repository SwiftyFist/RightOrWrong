using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSurces;
    public static int playerScore = 0; //Tiene conto dei punti del player    
    
    public static void score (string subjectName)//Assegna i punti al player
    {
        if (subjectName == "Bullied")
        {
            if (playerScore > -10)
                playerScore--;
        }
        else if (subjectName == "BadGuy0" || subjectName == "BadGuy1" 
            || subjectName == "BadGuy2" || subjectName == "BadGuy3")
        {
            if (playerScore < 10)
                playerScore++;
        }

        Debug.Log("Player score = " + playerScore);
    }

    void Awake()
    {
    }

    void Update ()
    {
        if (playerScore > -1)
        {
            audioSurces[1].Stop();
            audioSurces[0].Play();
        }
        else
        {
            audioSurces[0].Stop();
            audioSurces[1].Play();
        }
    }

    void SourcesToPlay(int i)
    {
        if (audioSurces[0].isPlaying)
        {
            audioSurces[0].Stop();
            audioSurces[1].Play();
        }
        else if (audioSurces[1].isPlaying)
        {
            audioSurces[1].Stop();
            audioSurces[0].Play();
        }
    }
}
