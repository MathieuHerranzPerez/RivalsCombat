using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private float timeToSpawn = 3f;
    [SerializeField]
    private GameObject cubePrefab = default;
    [SerializeField]
    private GameObject cubeContainerGO = default;
    [SerializeField]
    private List<Transform> listSpawnPoint = default;

    // ---- INTERN ----
    private List<Player> listPlayer = new List<Player>();
    private int spawnIndex = 0;

    public void AddPlayer(Player player)
    {
        listPlayer.Add(player);
    }


    public void StartSpawning()
    {
        if (listPlayer.Count >= listSpawnPoint.Count)
        {
            Debug.LogError("Spawn point list is to small : there is more players than spawn points");
        }

        foreach (Player p in listPlayer)
        {
            Spawn(p);
        }
    }

    public void NotifyDeath(Cube cube)
    {
        MultipleTargetCamera.Instance.RemoveTarget(cube.transform);
        StartCoroutine(PreparePlayerSpawn(cube.player));
    }

    private IEnumerator PreparePlayerSpawn(Player p)
    {
        float time = 0f;
        while(time < timeToSpawn)
        {
            time += Time.deltaTime;
            yield return null;
        }
        Spawn(p);
    }

    private void Spawn(Player p)
    {
        // instantiate a cube
        GameObject cubeCloneGO = (GameObject)Instantiate(cubePrefab, listSpawnPoint[spawnIndex].position, Quaternion.identity, cubeContainerGO.transform);
        spawnIndex = (spawnIndex == listSpawnPoint.Count - 1) ? 0 : spawnIndex + 1;
        
        Cube cube = cubeCloneGO.GetComponent<Cube>();
        // give it a cubeSpawner
        cube.SetCubeSpawner(this);

        // give it the player
        cube.SetPlayer(p);

        // give it a weapon
        cube.SetWeapon(p.cubeWeaponPrefab);

        // give it to the camera
        MultipleTargetCamera.Instance.AddTarget(cube.transform);

        // change its color
        // TODO
    }
}
