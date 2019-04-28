using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RocketMovement : NetworkBehaviour {

    [SerializeField]
    private float speed = 0.5f;
    private Vector2 StartTouchPos, EndTouchPos;
    private CameraBorders cam;
    public MovementState state = MovementState.stop;

    [SyncVar]
    private int playerNumber = 0;

    [SyncVar]
    private int points;
    public int Points
    {
        get { return points; }
        set
        {
            if (value > 0 && points != value)
                points = value;
        }
    }

    public enum MovementState
    {
        up,
        down,
        stop
    }

    private void Awake()
    {
        cam = Camera.main.GetComponent<CameraBorders>();
    }
    private void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if ((Input.touchCount > 0) && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            StartTouchPos = Input.GetTouch(0).position;
            Debug.Log(StartTouchPos);
        }
        if ((Input.touchCount > 0) && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            EndTouchPos = Input.GetTouch(0).position;
            if (EndTouchPos.y < StartTouchPos.y)
            {
                state = MovementState.down;
            }
            if (EndTouchPos.y > StartTouchPos.y)
            {
                state = MovementState.up;
            }
        }

        if (state == MovementState.down && (transform.position.y > -CameraBorders.Size.y + transform.localScale.y / 2))
        {
            transform.Translate(0, -speed, 0);
        }
        else if (state == MovementState.up && (transform.position.y < CameraBorders.Size.y - transform.localScale.y / 2))
        {
            transform.Translate(0, speed, 0);
        }

    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<SpriteRenderer>().color = Color.blue;
        this.tag = "serverPlayer";
        if (isServer)
            playerNumber = 1;
    }
}
