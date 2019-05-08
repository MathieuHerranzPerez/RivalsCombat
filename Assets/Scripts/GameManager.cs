using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CubeSpawner cubeSpawner = default;

    void Awake()
    {
        Cursor.visible = false;
    }

    void Start()
    {
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
