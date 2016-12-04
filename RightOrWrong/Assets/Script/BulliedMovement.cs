
/*//Modifiche del 24/11/2016
 * Nelle Funzioni Interne IsInsideX e IsInsideY riduco il range della x e della y in
 * modo da evitare che il bullizzado finisca in un angolo e dare la possibilità ai bulli 
 * di andargli dietro.
 * 
 * Trovato un bug(0) si blocca al limite o lo ignora
 * 
 * Rielaborati i controlli sulla direzione e sul range, ho reso più 
 * indipendete la funzione Movement spostando in una funzione esterna
 * il controllo sulla direzione, così ho risolto il bug(0)
 * 
 * Trovato un bug(1) al limite si blocca sulla prima sponda
 * 
 * Ho inserito il controllo sul limite nella funzione SelectDirection in modo
 * da risolvere il bug(1) e allo stesso tempo ridurre il numero di parametri
 * di movement a due evitando di passare anche li una transform.
 * 
 * Sostituita la struttura ad IF in movement con uno switch per ottimizzare.
 * 
 * Ridotto il numero di parametri per movement a uno, ho tolto il parametro speed
 * pensando di utilizzare direttamente la variabile della classe, 
 * NB:è importante per la portabilità del codice però usare lo stesso nome anche nello script per i bulli
 * 
 * Il bullizzato trema quando è tra più soggetti alla stezza distanza e quando 
 * si allontana dal follow del player 
 */

/*//Modifiche del 25//11/2016
 * Resi modificabili da Inspector i valori massimi di range per il movimento dell ia del bullo
 * Reso modificabile da Inspector il valore di speedUp nel caso il bullo sia cattivo
 * 
 */

/*//Modifiche del 28//11/2016
* Il bullizzato si ferma prima dello scontro con il player in modo da evitare push
* 
*/

using System; 
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class BulliedMovement : MonoBehaviour
{

    #region Variabili

    //Bullied
    [Range(0.00f, 10.00f)] [SerializeField] private float mySpeed; //Velocità di movimento
    private Rigidbody2D myRigidBody2D;      //Variabile contenente il RigidBody2D del Bullied
    private Transform myTransform;          //Variabile contenente la Transform del Bullied
    [SerializeField] private float xMax, xMin, yMax, yMin; //Variabili contenenti i limiti di movimento per l`IA
    [SerializeField] private float speedUp; //Valore di speedUp nel caso il player sia cattivo

    //BadGuys
    [SerializeField] private Transform[] badGuysTransforms;   //Vettore "dinamico" contenente le Transform di tutti i bulli
    private Transform closestBadGuy;                //Bullo piú vicino

    //Player
    [SerializeField] private Transform playerTransform;      //Variabile contenente la Transform del Player
    [SerializeField] bool playerIsGood;      //La variabile indica se il player è buono o meno per ora la tengo così per i test, poi sarà da adattare
    [SerializeField] float minDistanceForTheFollow; //Indica il valore minimo della distanza da cui il bullied inizia a seguire il player

    #endregion

    #region Funzioni per unity

    void Awake()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>(); //Assegnamento RigidBody2D
        myTransform = GetComponent<Transform>();     //Assegnamento Transform
        //Se il player è cattivo aumenta la velocità
        //NB È necessario riavviare il livello per la modifica della velocità nel caso 
        //il player cambiasse orientamento
        if (!playerIsGood)
            mySpeed = mySpeed + speedUp;
    }

    void Update()
    {
        if (GameManager.playerScore < 0)
            playerIsGood = false;
        else
            playerIsGood = true;

        int direction; //Contenitore per la direzione da prendere
        int CBG = ClosestBadGuy(); //Numero del badGuy più vicino

        //Controlla se il player è buono e se è in range per il follow       
        if (HaveToFollow())
        {
            //Se il player è buono ed è all`interno del range
            //Viene decisa la direzione
            direction = SelectDirection(myTransform, playerTransform, 1);
            //Richiamato il movimento
            Movement(direction);
        }        
        else
        {
            //Se il player non è buono ed è più vicino rispetto agli altri blulli scappa nella direzione opposta
            if (DistanceBetweenTwoPoint(badGuysTransforms[CBG], myTransform) > DistanceBetweenTwoPoint(playerTransform, myTransform) && !playerIsGood)
            {
                direction = SelectDirection(myTransform, playerTransform, -1);
                Movement(direction);
            }
            //Altrimenti scappa dal bullo più vicino
            else
            {
                direction = SelectDirection(myTransform, badGuysTransforms[CBG], -1);
                Movement(direction);
            }
        }


    }
    
    #endregion

    #region Funzioni interne

    int ClosestBadGuy() //Attraverso il calcolo della distanza tra due punti sul piano determina il badGuy più vicino
    {
        int clos = 0;
        double minDistance = DistanceBetweenTwoPoint(badGuysTransforms[0], myTransform);

        for (int i = 0; i < badGuysTransforms.Length - 1; i++)
        {
            double distance = DistanceBetweenTwoPoint(badGuysTransforms[i + 1], myTransform);
            if (minDistance > distance)
            {
                clos = i + 1;
                minDistance = distance;
            }
        }

        return clos;
    }

    double DistanceBetweenTwoPoint(Transform A, Transform B) //Calcola la distanza tra due punti
    {
        float deltaX = A.position.x - B.position.x;
        float deltaY = A.position.y - B.position.y;
        double distance = (Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2)));

        return Math.Abs(distance);
    }

    bool HaveToFollow() //Verifica se il player è buono e se è all interno della distanza minima per il follow 
    {
        if (playerIsGood && (DistanceBetweenTwoPoint(playerTransform, myTransform) <= minDistanceForTheFollow))
            return true;
        else
            return false;
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
            default: //Fermo
                myRigidBody2D.velocity = new Vector3(0, 0, 0);
                break;
        }
    }

    //Seleziona la direzione da prendere controllando anche il limite, HaveToFollow(1 segue, -1 scappa)
    int SelectDirection (Transform A, Transform B, int HaveToFollow)
    {
        bool isInsideXsup;
        bool isInsideYsup;
        bool isInsideXinf;
        bool isInsideYinf;

        //L assegnamento del limite è fatto in modo da cambiare in base a se deve seguire o scappare
        //Anche il valore della direzione è scelto per essere l opposto della direzione opposta
        //in modo da poter fare la metà dei controlli semplicemente moltiplicando per il valore HaveToFollow
        //variabile flag per il controllo di segue o scappa

        if (HaveToFollow > 0)
        {
            isInsideXsup = IsInsideXsup(A.position.x);
            isInsideYsup = IsInsideYsup(A.position.y);
            isInsideXinf = IsInsideXinf(A.position.x);
            isInsideYinf = IsInsideYinf(A.position.y);
        }
        else
        {
            isInsideXsup = IsInsideXinf(A.position.x);
            isInsideYsup = IsInsideYinf(A.position.y);
            isInsideXinf = IsInsideXsup(A.position.x);
            isInsideYinf = IsInsideYsup(A.position.y);
        }

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
        if (A.position.x > B.position.x + 1 && A.position.y > B.position.y + 1 && isInsideXinf && isInsideYinf)
            return -1 * HaveToFollow;
        else if (A.position.x < B.position.x - 1 && A.position.y > B.position.y + 1 && isInsideXsup && isInsideYinf)
            return -2 * HaveToFollow;
        else if (A.position.x > B.position.x + 1 && A.position.y < B.position.y - 1 && isInsideXinf && isInsideYsup)
            return 2 * HaveToFollow;
        else if (A.position.x < B.position.x - 1 && A.position.y < B.position.y - 1 && isInsideXsup && isInsideYsup)
            return 1 * HaveToFollow;
        else if (A.position.x > B.position.x + 1 && isInsideXinf)
            return -4 * HaveToFollow;
        else if (A.position.x < B.position.x - 1 && isInsideXsup)
            return 4 * HaveToFollow;
        else if (A.position.y > B.position.y + 1 && isInsideYinf)
            return -5 * HaveToFollow;
        else if (A.position.y < B.position.y - 1 && isInsideYsup)
            return 5 * HaveToFollow;
        else
            return 0;
    }

    //Controllo dei limiti
    bool IsInsideXsup (float X)
    {
        if (X <= xMax)
            return true;
        else
            return false;
    }
    bool IsInsideXinf (float X)
    {
        if (X >= xMin)
            return true;
        else
            return false;
    }
    bool IsInsideYsup (float Y)
    {
        if (Y <= yMax)
            return true;
        else
            return false;
    }
    bool IsInsideYinf (float Y)
    {
        if (Y >= yMin)
            return true;
        else
            return false;
    }

    #endregion

}
