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

    public abstract void TrackFire();
    protected abstract void Fire();
}
