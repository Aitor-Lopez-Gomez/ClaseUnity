using UnityEngine;

public class RespawnCoche : MonoBehaviour
{
    public float alturaMuerte = -5f;

    private Vector3 posicionRespawn;
    private Quaternion rotacionRespawn;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        posicionRespawn = transform.position;
        rotacionRespawn = transform.rotation;
    }

    void Update()
    {
        // R = reiniciar manualmente
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reaparecer();
        }

        // Si cae del mapa
        if (transform.position.y < alturaMuerte)
        {
            Reaparecer();
        }
    }

    public void GuardarCheckpoint(
        Vector3 posicion,
        Quaternion rotacion)
    {
        posicionRespawn = posicion;
        rotacionRespawn = rotacion;
    }

    public void Reaparecer()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = posicionRespawn;
        transform.rotation = rotacionRespawn;
    }
}