using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubeController))]
public class Cube : MonoBehaviour
{
    public static List<Cube> listAllCube { get; private set; }

    public PlayerInput input { get; private set; }

    public int playerNumber { get; private set; }
    public Player player { get; private set; }
    public bool isAlive { get; private set; }

    // ---- INTERN ----
    private CubeSpawner cubeSpawner;
    private CubeController cubeController;

    void Awake()
    {
        if (listAllCube == null)
            listAllCube = new List<Cube>();
        listAllCube.Add(this);

        isAlive = true;
    }

    void Start()
    {
        cubeController = GetComponent<CubeController>();
    }

    void OnDisable()
    {
        listAllCube.Remove(this);
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
        playerNumber = player.playerNumber;
        input = player.GetPlayerInput();
    }

    public void SetCubeSpawner(CubeSpawner cubeSpawner)
    {
        this.cubeSpawner = cubeSpawner;
    }


    public void TakeDamageFormBullet(float dmgAmount, Bullet bullet)
    {
        
    }

    public CubeController GetCubeController()
    {
        return this.cubeController;
    }
}
