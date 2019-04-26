using UnityEngine;

public class CubeFX : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem jumpingEffect = default;
    [SerializeField]
    private ParticleSystem landingEffect = default;
    [SerializeField]
    private Transform jumpingEffectPoint;

    public void NotifyLanding()
    {
        ParticleSystem p = (ParticleSystem)Instantiate(landingEffect, jumpingEffectPoint.position, transform.rotation);
        Destroy(p.gameObject, 1f);
    }

    public void NotifyJumping()
    {
        //ParticleSystem p = (ParticleSystem)Instantiate(jumpingEffect, jumpingEffectPoint.position, transform.rotation);
        //Destroy(p.gameObject, 1f);
    }
}
