using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class player_controler : MonoBehaviour
{
    public vars var;
    public float gravity = 20.0F;
    public float cameraHeight = 20f;
    public float cameraDistance = 7f;

    public float next;
    public float speed;

    private Vector3 moveDirection = Vector3.zero;
    public GameObject targetObject;
    public GameObject[] sync_obj;
    Plane surfacePlane = new Plane();
    Vector2 direction;
    public float sync_time = 0;

    void Start () {
        Application.targetFrameRate = 59;
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var = new save_load().load();
        speed = var.speed;
    }

    void Update () {
        var = new save_load().load();
        // перемещение перса
         CharacterController controller = GetComponent<CharacterController>();
         if (controller.isGrounded) {
             moveDirection = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
             moveDirection = transform.TransformDirection(moveDirection);
             moveDirection *= speed;
         }
         moveDirection.y -= gravity * Time.deltaTime;
         controller.Move(moveDirection * Time.deltaTime);

        // цель
        targetObject.transform.position = getpoint();
        targetObject.transform.LookAt(new Vector3(transform.position.x, targetObject.transform.position.y, transform.position.z));

        // положение камеры
        Vector3 cameraOffset = Vector3.zero;
        cameraOffset = new Vector3(cameraDistance, cameraHeight, 0);

        // положение перса
        Vector2 playerPosOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 cursorPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 offsetVector = cursorPosition - playerPosOnScreen;

        // именение положеня камеры
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, transform.position + cameraOffset, Time.deltaTime * 7.4f);
        Camera.main.transform.LookAt(transform.position + new Vector3(-offsetVector.y * 2, 0, offsetVector.x * 2));

        if (Input.GetAxis("Mouse ScrollWheel") != 0 && Camera.main.orthographicSize <= 10 && Camera.main.orthographicSize >= 4)
        {
            //cameraHeight += (Input.GetAxis("Mouse ScrollWheel") * 20);
            //cameraDistance += (Input.GetAxis("Mouse ScrollWheel") * 10);
            Camera.main.orthographicSize += Input.GetAxis("Mouse ScrollWheel");
        }
        else if (Camera.main.orthographicSize <= 4)
        {
            Camera.main.orthographicSize = 4;
        }
        else if (Camera.main.orthographicSize >= 10)
        {
            Camera.main.orthographicSize = 10;
        }

        rotation_sync();

        // дещ
        if (Input.GetKey(KeyCode.Space) && Time.time >= next)
        {
            dash();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = var.speed * 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = var.speed;
        }
    }

    void rotation_sync()
    {
        foreach (GameObject item in sync_obj)
        {
            // поворот модели
            //item.transform.LookAt(new Vector3(targetObject.transform.position.x, item.transform.position.y, targetObject.transform.position.z));
            StartCoroutine(RotateTowardsTarget(item));
        }
    }

    IEnumerator RotateTowardsTarget(GameObject obj)
    {
        float duration = sync_time;

        // store the initial and target rotation once
        var startRotation = obj.transform.rotation;
        var targetRotation = Quaternion.LookRotation(targetObject.transform.position - obj.transform.position);

        for (float timePassed = 0.0f; timePassed < duration; timePassed += Time.deltaTime)
        {
            float factor = timePassed / duration;
            // optionally add ease-in and -out
            //factor = Mathf.SmoothStep(0, 1, factor);

            obj.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, factor);
            yield return null;
        }

        // just to be sure to end up with clean values
        obj.transform.rotation = targetRotation;
    }

    void dash()
    {
        next = Time.time + 1f / var.dash_time;
        speed = var.speed * 4;
        Invoke("SpeedStandart", 0.2f);
    }
    void SpeedStandart(){
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = var.speed * 2;
        }
        else{
            speed = var.speed;
        }
    }

    void FixedUpdate()
    {
    }

    Vector3 getpoint()
    {
        surfacePlane.SetNormalAndPosition(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;
        if (surfacePlane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            return hitPoint;
        }
        //No raycast hit, hide the aim target by moving it far away
        return new Vector3(-5000, -5000, -5000);
    }
}
