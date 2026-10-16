using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<RespawnJugador>()
                 .GuardarCheckpoint(transform.position);
        }
    }
}