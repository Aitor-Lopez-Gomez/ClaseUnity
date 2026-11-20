using UnityEngine;

public class CheckpointCoche : MonoBehaviour
{
    public int numeroCheckpoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnCoche respawn =
                other.GetComponent<RespawnCoche>();

            if (respawn != null)
            {
                respawn.GuardarCheckpoint(
                    transform.position,
                    transform.rotation
                );
            }

            if (CarreraManager.Instance != null)
            {
                CarreraManager.Instance.PasarCheckpoint(
                    numeroCheckpoint
                );
            }
        }
    }
}