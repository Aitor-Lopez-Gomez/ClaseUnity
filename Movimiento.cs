using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 6f;
    public float velocidadGiro = 100f;

    [Header("Salto")]
    public float fuerzaSalto = 7f;
    public float gravedad = -20f;

    [Header("Cámara")]
    public Transform camara;

    private CharacterController controller;
    private float velocidadVertical;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Si no asignamos cámara manualmente, usa la Main Camera
        if (camara == null && Camera.main != null)
        {
            camara = Camera.main.transform;
        }
    }

    void Update()
    {
        MoverJugador();
    }

    void MoverJugador()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Dirección basada en la cámara
        Vector3 delante = camara.forward;
        Vector3 derecha = camara.right;

        delante.y = 0;
        derecha.y = 0;

        delante.Normalize();
        derecha.Normalize();

        Vector3 direccion = delante * vertical + derecha * horizontal;

        // Evita ir más rápido en diagonal
        if (direccion.magnitude > 1f)
        {
            direccion.Normalize();
        }

        // Girar suavemente hacia donde caminamos
        if (direccion.magnitude > 0.1f)
        {
            Quaternion giroObjetivo = Quaternion.LookRotation(direccion);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                giroObjetivo,
                velocidadGiro * Time.deltaTime
            );
        }

        // Si está en el suelo
        if (controller.isGrounded)
        {
            velocidadVertical = -2f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocidadVertical = fuerzaSalto;
            }
        }

        // Gravedad
        velocidadVertical += gravedad * Time.deltaTime;

        Vector3 movimientoFinal = direccion * velocidad;
        movimientoFinal.y = velocidadVertical;

        controller.Move(movimientoFinal * Time.deltaTime);
    }
}