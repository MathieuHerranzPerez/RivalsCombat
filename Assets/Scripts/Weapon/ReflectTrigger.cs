using UnityEngine;

public class ReflectTrigger : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private Sword sword;

    void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        if (bullet)
        {
            sword.NotifyReflection(bullet);
        }
    }
}
