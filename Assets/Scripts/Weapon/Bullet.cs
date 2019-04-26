﻿using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField]
    protected AudioSource explosionAudio;

    [SerializeField]
    protected float maxDamage = 100f;
    [SerializeField]
    protected float maxLifeTime = 5f;

    // ---- INTERN ----
    protected Rigidbody rBody;

    public void ChangeVelocity(Vector3 direction)
    {
        rBody.velocity = direction * rBody.velocity.magnitude;
    }
}
