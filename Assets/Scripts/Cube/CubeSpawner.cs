using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject cubePrefab = default;
    [SerializeField]
    private GameObject cubeContainerGO = default;
    [SerializeField]
    private List<Transform> listSpawnPoint = default;

    // ---- INTERN ----
    private List<Player> listPlayer = new List<Player>();

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

        int i = 0;
        foreach (Player p in listPlayer)
        {
            // instantiate a cube
            GameObject cubeCloneGO = (GameObject)Instantiate(cubePrefab, listSpawnPoint[i].position, Quaternion.identity, cubeContainerGO.transform);
            Cube cube = cubeCloneGO.GetComponent<Cube>();
            ++i;
            // give it a cubeSpawner
            cube.SetCubeSpawner(this);

            // give it the player
            cube.SetPlayer(p);

            // change its color
            // TODO
        }
    }


    //private Transform GetSpawnPoint()
    //{

    //}
}
