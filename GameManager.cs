using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int llaves = 0;
    public int llavesNecesarias = 5;

    public TextMeshProUGUI textoLlaves;
    public GameObject puerta;

    public void RecogerLlave()
    {
        llaves++;

        textoLlaves.text = "Llaves: " + llaves + "/" + llavesNecesarias;

        if (llaves >= llavesNecesarias)
        {
            puerta.SetActive(false);
        }
    }
}