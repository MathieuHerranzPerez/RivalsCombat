using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class CubeWeapon : MonoBehaviour
{
    public string weaponName;

    public GameObject bulletGO;
    [SerializeField]
    protected Image cursorImage;
    [SerializeField]
    protected Animator weaponAnimator;
    [SerializeField]
    public Transform firePoint;

    // ---- INTERN ----
    protected Cube cube;

    public void SetCube(Cube cube)
    {
        this.cube = cube;
    }

    public abstract void TrackFire();
    protected abstract void Fire();
}
