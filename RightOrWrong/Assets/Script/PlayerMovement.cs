/*//Modifiche del 2/12/2016
 * 
 * Aggiunta la parte per il dash 
 * 
 */

using UnityEngine;
using System.Collections;

//Richiesta di un RigidBody2D e creazione in caso di assenza
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{

    #region Variabili

    //Movement key
    [SerializeField] private KeyCode moveUp;
    [SerializeField] private KeyCode moveDown;
    [SerializeField] private KeyCode moveLeft;
    [SerializeField] private KeyCode moveRight;
    [SerializeField] private KeyCode dash; //Tasto per il dash

    [Range (0.00f, 10.00f)] [SerializeField] private float mySpeed; //Velocità di movimento
    [Range(0.00f, 100.00f)] [SerializeField] private float dashSpeed; //Velocità di movimento
    public Rigidbody2D myRigidBody2D; //Variabile contenente il RigidBody

    #endregion

    #region Funzioni per Unity

    void Awake ()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>(); //Assegnamento RigidBody2D
    }

	void Update ()
    {
        float xVelocity = myRigidBody2D.velocity.x;
        float yVelocity = myRigidBody2D.velocity.y;

        //Controllo quale tasto viene premuto e muovo di conseguenza
        if (Input.GetKeyDown(dash))
        {
            int direction = SelectDirection(xVelocity, yVelocity);
            Dash(direction);
        }
        else if (Input.GetKey(moveUp) && Input.GetKey(moveRight))            //Movimento diagonale Alto Destra
            myRigidBody2D.velocity = new Vector3(mySpeed, mySpeed, 0); 
        else if (Input.GetKey(moveDown) && Input.GetKey(moveRight))     //Movimento diagonale Basso Destra
            myRigidBody2D.velocity = new Vector3(mySpeed, mySpeed * -1);
        else if (Input.GetKey(moveUp) && Input.GetKey(moveLeft))        //Movimento diagonale Aldo Sinistra
            myRigidBody2D.velocity = new Vector3(mySpeed * -1, mySpeed, 0);
        else if (Input.GetKey(moveDown) && Input.GetKey(moveLeft))      //Movimento diagonale Basso Sinistra
            myRigidBody2D.velocity = new Vector3(mySpeed * -1, mySpeed * -1, 0);
        else if (Input.GetKey(moveUp))                                  //Movimento Alto
            myRigidBody2D.velocity = new Vector3(0, mySpeed, 0);
        else if (Input.GetKey(moveDown))                                //Movimento Basso
            myRigidBody2D.velocity = new Vector3(0, mySpeed * -1, 0);
        else if (Input.GetKey(moveLeft))                                //Movimento Sinistra
            myRigidBody2D.velocity = new Vector3(mySpeed * -1, 0, 0);
        else if (Input.GetKey(moveRight))                               //Movimento Destra
            myRigidBody2D.velocity = new Vector3(mySpeed, 0, 0);
        else
            myRigidBody2D.velocity = new Vector3(0, 0, 0);

	}

    void OnCollisionEnter2D(Collision2D colInfo)
    {
        if (Input.GetKey(dash))
        {
            GameManager.score(colInfo.collider.name);
            GameObject.FindWithTag("Player").GetComponent<AudioSource>().Play();
        }
    }

    #endregion

    #region Funzioni interne

    //Effettua il dash
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


    }

    //Seleziona la direzione per il dash
    int SelectDirection(float x, float y)
    {
        //Variabili per la direzione della velocità
        int isXdirectionPositive = IsDirectionPositive(x);
        int isYdirectionPositive = IsDirectionPositive(y);

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

        if (isXdirectionPositive == -1 && isYdirectionPositive == -1)
            return -1;
        else if (isXdirectionPositive == 1 && isYdirectionPositive == -1)
            return -2;
        else if (isXdirectionPositive == -1 && isYdirectionPositive == 1)
            return 2;
        else if (isXdirectionPositive == 1 && isYdirectionPositive == 1)
            return 1;
        else if (isXdirectionPositive == -1)
            return -4;
        else if (isXdirectionPositive == 1)
            return 4;
        else if (isYdirectionPositive == -1)
            return -5;
        else if (isYdirectionPositive == 1)
            return 5;
        else
            return 0;
    }

    //Controllo della direzione della velocità
    int IsDirectionPositive(float X)
    {
        if (X > 0)
            return 1;
        else if (X < 0)
            return -1;
        else
            return 0;
    }

    #endregion

}
