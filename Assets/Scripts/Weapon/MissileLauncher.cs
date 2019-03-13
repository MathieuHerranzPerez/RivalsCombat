using UnityEngine;

public class MissileLauncher : CubeWeapon
{
    public float minLaunchForce = 10f;
    public float maxLaunchForce = 30f;
    public float maxChargeTime = 1f;

    private float currentLaunchForce;
    private float chargeSpeed;
    private bool fired;

    void Start()
    {
        chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
        currentLaunchForce = minLaunchForce;
        fired = false;
    }

    protected override void Fire()
    {
        // reset the cursor
        // cursorImage.fillAmount = 0;

        // instantiate and launch the shell
        fired = true;
        GameObject shellCloneGO = (GameObject) Instantiate(bulletGO, firePoint.position, firePoint.rotation);
        shellCloneGO.GetComponent<Rigidbody>().velocity = currentLaunchForce * firePoint.forward;

        currentLaunchForce = minLaunchForce;

        //FiringAudio.Play();
        //weaponAnimator.SetTrigger("shoot");
    }

    public override void TrackFire()
    {
        if (currentLaunchForce >= maxLaunchForce && fired)
        {
            currentLaunchForce = maxLaunchForce;
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            fired = false;
            currentLaunchForce = minLaunchForce;
        }
        else if (Input.GetButton("Fire1") && !fired)
        {
            currentLaunchForce += chargeSpeed * Time.deltaTime;

            if (currentLaunchForce >= maxLaunchForce)
                currentLaunchForce = maxLaunchForce;
            else
                currentLaunchForce += chargeSpeed * Time.deltaTime;
            // fill the UI cursor
            // cursorImage.fillAmount = currentLaunchForce / maxLaunchForce;
        }
        else if (Input.GetButtonUp("Fire1") && !fired)
        {
            Fire();
        }
    }
}
