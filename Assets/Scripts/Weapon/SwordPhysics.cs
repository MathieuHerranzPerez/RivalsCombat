using UnityEngine;

public class SwordPhysics : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private Sword sword;

    void OnTriggerEnter(Collider other)
    {
        sword.Hit(other);
    }
}
