using UnityEngine;
using TMPro;

public class CarreraManager : MonoBehaviour
{
    public static CarreraManager Instance;

    public int numeroCheckpoints = 4;
    public int vueltasNecesarias = 3;

    public TextMeshProUGUI textoVueltas;

    private int siguienteCheckpoint = 0;
    private int vueltaActual = 1;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ActualizarTexto();
    }

    public void PasarCheckpoint(int numero)
    {
        if (numero == siguienteCheckpoint)
        {
            siguienteCheckpoint++;

            Debug.Log(
                "Checkpoint " + numero + " correcto"
            );
        }
    }

    public void CruzarMeta()
    {
        // Solo cuenta si hizo todos los checkpoints
        if (siguienteCheckpoint == numeroCheckpoints)
        {
            siguienteCheckpoint = 0;

            if (vueltaActual >= vueltasNecesarias)
            {
                Debug.Log("¡CARRERA TERMINADA!");

                if (textoVueltas != null)
                {
                    textoVueltas.text =
                        "¡CARRERA TERMINADA!";
                }

                return;
            }

            vueltaActual++;

            ActualizarTexto();
        }
    }

    void ActualizarTexto()
    {
        if (textoVueltas != null)
        {
            textoVueltas.text =
                "Vuelta: " +
                vueltaActual +
                "/" +
                vueltasNecesarias;
        }
    }
}