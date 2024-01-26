using UnityEngine;

/// <summary>
/// Script which detect mouse click and decide who will take input Ball or Camera
/// </summary>
public class InputManager : MonoBehaviour
{
    [SerializeField]
    private float distanceBetweenBallAndMouseClickLimit = 1.5f; //variable to decide who will take input Ball or Camera

    private float distanceBetweenBallAndMouseClick; //variable to track the distance

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameStatus != GameStatus.Playing)
            return; //if gameStatus is not playing, return

        if (Input.GetMouseButtonDown(0)) //if mouse button is clicked and canRotate is false
        {
            GetDistance(); //get the distance between mouseClick point and ball
            if (distanceBetweenBallAndMouseClick <= distanceBetweenBallAndMouseClickLimit)
            {
                BallControl.instance.MouseDownMethod();
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (distanceBetweenBallAndMouseClick <= distanceBetweenBallAndMouseClickLimit)
            {
                BallControl.instance.MouseNormalMethod();
            }
            else
            { //else call camera method
                CameraRotation.instance.RotateCamera(Input.GetAxis("Mouse X"));
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (distanceBetweenBallAndMouseClick <= distanceBetweenBallAndMouseClickLimit)
            {
                BallControl.instance.MouseUpMethod();
            }
        }
    }

    void GetDistance()
    {
        //we create a plane whose mid point is at ball position and whose normal is toward Camera
        var plane = new Plane(
            Camera.main.transform.forward,
            BallControl.instance.transform.position
        );
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition); //create a ray
        float dist; //varibale to get ditance
        if (plane.Raycast(ray, out dist))
        {
            var v3Pos = ray.GetPoint(dist); //get the point at the given distance
            //calculate the distance
            distanceBetweenBallAndMouseClick = Vector3.Distance(
                v3Pos,
                BallControl.instance.transform.position
            );
        }
    }
}
