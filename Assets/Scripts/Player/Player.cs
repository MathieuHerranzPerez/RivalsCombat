using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [SerializeField]
    public int playerNumber { get; private set; }
    [SerializeField]
    private Color color;

    public PlayerInput input { get; private set; }

    void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    public PlayerInput GetPlayerInput()
    {
        return this.input;
    }
}
