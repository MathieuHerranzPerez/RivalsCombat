using UnityEngine;

public class MissileLauncher : CubeWeapon
{
    [SerializeField]
    private float minLaunchForce = 5f;
    [SerializeField]
    private float maxLaunchForce = 30f;
    [SerializeField]
    private float maxChargeTime = 1.5f;
    [SerializeField]
    private float recoilForceDiv = 6f;

    // ---- INTERN ----
    private float currentLaunchForce;
    private float chargeSpeed;
    private bool fired;

    private float lastRightTriggerValue = 0f;

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

        Vector3 heading = cube.transform.position - firePoint.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        cube.GetCubeController().ApplyForce(direction * (currentLaunchForce / recoilForceDiv));

        currentLaunchForce = minLaunchForce;

        //FiringAudio.Play();
        //weaponAnimator.SetTrigger("shoot");
    }

    public override void TrackFire(float triggerValue, bool isFireBtnDown, bool isFireBtn, bool isFireBtnUp)
    {
        if (currentLaunchForce >= maxLaunchForce && fired)
        {
            currentLaunchForce = maxLaunchForce;
        }
        else if((triggerValue > 0.05f && lastRightTriggerValue < 0.05f) || isFireBtnDown)
        {
            fired = false;
            currentLaunchForce = minLaunchForce;
        }
        else if(((triggerValue > 0.05f && lastRightTriggerValue > 0.05f) || isFireBtn) && !fired)
        {
            currentLaunchForce += chargeSpeed * Time.deltaTime;

            if (currentLaunchForce >= maxLaunchForce)
                currentLaunchForce = maxLaunchForce;
            else
                currentLaunchForce += chargeSpeed * Time.deltaTime;
            // fill the UI cursor
            // cursorImage.fillAmount = currentLaunchForce / maxLaunchForce;
        }
        else if (((triggerValue < 0.05f && lastRightTriggerValue > 0.05f) || isFireBtnUp) && !fired)
        {
            Fire();
        }

        lastRightTriggerValue = cube.input.RightTrigger;
    }
}
