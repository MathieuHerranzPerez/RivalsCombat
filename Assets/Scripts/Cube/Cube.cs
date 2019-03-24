using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public static List<Cube> listAllCube { get; private set; }

    public PlayerInput input { get; private set; }

    public int playerNumber { get; private set; }
    public Player player { get; private set; }
    public bool isAlive { get; private set; }

    private CubeSpawner cubeSpawner;

    void Awake()
    {
        if (listAllCube == null)
            listAllCube = new List<Cube>();
        listAllCube.Add(this);

        isAlive = true;
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
}
