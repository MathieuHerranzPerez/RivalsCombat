using System;
using UnityEngine;

[Serializable]
public abstract class CubeWeapon : MonoBehaviour
{
    public string weaponName;
    public Sprite weaponImage = default;

    [Header("Setup")]
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

    public abstract void TrackFire(float triggerValue, bool isFireBtnDown, bool isFireBtn, bool isFireBtnUp);
    protected abstract void Fire();
}
