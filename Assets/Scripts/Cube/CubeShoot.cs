using UnityEngine;

[RequireComponent(typeof(Cube))]
public class CubeShoot : MonoBehaviour
{
    public CubeWeapon weapon;

    // ---- INTER ----
    private Cube cube;

    void Start()
    {
        cube = GetComponent<Cube>();
        weapon.SetCube(cube);
    }

    void Update()
    {
        //if (Input.GetButton("Fire1"))
        //{
        //    weapon.TrackFire();
        //}
        //else if (Input.GetButtonUp("Fire1"))
        //{
        //    weapon.TrackFire();
        //}
        if (cube.input.IsButtonDown(PlayerInput.Button.B) || cube.input.IsButton(PlayerInput.Button.B))
        {
            weapon.TrackFire();
        }
        else if(cube.input.IsButtonUp(PlayerInput.Button.B))
        {
            weapon.TrackFire();
        }
    }
}
