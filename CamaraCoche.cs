using UnityEngine;

public class CamaraCoche : MonoBehaviour
{
    public Transform coche;

    public Vector3 distancia = new Vector3(0, 5, -8);
    public float suavidad = 5f;

    void LateUpdate()
    {
        if (coche == null)
            return;

        Vector3 posicionDeseada =
            coche.TransformPoint(distancia);

        transform.position = Vector3.Lerp(
            transform.position,
            posicionDeseada,
            suavidad * Time.deltaTime
        );

        transform.LookAt(coche);
    }
}