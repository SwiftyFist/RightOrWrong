using UnityEngine;
using System.Collections;

public class SenseOfDepth : MonoBehaviour
{

    #region Variabili

    private Transform myTransform; //Variabile usata per allocare la Transform del player
    private float m, q; //Coefficente angolare e Termine noto per il calcolo 

    [SerializeField] private float yMax, yMin; //Y per il calcolo
    [SerializeField] private float MaxScale, MinScale; //Valori di scala per i valori di Y
    #endregion

    #region Funzioni per Unity

    void Awake()
    {
        myTransform = GetComponent<Transform>(); //Assegna la trasformazione del player alla variabile
        FunctionCalc();
    }

    void Update()
    {
        //Modifica il valore di scala in base alla posizione della y
        myTransform.localScale = new Vector3(scaleCalc(myTransform.position.y), scaleCalc(myTransform.position.y), 1);
    }

    #endregion

    #region Funzioni interne

    float scaleCalc(float x) //Calcola il rispettivo valore di scala in base alla posizione
    {
        float y;
        y = (x * (m)) + q;

        return y;
    }

    void FunctionCalc() //Calcola il coefficente angolare e il termine noto per la funzione della retta usata per lo scale
    {
        m = (MinScale + (MaxScale * -1)) / (yMax + (yMin * -1));
        q = MaxScale + ((yMin * m) * -1);
    }

    #endregion

}
