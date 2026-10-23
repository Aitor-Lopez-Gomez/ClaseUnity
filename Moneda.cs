using UnityEngine;

public class Llave : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().RecogerLlave();
            Destroy(gameObject);
        }
    }
}