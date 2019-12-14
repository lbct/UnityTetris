using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    private Vector3 puntoRotacion;
    private float tiempoPrevio;
    private float tiempoCaida = 0.8f;
    public static int altura = 26;
    public static int anchura = 13;

    public bool bloqueado;

    void Start()
    {
        bloqueado = false;
        var figuras = GetComponentsInChildren<Figura>();
        var color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
        foreach (var figura in figuras)
        {
            var mesh = figura.GetComponentInChildren<MeshRenderer>();
            mesh.material.color = color;
        }
    }

    public void Desbloquear()
    {
        bloqueado = false;
    }

    void Update()
    {
        if (!bloqueado)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);
                if (!MovimientoValido())
                    transform.position -= new Vector3(-1, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1, 0, 0);
                if (!MovimientoValido())
                    transform.position -= new Vector3(1, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.RotateAround(transform.TransformPoint(puntoRotacion), new Vector3(0, 0, 1), 90);
                if (!MovimientoValido())
                    transform.RotateAround(transform.TransformPoint(puntoRotacion), new Vector3(0, 0, 1), -90);
            }

            if (Time.time - tiempoPrevio > (Input.GetKey(KeyCode.DownArrow) ? tiempoCaida / 10 : tiempoCaida))
            {
                transform.position += new Vector3(0, -1, 0);
                if (!MovimientoValido())
                {
                    transform.position -= new Vector3(0, -1, 0);
                    bloqueado = true;
                    var figuras = GetComponentsInChildren<Figura>();
                    foreach (var figura in figuras)
                        figura.Bloquear();
                }
                tiempoPrevio = Time.time;
            }
        }
    }


    bool MovimientoValido()
    {
        bool valido = true;
        var figuras = GetComponentsInChildren<Figura>();
        foreach (var figura in figuras)
        {
            if (valido)
                valido = figura.VerificarEspacioLibre();
        }
        return valido;
    }

}
