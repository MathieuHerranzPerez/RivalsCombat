using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [SerializeField]
    public int playerNumber { get; private set; }
    [SerializeField]
    private Color color;

    private PlayerInput input;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    public void SetControllerNumber(int controller)
    {
        input.SetControllerNumber(controller);
    }

    public PlayerInput GetPlayerInput()
    {
        return this.input;
    }
}
