
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 6f;
    public float velocidadGiro = 360f;

    [Header("Salto")]
    public float fuerzaSalto = 7f;
    public float gravedad = -20f;

    [Header("Camara")]
    public Transform camara;
    public float sensibilidadRaton = 3f;
    public float distanciaCamara = 5f;
    public float alturaCamara = 2f;

    private CharacterController controller;
    private float velocidadVertical;

    private float rotacionHorizontal;
    private float rotacionVertical;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (camara == null && Camera.main != null)
        {
            camara = Camera.main.transform;
        }

        if (camara != null)
        {
            // Evitar que la cámara gire con el jugador
            if (camara.IsChildOf(transform))
            {
                camara.SetParent(null);
            }

            rotacionHorizontal = camara.eulerAngles.y;
            rotacionVertical = camara.eulerAngles.x;

            if (rotacionVertical > 180f)
            {
                rotacionVertical -= 360f;
            }

            rotacionVertical = Mathf.Clamp(
                rotacionVertical, -30f, 70f
            );
        }
    }

    void Update()
    {
        GirarCamara();
        MoverJugador();
    }

    void LateUpdate()
    {
        ActualizarCamara();
    }

    void GirarCamara()
    {
        // Girar cámara solo con clic derecho
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            rotacionHorizontal += mouseX * sensibilidadRaton;
            rotacionVertical -= mouseY * sensibilidadRaton;

            rotacionVertical = Mathf.Clamp(
                rotacionVertical, -30f, 70f
            );
        }
    }

    void ActualizarCamara()
    {
        if (camara == null) return;

        Quaternion rotacion = Quaternion.Euler(
            rotacionVertical,
            rotacionHorizontal,
            0f
        );

        Vector3 objetivo = transform.position +
                           Vector3.up * alturaCamara;

        camara.rotation = rotacion;

        camara.position = objetivo -
                          rotacion * Vector3.forward *
                          distanciaCamara;
    }

    void MoverJugador()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 delante = camara != null
            ? camara.forward
            : transform.forward;

        Vector3 derecha = camara != null
            ? camara.right
            : transform.right;

        delante.y = 0;
        derecha.y = 0;

        delante.Normalize();
        derecha.Normalize();

        Vector3 direccion = delante * vertical +
                            derecha * horizontal;

        if (direccion.magnitude > 1f)
        {
            direccion.Normalize();
        }

        // El personaje gira hacia donde camina
        if (direccion.magnitude > 0.1f)
        {
            Quaternion giroObjetivo =
                Quaternion.LookRotation(direccion);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                giroObjetivo,
                velocidadGiro * Time.deltaTime
            );
        }

        // Salto
        if (controller.isGrounded)
        {
            if (velocidadVertical < 0)
            {
                velocidadVertical = -2f;
            }

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

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
