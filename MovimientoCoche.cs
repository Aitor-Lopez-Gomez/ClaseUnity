using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovimientoCoche : MonoBehaviour
{
    public float aceleracion = 20f;
    public float velocidadMaxima = 18f;
    public float velocidadGiro = 90f;
    public float fuerzaFreno = 8f;

    private Rigidbody rb;

    private float acelerar;
    private float girar;
    private bool frenar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Hace que el coche sea más difícil de volcar
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void Update()
    {
        acelerar = Input.GetAxis("Vertical");
        girar = Input.GetAxis("Horizontal");

        frenar = Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate()
    {
        Vector3 velocidadHorizontal =
            new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        // ACELERAR
        if (velocidadHorizontal.magnitude < velocidadMaxima)
        {
            rb.AddForce(
                transform.forward * acelerar * aceleracion,
                ForceMode.Acceleration
            );
        }

        // GIRAR
        if (velocidadHorizontal.magnitude > 0.5f)
        {
            float giro =
                girar * velocidadGiro * Time.fixedDeltaTime;

            Quaternion nuevaRotacion =
                Quaternion.Euler(0, giro, 0);

            rb.MoveRotation(rb.rotation * nuevaRotacion);
        }

        // FRENO
        if (frenar)
        {
            Vector3 fuerza =
                new Vector3(-rb.linearVelocity.x, 0, -rb.linearVelocity.z);

            rb.AddForce(
                fuerza * fuerzaFreno,
                ForceMode.Acceleration
            );
        }
    }
}