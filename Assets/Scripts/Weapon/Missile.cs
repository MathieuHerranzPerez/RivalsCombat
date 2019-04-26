using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Missile : Bullet
{
    [SerializeField]
    private float explosionForce = 10f;
    [SerializeField]
    private float explosionRadius = 5f;
    [SerializeField]
    private GameObject explosionEffectPrefab = default;

    [Header("Camera shake")]
    [SerializeField]
    private float camShakeMagnitude = 0.1f;
    [SerializeField]
    private float camShakeRotation = 0.1f;
    [SerializeField]
    private float camShakeTime = 0.2f;

    // ---- INTERN ----
    protected bool IsCollided = false;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        Destroy(gameObject, maxLifeTime);
    }

    void Update()
    {
        // rotate the missile to follow its gravity curve
        float angle = Mathf.Atan2(rBody.velocity.y, rBody.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    virtual protected void OnCollisionEnter(Collision collision)
    {
        if (!IsCollided)
        {
            IsCollided = true;
            Explode();
        }
    }

    protected virtual void Explode() 
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            Rigidbody targetRigidBody = collider.GetComponent<Rigidbody>();
            if (targetRigidBody)
            {
                Cube target = targetRigidBody.GetComponent<Cube>();
                if (target)
                {
                    float damageAmount = CalculateDamage(targetRigidBody.position);
                    target.TakeDamageFormBullet(damageAmount);
                }
            }
        }

        Collider[] collidersToAddForce = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in collidersToAddForce)
        {
            Rigidbody targetRigidBody = collider.GetComponent<Rigidbody>();
            if (targetRigidBody && targetRigidBody != this.GetComponent<Rigidbody>())
            {
                CubeController cubeController = targetRigidBody.GetComponent<CubeController>();
                if (cubeController)
                {
                    cubeController.CancelDash();
                }
                targetRigidBody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                //AddExplosionForce(targetRigidBody, explosionForce, transform.position, explosionRadius);
            }
        }
            

        // effect
        if(explosionEffectPrefab)
        {
            GameObject explosionGO = Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
            Destroy(explosionGO, 1.5f);
        }
        CameraShake.Instance.Shake(camShakeTime, camShakeMagnitude, camShakeRotation);

        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        Vector3 explosionToTarget = targetPosition - transform.position;
        float explosionDistance = explosionToTarget.magnitude;
        float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;
        float damage = relativeDistance * maxDamage;
        damage = Mathf.Max(0f, damage);

        return damage;
    }

    //private void AddExplosionForce(Rigidbody rb, float explosionForce, Vector3 pos, float explosionRadius)
    //{
    //    Vector3 direction = (rb.position - pos).normalized;
    //    float distance = Vector3.Distance(rb.position, pos);
    //    float explosion = distance * explosionRadius / explosionForce;
    //    rb.velocity += explosion * direction;
    //    Debug.Log("Direction : " + direction);
    //}

    void OnDrawGizmos()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
