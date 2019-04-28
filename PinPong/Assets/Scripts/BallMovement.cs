using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class BallMovement : NetworkBehaviour {

    [SerializeField]
    private Ball[] ballSettings = new Ball[3];

    public RocketMovement[] players = new RocketMovement[2];
    private Vector2 direction;
    private float speed = 0.2f;
    private CameraBorders cam;

    public void Start()
    {
        cam = Camera.main.GetComponent<CameraBorders>();
        RespawnBall();
    }

    private void RespawnBall()
    {
        players[0] = GameObject.FindGameObjectWithTag("serverPlayer").GetComponent<RocketMovement>();
        players[1] = GameObject.FindGameObjectWithTag("clientPlayer").GetComponent<RocketMovement>();

            if (transform.position.x > CameraBorders.Size.x)
            {
                players[1].Points++;
                cam.UpdateScore(1, players[1].Points);
            }
            else if (transform.position.x < -CameraBorders.Size.x)
            {
                players[0].Points++;
                cam.UpdateScore(0, players[0].Points);
            }

            int iter = (int)Random.Range(0, ballSettings.Length);
            GetComponent<SpriteRenderer>().color = ballSettings[iter].color;
            GetComponent<TrailRenderer>().startColor = ballSettings[iter].color;
            speed = ballSettings[iter].speed;

        transform.position = Vector3.zero;
        float x = Random.Range(0.3f, 0.7f);
        float y = Mathf.Sqrt(1 - x * x);
        direction = new Vector2(x, y);
    }

    public void FixedUpdate()
    {
        transform.Translate(direction * speed);
        if (Mathf.Abs(transform.position.x) - transform.localScale.x / 2 > CameraBorders.Size.x)
            RespawnBall();
        if (Mathf.Abs(transform.position.y) + transform.localScale.y / 2 > CameraBorders.Size.y)
            direction = new Vector2(direction.x, -direction.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        direction = new Vector2(-direction.x, direction.y);
    }

}
