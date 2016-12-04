using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
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
}
