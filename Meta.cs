using UnityEngine;

public class Meta : MonoBehaviour
{
    public GameObject panelVictoria;

    void Start()
    {
        panelVictoria.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panelVictoria.SetActive(true);

            MovimientoJugador movimiento =
                other.GetComponent<MovimientoJugador>();

            if (movimiento != null)
            {
                movimiento.enabled = false;
            }
        }
    }
}