using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FormaFigura
{
    public string Nombre { get; set; }
    public List<Vector3> Posiciones { get; set; }
}

public class Spawner : MonoBehaviour
{
    public GameObject[] Prefabs;

    public Figura cubo_base;

    private float contadorTiempo;

    private float pos_y_inicial = 22f;
    private float pos_x_max = 12;
    private float pos_x_min = 0;

    private Movimiento ultimoGenerado;
    private List<Figura> figuras;

    private List<FormaFigura> formasFiguras = new List<FormaFigura>()
    {
        new FormaFigura()
        {
            Posiciones = new List<Vector3>()
            {
                new Vector3(0, 2, 0),
                new Vector3(1, 2, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 0)
            },
            Nombre = "Figura C"
        },
        new FormaFigura()
        {
            Posiciones = new List<Vector3>()
            {
                new Vector3(0, 3, 0),
                new Vector3(0, 2, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 0, 0)
            },
            Nombre = "Figura I"
        },
        new FormaFigura()
        {
            Posiciones = new List<Vector3>()
            {
                new Vector3(1, 1, 0),
                new Vector3(1, 0, 0),
                new Vector3(2, 1, 0),
                new Vector3(0, 1, 0),
                new Vector3(1, 2, 0)
            },
            Nombre = "Figura Mas"
        }
    };

    void Start()
    {
        contadorTiempo = Time.time;
        figuras = new List<Figura>();
    }

    private void Update()
    {
        if(Time.time - contadorTiempo >= 3 && (ultimoGenerado == null || ultimoGenerado.bloqueado))
        {
            contadorTiempo = Time.time;
            instanciar();
        }
        List<Figura> elimList = new List<Figura>();
        for(int i = 0; i < Figura.tablero.GetLength(1); i++)
        {
            bool eliminarFila = true;
            for(int j=0;j<Figura.tablero.GetLength(0); j++)
            {
                if (eliminarFila)
                    eliminarFila = Figura.tablero[j, i];
            }
            if (eliminarFila)
            {
                for(int j =0;j< Figura.tablero.GetLength(0); j++)
                {
                    var fig = figuras.FirstOrDefault(f => ((int)f.transform.position.x) == j && ((int)f.transform.position.y) == i);
                    if (fig != null)
                        elimList.Add(fig);
                }
            }
        }
        if (elimList.Count > 0)
        {
            var movs = FindObjectsOfType<Movimiento>();
            Figura.tablero = new bool[13, 27];
            foreach (var f in elimList)
            {
                Destroy(f);
                figuras.Remove(f);
            }
            foreach (var mov in movs)
                mov.Desbloquear();
        }
    }

    public void instanciar()
    {
        var formaFigura = formasFiguras[Random.Range(0, formasFiguras.Count)];

        var figura = new GameObject(formaFigura.Nombre);
        figura.AddComponent<Movimiento>();
        figura.transform.position = new Vector3(0, pos_y_inicial);
        foreach(var posUnitaria in formaFigura.Posiciones)
        {
            var obj = Instantiate(cubo_base, posUnitaria, Quaternion.identity, figura.transform);
            obj.transform.localPosition = posUnitaria;
            figuras.Add(obj);
        }
        ultimoGenerado = figura.GetComponent<Movimiento>();
    }
}
