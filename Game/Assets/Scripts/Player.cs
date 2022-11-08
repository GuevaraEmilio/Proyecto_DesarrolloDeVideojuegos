using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float range = 100f;
    public float health = 100f;
    public float toxRes = 100f;
    private Transform fpsCam;
    public float speed = 2f;
    public Vector2 mouseSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        fpsCam = transform.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
        CameraMovement();
    }

    void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
    }
    void Movement()
    {
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");
        Vector3 inputPlayer = new Vector3(movX, 0, movY);
        transform.Translate(inputPlayer * Time.deltaTime * speed);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position += Vector3.up * 2;
        }
    }

    private void CameraMovement()
    {
        float camX = Input.GetAxis("Mouse X");
        float camY = Input.GetAxis("Mouse Y");
        if(camX != 0)
        {
            transform.Rotate(Vector3.up * camX * mouseSensitivity.x);
        }
        if(camY != 0)
        {
            float angle = (fpsCam.localEulerAngles.x - camY * mouseSensitivity.y + 360) % 360;
            if(angle > 180) { angle -= 360; }
            angle = Mathf.Clamp(angle, -80, 75);
            fpsCam.localEulerAngles = Vector3.right * angle;
            //fpsCam.Rotate(Vector3.left * camY * mouseSensitivity.y);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.gameObject.tag == "MedicKit")
        {
            if(health < 100f)
            {
                Debug.Log("La vida del jugador ha aumentado");
                Destroy(other.transform.gameObject);
            }
            else
            {
                Debug.Log("La vida del jugador ya esta al maximo");
            }
        }
        if(other.transform.gameObject.tag == "PowerUp1")
        {
            if(toxRes < 100f)
            {
                Debug.Log("La resistencia a material toxico ha aumentado");
                Destroy(other.transform.gameObject);
            }
            else
            {
                Debug.Log("La resitencia al material toxico ya esta al maximo");
            }
        }
    }
}
