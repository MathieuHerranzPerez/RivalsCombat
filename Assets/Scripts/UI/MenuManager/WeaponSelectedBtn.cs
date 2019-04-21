using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponSelectedBtn : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private string weaponsFolderPath = "Weapons";
    [SerializeField]
    private Image selectedWeaponImg = default;
    [SerializeField]
    private GameObject btnWeaponContainerLayoutGO = default;
    [SerializeField]
    private EventSystem eventSystem = default;
    [SerializeField]
    private GameObject btnWeaponPrefab = default;
    [SerializeField]
    private PersonalisationPanel personalisationPanel = default;

    // ---- INTERN ----
    //private List<CubeWeapon> listWeapon = new List<CubeWeapon>();
    private List<GameObject> listWeapon = new List<GameObject>();
    private Object[] weapons;
    //private CubeWeapon weaponSelected;
    private GameObject weaponSelectedPrefab;

    private MyButton firstBtnSelected;

    void Start()
    {
        InitBtn();
        ChangeWeapon(listWeapon[0]);
    }

    public void DisplayWeaponBtn()
    {
        btnWeaponContainerLayoutGO.SetActive(true);
        eventSystem.SetSelectedGameObject(firstBtnSelected.gameObject);
    }

    //public void ChangeWeapon(CubeWeapon weapon)
    //{
    //    weaponSelected = weapon;
    //    selectedWeaponImg.sprite = weapon.weaponImage;
    //    HideWeaponBtn();
    //}

    public void ChangeWeapon(GameObject weaponPrefab)
    {
        weaponSelectedPrefab = weaponPrefab;
        selectedWeaponImg.sprite = weaponSelectedPrefab.GetComponent<CubeWeapon>().weaponImage;
        HideWeaponBtn();
        personalisationPanel.NotifyWeaponChanged(weaponPrefab);
}

    private void HideWeaponBtn()
    {
        btnWeaponContainerLayoutGO.SetActive(false);
        eventSystem.SetSelectedGameObject(this.gameObject);
    }

    private void InitBtn()
    {
        // find all the weapons in the resources folder
        weapons = Resources.LoadAll(weaponsFolderPath, typeof(GameObject));

        foreach (Object o in weapons)
        {
            if(((GameObject)o).GetComponent<CubeWeapon>())
            {
                listWeapon.Add((GameObject) o);
            }
        }

        MyButton previousBtn = null;
        foreach (GameObject cubeWeaponPrefab in listWeapon)
        {
            GameObject btnGO = (GameObject)Instantiate(btnWeaponPrefab, btnWeaponContainerLayoutGO.transform) as GameObject;
            MyButton currentBtn = btnGO.GetComponent<MyButton>();
            MyEventSystemProvider btnEventSystem = btnGO.GetComponent<MyEventSystemProvider>();
            btnEventSystem.eventSystem = this.eventSystem;

            // Set the navigation between the buttons
            Navigation navigation = currentBtn.navigation;
            navigation.mode = Navigation.Mode.Explicit;
            if (previousBtn != null)
            {
                // current
                navigation.selectOnLeft = previousBtn;

                // previous
                Navigation navigationPrevious = previousBtn.navigation;
                navigationPrevious.selectOnRight = currentBtn;
                previousBtn.navigation = navigationPrevious;
            }
            else
            {
                firstBtnSelected = currentBtn;
            }
            currentBtn.navigation = navigation;

            // set the image
            currentBtn.image.sprite = cubeWeaponPrefab.GetComponent<CubeWeapon>().weaponImage;
            // todo give the weapon
            WeaponBtn currentWeaponBtn = currentBtn.GetComponent<WeaponBtn>();
            currentWeaponBtn.Init();
            currentWeaponBtn.SetSelectedBtn(this);
            currentWeaponBtn.SetCubeWeaponPrefab(cubeWeaponPrefab);

            previousBtn = currentBtn;
        }
    }
}
