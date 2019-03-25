using UnityEngine;

[RequireComponent(typeof(Cube))]
public class CubeShoot : MonoBehaviour
{
    public CubeWeapon weapon;

    // ---- INTER ----
    private Cube cube;
    private Quaternion rotation;
    private bool isShooting = false;

    void Start()
    {
        cube = GetComponent<Cube>();
        weapon.SetCube(cube);
        rotation = weapon.transform.rotation;
    }

    void Update()
    {
        // if the player clic on the fire btn
        if(cube.input.IsButtonDown(PlayerInput.Button.B) || cube.input.RightTrigger > 0.05f)
        {
            cube.GetCubeController().ImmobilizeForFire();
            isShooting = true;
        }
        else if(cube.input.IsButtonUp(PlayerInput.Button.B))
        {
            cube.GetCubeController().LetFree();
            rotation = weapon.transform.rotation;
            isShooting = false;
        }

        weapon.TrackFire(cube.input.RightTrigger, cube.input.IsButtonDown(PlayerInput.Button.B), cube.input.IsButton(PlayerInput.Button.B), cube.input.IsButtonUp(PlayerInput.Button.B));

        DisplayWeaponDirection(); // affD
    }

    void LateUpdate()
    {
        if (!isShooting)
        {
            // don't rotate the weapon
            weapon.transform.rotation = rotation;
        }
        else
        {
            Aim();
        }
    }

    private void Aim()
    {
        float x = - cube.input.Horizontal;
        float y = - cube.input.Vertical;
        if (x != 0.0 || y != 0.0)
        {
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            // cube.GetCubeController().RotateForAim(angle);
            weapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void DisplayWeaponDirection()
    {
        Vector3 dir = weapon.firePoint.position - transform.position;
        Debug.DrawRay(transform.position, dir * 2, Color.green);
    }
}
