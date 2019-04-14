using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField]
    private GameObject destroyedVersionPrefab = default;

    public void Destroy()
    {
        GameObject destroyedVersionClone = (GameObject)Instantiate(destroyedVersionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(destroyedVersionClone, 4f);
    }

    public void Destroy(Color color) {
        GameObject destroyedVersionClone = (GameObject) Instantiate(destroyedVersionPrefab, transform.position, transform.rotation);
        // set the material to all the children
        foreach(Transform trans in destroyedVersionClone.transform)
        {
            MeshRenderer mr = trans.GetComponent<MeshRenderer>();
            if(mr)
            {
                mr.material.color = color;
            }
        }

        Destroy(gameObject);
        Destroy(destroyedVersionClone, 4f);
    }
}
