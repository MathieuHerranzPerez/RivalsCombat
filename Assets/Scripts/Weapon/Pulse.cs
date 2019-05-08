using System.Collections;
using UnityEngine;

public class Pulse : Missile
{
    //[SerializeField]
    //private Material shockWaveMaterial;

    // ---- INTERN ----
    private Pulsor pulsor;
    
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        StartCoroutine(DestroyAfterMaxTime());

        //shockWaveMaterial.SetFloat("_Radius", -0.2f);
    }

    public void CallDestruction()
    {
        Explode(null);
    }

    protected override void Explode(GameObject other)
    {
        //shockWaveMaterial.SetFloat("_CenterX", transform.position.x);
        //shockWaveMaterial.SetFloat("_CenterY", transform.position.y);
        //StartCoroutine(ShockWaveEffect());

        pulsor.NotifyCurrentPulseDestroyed();
        base.Explode(other);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (!IsCollided)
        {
            IsCollided = true;
            Explode(collision.gameObject);
        }
    }

    public void SetPulsor(Pulsor pulsor)
    {
        this.pulsor = pulsor;
    }

    private IEnumerator DestroyAfterMaxTime()
    {
        float time = 0f;
        while(time < maxLifeTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        pulsor.NotifyCurrentPulseDestroyed();
        Destroy(gameObject);
    }

    //private IEnumerator ShockWaveEffect()
    //{
    //    float tParam = 0f;
    //    float waveRadius;
    //    while (tParam < 2f)
    //    {
    //        Debug.Log("la : " + tParam);
    //        tParam += Time.deltaTime;
    //        waveRadius = Mathf.Lerp(-0.2f, 20, tParam);
    //        shockWaveMaterial.SetFloat("_Radius", waveRadius);
    //        yield return null;
    //    }
    //}
}
