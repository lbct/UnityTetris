using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figura : MonoBehaviour
{
    public static bool[,] tablero = new bool[13, 27];

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Bloquear()
    {
        int x = (int)(transform.position).x;
        int y = (int)(transform.position).y;
        
        tablero[x, y] = true;
    }

    public bool VerificarEspacioLibre()
    {
        int x = (int)(transform.position).x;
        int y = (int)(transform.position).y;
        if (x < tablero.GetLength(0) && y < tablero.GetLength(1)
            && x >= 0 && y >= 0)
            return !tablero[x, y];
        return false;
    }
}
