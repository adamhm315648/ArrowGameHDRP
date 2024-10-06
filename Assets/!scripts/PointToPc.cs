using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToPc : MonoBehaviour
{

    public bool enteredPlayState;
    public GameObject monitor;
    public Camera cam;
    public GameObject camParent;

    public Vector3 camRotation;
    public Vector3 camParentRotation;
    public GameObject CanvasToDisable;

    // Define the event
    public delegate void DisplayTextDelegate(string message);
    public static event DisplayTextDelegate OnDisplayTextEvent;


    public delegate void DisableTextDelegate();
    public static event DisableTextDelegate OnDisableTextEvent;



    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        if (!enteredPlayState)
        {
            CheckRays();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            enteredPlayState = false;
            ExitPlayerState();
        }

    }

    void CheckRays()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);


            if (hit.transform.gameObject == monitor)
            {
                // Trigger the event when hitting the monitor
                OnDisplayTextEvent?.Invoke("Press E to Play");
                CheckInput();
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            OnDisableTextEvent?.Invoke();
        }
    }


    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            enteredPlayState = true;
            EnterPlayerState();
        }
    }

    void ExitPlayerState()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cam.fieldOfView = 65;
        camParent.GetComponent<PlayerMovement>().canLook = true;
        CanvasToDisable.SetActive(true);
    }
    void EnterPlayerState()
    {
        // camera object new rotation: 4.3, 0, 0
        // camera object change fov: 40
        // cam parent new rotation: 0, 35.9, 0
        CanvasToDisable.SetActive(false);
        //Vector3 camParentRot = new Vector3(0,36.2000198f,0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        camParent.GetComponent<PlayerMovement>().canLook = false;
        //camParent.transform.rotation = Quaternion.Euler(camParentRot);
        OnDisableTextEvent?.Invoke();
        cam.fieldOfView = 40;


    }
}
