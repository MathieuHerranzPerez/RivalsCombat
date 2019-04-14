using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubeController))]
[RequireComponent(typeof(Destructable))]
public class Cube : MonoBehaviour
{
    public static List<Cube> listAllCube { get; private set; }

    public PlayerInput input { get; private set; }
    public int playerNumber { get; private set; }
    public Player player { get; private set; }

    [SerializeField]
    private int maxLifePoint = 100;

    [SerializeField]
    private MeshRenderer cubeGFX = default;

    // ---- INTERN ----
    private int lifePoint;
    private CubeSpawner cubeSpawner;
    private CubeController cubeController;
    private Destructable destructable;

    void Awake()
    {
        if (listAllCube == null)
            listAllCube = new List<Cube>();
        listAllCube.Add(this);
    }

    void Start()
    {
        lifePoint = maxLifePoint;
        cubeController = GetComponent<CubeController>();
        destructable = GetComponent<Destructable>();
        cubeGFX.material.color = player.color;
    }

    void OnDestroy()
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

    public CubeController GetCubeController()
    {
        return this.cubeController;
    }

    public void TakeDamageFormBullet(float dmgAmount)
    {
        if (lifePoint > 0)
        {
            lifePoint -= (int)dmgAmount;
            if (lifePoint <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        cubeSpawner.NotifyDeath(this);
        // TODO animation
        destructable.Destroy(player.color);
    }
}
