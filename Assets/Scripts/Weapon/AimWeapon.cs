using UnityEngine;
using UnityEngine.UI;

public abstract class AimWeapon : CubeWeapon
{
    [SerializeField]
    protected GameObject cursorUIGO = default;
    [SerializeField]
    protected Image cursorImage = default;
    [SerializeField]
    protected Image cursorImageToFill = default;

    // ---- INTER ----
    protected Quaternion rotation;
    protected bool isShooting = false;

    protected virtual void ActOnCubeAndTrackFire()
    {
        // if the player is clicing on the fire btn
        if (cube.input.RightTrigger > 0.05f)
        {
            if (!isShooting)
            {
                cube.GetCubeController().ImmobilizeForFire();
                cursorUIGO.SetActive(true);
            }
            isShooting = true;
        }
        else if (cube.input.RightTrigger < 0.05f)
        {
            if (isShooting)
            {
                cube.GetCubeController().LetFree();
                cursorUIGO.SetActive(false);
            }
            rotation = transform.rotation;
            isShooting = false;
        }

        TrackFire(cube.input.LeftTrigger, cube.input.RightTrigger, cube.input.IsButtonDown(PlayerInput.Button.B), cube.input.IsButton(PlayerInput.Button.B), cube.input.IsButtonUp(PlayerInput.Button.B));
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
        float x = cube.input.Horizontal;    // -cube.input.Horizontal ?
        float y = cube.input.Vertical;      // -cube.input.Vertical ?
        if (x != 0.0 || y != 0.0)
        {
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            // cube.GetCubeController().RotateForAim(angle);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
