using UnityEngine;

public class SwordPhysics : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private Sword sword = default;

    void OnTriggerEnter(Collider other)
    {
        sword.Hit(other);
    }
}
