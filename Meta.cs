using UnityEngine;

public class Meta : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CarreraManager.Instance != null)
            {
                CarreraManager.Instance.CruzarMeta();
            }
        }
    }
}