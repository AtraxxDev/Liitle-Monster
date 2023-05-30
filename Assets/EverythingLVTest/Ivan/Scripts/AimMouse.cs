using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimMouse : MonoBehaviour
{
    public Transform Player;
    public Transform Gun;

    public float Speed = 3f;

    private Vector2 Rotation = new Vector2(0, 0);
    private Vector2 PlayerRot = new Vector2(0, 0);
    private Vector2 GunRot = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotation.x += Input.GetAxis("Mouse Y");
        Rotation.y += Input.GetAxis("Mouse X");

        PlayerRot.y += Input.GetAxis("Mouse X");

        transform.eulerAngles = (Vector2)Rotation * Speed;
        Player.transform.eulerAngles = (Vector2)PlayerRot * Speed;
    }
}
