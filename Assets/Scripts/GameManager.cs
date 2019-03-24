using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private CubeSpawner cubeSpawner = default;

    void Start()
    {
        Debug.Log("Players : " + Players.Instance);
        InitCubeSpawner();
    }

    void Update()
    {
        
    }

    private void InitCubeSpawner()
    {
        foreach(Player player in Players.Instance.GetPlayerList())
        {
            cubeSpawner.AddPlayer(player);
        }

        cubeSpawner.StartSpawning();
    }
}
