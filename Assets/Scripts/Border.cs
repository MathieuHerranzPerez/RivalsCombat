using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Border : MonoBehaviour
{
    [SerializeField]
    private float explosionRadius = 3f;
    [SerializeField]
    private float explosionForce = 500f;

    // ---- INTERN ----
    private MeshRenderer mesh;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Cube cube = other.GetComponent<Cube>();
        if (cube)
        {
            cube.TakeDamageFormBullet(cube.GetMaxLifePoint());

            Collider[] collidersToAddForce = Physics.OverlapSphere(other.transform.position, explosionRadius);
            foreach (Collider collider in collidersToAddForce)
            {
                Rigidbody targetRigidBody = collider.GetComponent<Rigidbody>();
                if (targetRigidBody)
                {
                    targetRigidBody.AddExplosionForce(explosionForce, other.transform.position, explosionRadius);
                }
            }
        }
    }

    public MeshRenderer GetMesh()
    {
        return mesh;
    }
}
