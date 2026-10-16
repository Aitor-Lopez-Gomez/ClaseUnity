using UnityEngine;

public class RespawnJugador : MonoBehaviour
{
    private Vector3 checkpoint;
    private CharacterController controller;

    void Start()
    {
        checkpoint = transform.position;
        controller = GetComponent<CharacterController>();
    }

    public void GuardarCheckpoint(Vector3 posicion)
    {
        checkpoint = posicion;
        Debug.Log("Checkpoint guardado");
    }

    public void Reaparecer()
    {
        controller.enabled = false;
        transform.position = checkpoint;
        controller.enabled = true;
    }
}