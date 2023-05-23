using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraControl : MonoBehaviour
{
    public bool activeTP;

    public Transform posTP;
    public Transform posPP;

    public float rotSpeed;
    public float rotMin, rotMax;
    float mouseX, mouseY;
    public Transform target, player;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Cam()
    {
        //Obtenemos el valor del eje x y del eje y del mouse
        mouseX += rotSpeed * Input.GetAxis("Mouse X");
        mouseY -= rotSpeed * Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, rotMin, rotMax);

        
        target.rotation = Quaternion.Euler(mouseY, mouseX, 0.0f);
        player.rotation = Quaternion.Euler(0.0f, mouseX, 0.0f);
    }

    private void LateUpdate()
    {
        Cam();

        if(activeTP==false&&Input.GetKeyDown(KeyCode.Tab))
        {
            activeTP = true;
            transform.position = posPP.position;
        }
        else if(activeTP==true&&Input.GetKeyDown(KeyCode.Tab))
        {
            activeTP = false;
            transform.position = posTP.position;
            transform.LookAt(player);
        }
    }
    
}
