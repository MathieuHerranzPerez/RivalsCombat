using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    public int playerNumber { get { return num; } private set { num = value; } }
    [SerializeField]
    private int num = 0;
    public bool isPlayed { get; private set; }

    public Color color { get; private set; }

    private PlayerInput input;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    void Start()
    {
        isPlayed = false;
    }

    public void SetControllerNumber(int controller)
    {
        // this.num = controller;
        input.SetControllerNumber(controller);
        isPlayed = true;
    }

    public PlayerInput GetPlayerInput()
    {
        return this.input;
    }

    public void SetColor(Color color)
    {
        this.color = color;
    }
}
