using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    public GameObject cubeWeWantToMove;
    public 

    RaycastHit hitInfo;
    Ray ray;

    private Camera camera;
    private Vector3 newPosition;

    private void Start()
    {
        camera = Camera.main;
    }

    public void MoveCubeTowardsPosition(Vector2 desiredPosition)
    {
        ShootArrayFromCamera();

    }

    private void ShootArrayFromCamera()
    {
        ray = camera.ScreenPointToRay(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y ,camera.transform.position.z));
    }

    private void TryGettingNewPosition()
    {
        if(Physics.Raycast(ray, out hitInfo))
        {
            newPosition = hitInfo.transform.position;
            Debug.Log(hitInfo.collider.gameObject);
        }
    }

    private void MoveCubeTowardsNewPosition()
    {
        //lerp
        cubeWeWantToMove.transform.position = newPosition;
    }

}
