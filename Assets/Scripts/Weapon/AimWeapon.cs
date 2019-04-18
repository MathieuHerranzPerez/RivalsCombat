using UnityEngine;
using UnityEngine.UI;

public abstract class AimWeapon : CubeWeapon
{
    [SerializeField]
    protected GameObject cursorUIGO;
    [SerializeField]
    protected Image cursorImage;

    // ---- INTER ----
    protected Quaternion rotation;
    protected bool isShooting = false;

    protected bool isB = false;

    protected virtual void ActOnCubeAndTrackFire()
    {
        // if the player clic on the fire btn
        if (cube.input.IsButtonDown(PlayerInput.Button.B) || cube.input.RightTrigger > 0.05f)
        {
            isB = cube.input.IsButtonDown(PlayerInput.Button.B);
            if (!isShooting)
            {
                cube.GetCubeController().ImmobilizeForFire();
                cursorUIGO.SetActive(true);
            }
            isShooting = true;
        }
        else if ((isB && cube.input.IsButtonUp(PlayerInput.Button.B)) || (!isB && cube.input.RightTrigger < 0.05f))
        {
            if (isShooting)
            {
                cube.GetCubeController().LetFree();
                cursorUIGO.SetActive(false);
            }
            rotation = transform.rotation;
            isShooting = false;
        }

        TrackFire(cube.input.RightTrigger, cube.input.IsButtonDown(PlayerInput.Button.B), cube.input.IsButton(PlayerInput.Button.B), cube.input.IsButtonUp(PlayerInput.Button.B));
    }

    protected void Init()
    {
        rotation = transform.rotation;
    }

    protected void Aiming() {
        if (!isShooting)
        {
            // don't rotate the weapon
            transform.rotation = rotation;
        }
        else
        {
            Aim();
        }
    }

    protected void Aim()
    {
        float x = -cube.input.Horizontal;
        float y = -cube.input.Vertical;
        if (x != 0.0 || y != 0.0)
        {
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            // cube.GetCubeController().RotateForAim(angle);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
