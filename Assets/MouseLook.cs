using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    public float rotationY = 0F;
    public float rotationX = 0F;

    Quaternion targetAngle;

    void Update ()
    {

        /*if ((!World.playerCanMove && !World.playerForceWatch) || !World.playerController.alive || World.playerExaminingItem)
        {
            rotationY = Mathf.DeltaAngle(transform.eulerAngles.x, 0);
            return;
        }*/

        if (axes == RotationAxes.MouseXAndY)
        {
            rotationX = transform.eulerAngles.y + Input.GetAxis ("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

            targetAngle = Quaternion.Euler (new Vector3 (-rotationY, rotationX, 0));
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate (0, Input.GetAxis ("Mouse X") * sensitivityX, 0);
        }
        else
        {
            rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

            transform.eulerAngles = new Vector3 (-rotationY, transform.eulerAngles.y, 0);
        }

        transform.rotation = Quaternion.Slerp (transform.rotation, targetAngle, 0.4f);
    }

    void Start ()
    {
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody> ())
            GetComponent<Rigidbody> ().freezeRotation = true;
    }
}