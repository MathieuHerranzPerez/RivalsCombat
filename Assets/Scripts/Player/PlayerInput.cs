using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Player player;

    private string horizontalAxis;
    private string verticalAxis;
    public string aBtn { get; private set; }
    public string bBtn { get; private set; }
    public string xBtn { get; private set; }
    private string triggerAxis;
    private int controllerNumber;

    public float Horizontal { get; set; }
    public float Vertical { get; set; }

    public enum Button
    {
        A,
        B,
        X,
    }

    void Awake()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if(controllerNumber > 0)
        {
            Horizontal = Input.GetAxis(horizontalAxis);
            Vertical = Input.GetAxis(verticalAxis);
        }
    }

    public void SetControllerNumber(int number)
    {
        controllerNumber = number;
        horizontalAxis = "J" + controllerNumber + "Horizontal";
        verticalAxis = "J" + controllerNumber + "Vertical";
        aBtn = "J" + controllerNumber + "A";
        bBtn = "J" + controllerNumber + "B";
        xBtn = "J" + controllerNumber + "X";
        triggerAxis = "J" + controllerNumber + "Trigger";
    }

    public bool IsButtonDown (Button button)
    {
        switch(button)
        {
            case Button.A:
                return Input.GetButtonDown(aBtn);
            case Button.B:
                return Input.GetButtonDown(bBtn);
            case Button.X:
                return Input.GetButtonDown(xBtn);
        }
        return false;
    }

    public bool IsButton(Button button)
    {
        switch (button)
        {
            case Button.A:
                return Input.GetButton(aBtn);
            case Button.B:
                return Input.GetButton(bBtn);
            case Button.X:
                return Input.GetButton(xBtn);
        }
        return false;
    }

    public bool IsButtonUp(Button button)
    {
        switch (button)
        {
            case Button.A:
                return Input.GetButtonUp(aBtn);
            case Button.B:
                return Input.GetButtonUp(bBtn);
            case Button.X:
                return Input.GetButtonUp(xBtn);
        }
        return false;
    }
}
