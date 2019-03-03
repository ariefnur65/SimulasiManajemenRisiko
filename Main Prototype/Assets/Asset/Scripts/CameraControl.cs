using UnityEngine;

public class CameraControl : MonoBehaviour {
    //Singleton
    public static CameraControl instance;

    public Vector2 panLimit;

    public float minY = 20f;
    public float maxY = 120f;
    Vector3 camPositionUpdate;
    public GameObject StartCameraPosition;
    public float speedFast, speedNormal;


    //Rotation
    public float rotY, rotX;
    public float mouseSensitivityX, mouseSensitivityY;
    float forward;
    float backward;

    float vertical, horizontal;
    public float sensitivity;

    private GameManager gameManager;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start(){
        camPositionUpdate = gameObject.transform.position ;
        gameManager = FindObjectOfType<GameManager>();

    }
    public void ResetPosition()
    {
        transform.position = StartCameraPosition.transform.position;

    }
    public float VerticalMovement()
    {

        if (Input.GetKey(KeyCode.W))
        {
            vertical = Mathf.MoveTowards(vertical, 1f, 0.02f * sensitivity);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            vertical = Mathf.MoveTowards(vertical, -1f, 0.02f * sensitivity);
        }
        else
        {
            vertical = Mathf.MoveTowards(vertical, 0f, 0.02f * sensitivity);

        }
        return vertical;
    }



    public float HorizontalMovement()
    {

        if (Input.GetKey(KeyCode.A))
        {
            horizontal = Mathf.MoveTowards(horizontal, -1f, 0.02f * sensitivity);
        }
      
        else if (Input.GetKey(KeyCode.D))
        {
            horizontal = Mathf.MoveTowards(horizontal, 1f, 0.02f * sensitivity);
        }
        else
        {
            horizontal = Mathf.MoveTowards(horizontal, 0f, 0.02f * sensitivity);

        }
        return horizontal;
    }
    //Update is called once per frame
    void Update () {
        //if (gameManager.getStateOfTheGame().Equals(true))
        //{



            #region Trash
            //         //Update Movement
            //forward = VerticalMovement();
            //backward = HorizontalMovement();



            //         //Debug.Log(forward);

            //         if (forward != 0 || backward != 0)
            //         {
            //             float speed = Input.GetKey(KeyCode.LeftShift) ? speedFast : speedNormal;
            //             Vector3 trans = new Vector3(backward * speed * 0.02f, 0.0f, forward * speed * 0.02f);
            //             camPositionUpdate = gameObject.transform.position + (gameObject.transform.localRotation * trans);
            //         }

            //         //Rotation

            //         if (Input.GetMouseButton(2))
            //         {
            //             //Debug.Log("Sebelum" + transform.localEulerAngles);
            //             rotX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivityX;
            //             if (transform.localEulerAngles.x <= 0)
            //             {
            //                 rotY = -transform.localEulerAngles.x + Input.GetAxis("Mouse Y") * mouseSensitivityY;
            //             }
            //             else
            //             {
            //                 rotY += Input.GetAxis("Mouse Y") * mouseSensitivityY;

            //             }

            //             //Debug.Log("Mid" + transform.localEulerAngles);

            //             rotY = Mathf.Clamp(rotY, -89.5f, 89.5f);
            //             //Debug.Log("Mid2" + transform.localEulerAngles);

            //             transform.localEulerAngles = new Vector3(-rotY, rotX, 0.0f);
            //             //Debug.Log(transform.localEulerAngles);
            //         }

            //         /*
            //         if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
            //         { //|| Input.mousePosition.y >= Screen.height - panBorder) { //Koordinat screen di mulai dari pojok kanan bawah mulai dari 0,0 jadi untuk pembatas atas harus di kurangi border sementara pembatas bawah sama dengan border
            //             camPositionUpdate.z += panSpeed * Time.deltaTime;
            //         }
            //         else if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
            //         //|| Input.mousePosition.y <= panBorder)
            //         { //Koordinat screen di mulai dari pojok kanan bawah mulai dari 0,0 jadi untuk  pembatas bawah sama dengan border
            //             camPositionUpdate.z -= panSpeed * Time.deltaTime;
            //         }
            //         else if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
            //         //|| Input.mousePosition.x <= panBorder)
            //         { //Koordinat screen di mulai dari pojok kanan bawah mulai dari 0,0 jadi untuk  pembatas kiri sama dengan border
            //             camPositionUpdate.x -= panSpeed * Time.deltaTime;
            //         }
            //         else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
            //         //|| Input.mousePosition.x >= Screen.width - panBorder)
            //         { //Koordinat screen di mulai dari pojok kanan bawah mulai dari 0,0 jadi untuk  pembatas kanan = lebar layar - border
            //             camPositionUpdate.x += panSpeed * Time.deltaTime;
            //         }
            //         camPositionUpdate.y -= scroll * scrollSpeed * Time.deltaTime;

            //         if (Input.GetMouseButton(2))//JIka mouse di klik tengah
            //         {
            //             Debug.Log("Message");
            //             yaw += speedH * Input.GetAxis("Mouse X");
            //             pitch -= speedV * Input.GetAxis("Mouse Y") ;

            //             transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            //         }  */


            //         //Membatasi pergerakan kamera
            //         camPositionUpdate.x = Mathf.Clamp(camPositionUpdate.x, -panLimit.x, panLimit.x);
            //         camPositionUpdate.y = Mathf.Clamp(camPositionUpdate.y, minY, maxY);
            //         camPositionUpdate.z = Mathf.Clamp(camPositionUpdate.z, -panLimit.y, panLimit.y);


            //         transform.position = camPositionUpdate;
            #endregion
        //}
        //else
        //{

        //}
       
	}

}
