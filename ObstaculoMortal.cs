using UnityEngine;

public class ObstaculoMortal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<RespawnJugador>().Reaparecer();
        }
    }
}