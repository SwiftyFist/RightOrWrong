/*//Modifiche del 28/11/2016
 * 
 * Trovato bug(0): i bulli si fermano anche quando non pushano altri
 * 
 * Ho spostato il controllo IsPushing da uno unico prima del movimento a
 * uno per ogni direzione nella funzione SelectDirection 
 */

/*//Modifiche del 1/12/2016
 * 
 * Cambio di strategia, i bulli si muovono in diverse posizioni in modo da accerchiare la vittima creando
 * una formazione che mantengono al movimento della vittima, la posizione è determinata da quale bullo sto
 * muovendo attraverso a dei gap 
 */

/*//Modifiche del 2/12/2016
 * 
 * Trovato bug(1): i bulli tremano
 * Risolto aggiungendo un controllo che sostituisce la posizione cercata da un punto a un range
 * 
 * Aggiunta la parte per il dash
 */

using System;
using UnityEngine;
using System.Collections;

public class BadGuysMovement : MonoBehaviour
{

    #region Variabili

    //BadGuy
    [Range(0.00f, 10.00f)] [SerializeField] private float mySpeed; //Velocità di movimento del badguy
    [Range(0, 100)] [SerializeField] float dashSpeed; //Velocità del dash
    private Rigidbody2D myRigidBody2D;      //Variabile contenente il RigidBody2D del badguy
    private Transform myTransform;          //Variabile contenente la Transform del badguy 
    [SerializeField]  private float xMax, xMin, yMax, yMin; //Variabili contenenti i limiti di movimento per l`IA
    [Range(0, 3)] [SerializeField]  private int badGuyIndicator; //Indica quale bullo sta muovendo
    private float xGap, yGap; //Sono i Gap che calcolano la posizione verso la quale si muovono i bulli
    private int i; //Variabile contatore usata per timerizzare gli attacchi dai vari bulli

    //Other BadGuys
    [SerializeField] private Transform[] otherBadGuysTransform;

    //Player
    [SerializeField] private Transform playerTransform;      //Variabile contenente la Transform del Player
    [SerializeField] bool playerIsGood;      //La variabile indica se il player è buono o meno per ora la tengo così per i test, poi sarà da adattare

    //Bullied
    [SerializeField]  private Transform bulliedTransform;


    #endregion

    #region Funzioni per Unity

    void Awake ()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>(); //Assegnamento RigidBody2D
        myTransform = GetComponent<Transform>();     //Assegnamento Transform
        FindPosition();
        //Ritarda gli attacchi in base a quale badguy è
        if (badGuyIndicator == 0)
            i = 0;
        else if (badGuyIndicator == 1)
            i = 60;
        else if (badGuyIndicator == 2)
            i = 120;
        else if (badGuyIndicator == 3)
            i = 180;
    }
	
	void Update ()
    {
        i++;
        if (i == 240)
        {
            Dash(selectDashDirection());
            i = 0;
        }
        else
        {
            int direction = SelectDirection(myTransform, bulliedTransform);
            Movement(direction);
        }
	}

    #endregion

    #region Funzioni interne

    double DistanceBetweenTwoPoint(Transform A, Transform B) //Calcola la distanza tra due punti
    {
        float deltaX = A.position.x - B.position.x;
        float deltaY = A.position.y - B.position.y;
        double distance = (Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2)));

        return Math.Abs(distance);
    }

    void Movement(int direction) //Muove l`oggetto nella direzione
    {
        switch (direction)
        {
            case 1: //Diagonale in alto a destra
                myRigidBody2D.velocity = new Vector3(mySpeed, mySpeed, 0);
                break;
            case 2: //Diagonale in alto a sinistra
                myRigidBody2D.velocity = new Vector3(mySpeed * -1, mySpeed, 0);
                break;
            case -2: //Diagonale in basso a destra
                myRigidBody2D.velocity = new Vector3(mySpeed, mySpeed * -1, 0);
                break;
            case -1: //Diagonale in basso a sinistra
                myRigidBody2D.velocity = new Vector3(mySpeed * -1, mySpeed * -1, 0);
                break;
            case 4: //Destra
                myRigidBody2D.velocity = new Vector3(mySpeed, 0, 0);
                break;
            case -4: //Sinistra
                myRigidBody2D.velocity = new Vector3(mySpeed * -1, 0, 0);
                break;
            case 5: //Alto
                myRigidBody2D.velocity = new Vector3(0, mySpeed, 0);
                break;
            case -5: //Basso
                myRigidBody2D.velocity = new Vector3(0, mySpeed * -1, 0);
                break;
            case 0: //Fermo
                myRigidBody2D.velocity = new Vector3(0, 0, 0);
                break;
        }


    }

    //Seleziona la direzione da prendere controllando anche il limite, HaveToFollow(1 segue, -1 scappa)
    int SelectDirection(Transform A, Transform B)
    {
        //Variabili per il controllo del range di movimento
        bool isInsideXsup = IsInsideXsup(A.position.x);
        bool isInsideYsup = IsInsideYsup(A.position.y);
        bool isInsideXinf = IsInsideXinf(A.position.x);
        bool isInsideYinf = IsInsideYinf(A.position.y);

        //Variabili per la posizione target del badGuy
        float targetPositionX = B.position.x + xGap;
        float targetPositionY = B.position.y + yGap;

        /*Direzioni:
        *   1 -> Diagonale in alto a destra
        *   2 -> Diagonale in alto a sinistra
        *  -2 -> Diagonale in basso a destra
        *  -1 -> Diagonale in basso a sinistra
        *   4 -> A destra
        *  -4 -> A sinistra
        *   5 -> In alto
        *  -5 -> In basso
        *   0 -> Fermo
        */
        if ((targetPositionX - 0.1f) < A.position.x && (targetPositionX + 0.1f) > A.position.x && (targetPositionY - 0.1f) < A.position.y && (targetPositionY + 0.1f) > A.position.y)
            return 0;
        else if (A.position.x > targetPositionX && A.position.y > targetPositionY && isInsideXinf && isInsideYinf)
            return -1;
        else if (A.position.x < targetPositionX && A.position.y > targetPositionY && isInsideXsup && isInsideYinf)
            return -2;
        else if (A.position.x > targetPositionX && A.position.y < targetPositionY && isInsideXinf && isInsideYsup)
            return 2;
        else if (A.position.x < targetPositionX && A.position.y < targetPositionY && isInsideXsup && isInsideYsup)
            return 1;
        else if (A.position.x > targetPositionX && isInsideXinf)
            return -4;
        else if (A.position.x < targetPositionX && isInsideXsup)
            return 4;
        else if (A.position.y > targetPositionY && isInsideYinf)
            return -5;
        else if (A.position.y < targetPositionY && isInsideYsup)
            return 5;
        else
            return 0;
    }

    //Controllo dei limiti
    bool IsInsideXsup(float X)
    {
        if (X <= xMax)
            return true;
        else
            return false;
    }
    bool IsInsideXinf(float X)
    {
        if (X >= xMin)
            return true;
        else
            return false;
    }
    bool IsInsideYsup(float Y)
    {
        if (Y <= yMax)
            return true;
        else
            return false;
    }
    bool IsInsideYinf(float Y)
    {
        if (Y >= yMin)
            return true;
        else
            return false;
    }

    void FindPosition () //Assegna i gap in base a quale badGuy è
    {
        switch (badGuyIndicator)
        {
            case 0: //Su y = -x y positiva
                xGap = -1.5f;
                yGap = 1.5f;
                break;
            case 1: //Su y = x y positiva
                xGap = 1.5f;
                yGap = 1.5f;
                break;
            case 2: //Su y = x y negativa
                xGap = -1.5f;
                yGap = -1.5f;
                break;
            case 3: //Su y = -x y negativa
                xGap = 1.5f;
                yGap = -1.5f;
                break;
        }
    }

    void Dash(int direction) //Muove l`oggetto nella direzione
    {
        switch (direction)
        {
            case 1: //Diagonale in alto a destra
                myRigidBody2D.velocity = new Vector3(dashSpeed, dashSpeed, 0);
                break;
            case 2: //Diagonale in alto a sinistra
                myRigidBody2D.velocity = new Vector3(dashSpeed * -1, dashSpeed, 0);
                break;
            case -2: //Diagonale in basso a destra
                myRigidBody2D.velocity = new Vector3(dashSpeed, dashSpeed * -1, 0);
                break;
            case -1: //Diagonale in basso a sinistra
                myRigidBody2D.velocity = new Vector3(dashSpeed * -1, dashSpeed * -1, 0);
                break;
            case 4: //Destra
                myRigidBody2D.velocity = new Vector3(dashSpeed, 0, 0);
                break;
            case -4: //Sinistra
                myRigidBody2D.velocity = new Vector3(dashSpeed * -1, 0, 0);
                break;
            case 5: //Alto
                myRigidBody2D.velocity = new Vector3(0, dashSpeed, 0);
                break;
            case -5: //Basso
                myRigidBody2D.velocity = new Vector3(0, dashSpeed * -1, 0);
                break;
            case 0: //Fermo
                myRigidBody2D.velocity = new Vector3(0, 0, 0);
                break;
        }

        GetComponent<AudioSource>().Play();

    }

    int selectDashDirection() //Seleziona la direzione del dash
    {
        if (badGuyIndicator == 0)
            return -2;
        else if (badGuyIndicator == 1)
            return -1;
        else if (badGuyIndicator == 2)
            return 1;
        else
            return 2;
    }
    
    #endregion
}
