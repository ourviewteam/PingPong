using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraBorders : MonoBehaviour
{
    [SerializeField]
    private List<Text> scoreFields = new List<Text>();
    private static Vector2 size;
    public static Vector2 Size
    {
        get { return size; }
    }

    public void Start()
    {
        Camera cam = Camera.main;
        size = new Vector2(cam.orthographicSize * cam.aspect, cam.orthographicSize);
    }

    public void UpdateScore(int rocket, int points)
    {
        scoreFields[rocket].text = points.ToString();
    }
}
