using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float runSpeed = 4;
    public float walkSpeed = 2;
    public float crouchSpeed = 2f;
    public float bobbingAmount = 0.5f;
    public float bobbingSpeed = 1f;

    float bobbingSpeedMultiplyer = 1;
    float speedMultiplyer = 1;

    public ParticleSystem ps;
    //sounds
    [SerializeField]
    AudioClip[] soundSteps;
    AudioSource sndSourceSteps;
    bool soundStepsCanPlay;

    public Transform cam;
    private Rigidbody rb;
    private CapsuleCollider cc;
    public float weightAmmo = 0f;
    public float fireAmmo = 0f;

    Vector3 camLocalStart;
    Vector3 directionVector;
    float bobbing = 0;
    float breathing = 0;
    bool crouch = false;
    float camCrouch = 0;
    private bool jump;

    public GameObject gun;

    void Awake()
    {
        cam = GetComponentInChildren<Camera>().transform;
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        //sounds
        sndSourceSteps = gameObject.AddComponent<AudioSource>();
        sndSourceSteps.clip = soundSteps[0];
        sndSourceSteps.volume = 0.3f;


        camLocalStart = cam.localPosition;

        speed = walkSpeed;
    }

    void Start()
    {

    }


    void Update()
    {

        //rotate body
        //Vector3 ang = body.localEulerAngles;
        //Quaternion handTargetRot = Quaternion.Euler (new Vector3 (ang.x, cam.transform.eulerAngles.y, ang.z));
        //body.rotation = Quaternion.Slerp (body.rotation, handTargetRot, 0.5f);


        directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        float bobbingY = 0;
        if (directionVector != Vector3.zero)
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = Mathf.Lerp(speed, runSpeed, 0.1f);
            }
            else
            {
                speed = Mathf.Lerp(speed, walkSpeed, 0.4f);
            }
            if (crouch)
            {
                if (speed > crouchSpeed) speed = crouchSpeed;
            }
            Vector3 camDirection = cam.forward;
            camDirection.y = 0;

            Transform camTransform = cam;
            float euler = camTransform.localEulerAngles.x;
            camTransform.Rotate(new Vector3(-euler, 0, 0));
            directionVector = camTransform.rotation * directionVector;
            camTransform.Rotate(new Vector3(euler, 0, 0));

            //if (!controller.isGrounded)
            //{
            //    directionVector.y = -1.0f;
            //}

            //controller.Move (directionVector.normalized * speed * Time.deltaTime); old but gold

            if (Vector3.Angle(directionVector, rb.velocity) > 5)
            {
                speedMultiplyer = 2f;
                bobbingSpeedMultiplyer = 4;
            }
            else
            {
                speedMultiplyer = 1;
                bobbingSpeedMultiplyer = 1;
            }

            float rbSpeed = rb.velocity.magnitude / speedMultiplyer;
            float ratio = (rbSpeed - walkSpeed) / (runSpeed - walkSpeed) + 0.1f;

            bobbingY = (Mathf.Cos(bobbing * Mathf.PI / 180f) * bobbingAmount) / (7f - ratio);
            bobbing += (rbSpeed * (5f - ratio)) * bobbingSpeed * Time.deltaTime * bobbingSpeedMultiplyer;
            if (bobbingY < 0)
            {
                if (soundStepsCanPlay)
                {
                    if (soundSteps.Length > 0)
                        sndSourceSteps.clip = soundSteps[Random.Range(0, soundSteps.Length)];
                    else
                        sndSourceSteps.clip = soundSteps[Random.Range(0, soundSteps.Length)];

                    sndSourceSteps.volume = (speed / runSpeed) * (speed / runSpeed) * 0.3f;
                    sndSourceSteps.pitch = 0.95f + 0.1f * Random.value;
                    sndSourceSteps.Play();
                    soundStepsCanPlay = false;
                }
            }
            else
            {
                soundStepsCanPlay = true;
            }
        }
        else
        {
            speed = 0;
        }


        if (Input.GetKey(KeyCode.LeftControl))
        {
            crouch = true;
        }
        else if (crouch == true)
        {
            //if (!Physics.Raycast (transform.position + new Vector3 (0f, 0.2f, 0f), transform.up, 1.4f, Layers.MASK_DEFAULT))
            {
                crouch = false;
            }
        }

        if (crouch)
        {
            camCrouch = Mathf.Lerp(camCrouch, -0.9f, 0.2f);
            cc.height = 0.6f;
            cc.center = new Vector3(0, 0.38f, 0);
        }
        else
        {
            camCrouch = Mathf.Lerp(camCrouch, 0, 0.2f);
            cc.height = 1.7f;
            cc.center = new Vector3(0, 0.85f, 0);
        }

        breathing += (-30.0f + Random.value * 150.0f) * Time.deltaTime;
        float breathingY = 0.02f * Mathf.Cos(breathing * Mathf.PI / 180f);

        cam.localPosition = new Vector3(camLocalStart.x, camLocalStart.y + bobbingY + camCrouch + breathingY, camLocalStart.z);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            /*if (Physics.Raycast (transform.position, Vector3.down, 0.5f,Layers.Negate (Layers.MASK_PLAYER)))
            {
                jump = true;
            }*/
            jump = true;
        }

        //leva peso
        RaycastHit hitInfo;
        if (Input.GetMouseButton(0) && Physics.Raycast(cam.position, cam.transform.forward, out hitInfo, 5f))
        {
            string tag = hitInfo.collider.tag;
            if (tag == "crate")
            {
                if (hitInfo.transform.position.y < 4.5f)
                {
                    hitInfo.transform.Translate(Vector3.up * Time.deltaTime * 2f);
                    weightAmmo += 0.01f;
                    SetGunParticles(hitInfo.point, gun.transform.position);
                }
            }
            else if (tag == "fire" && !hitInfo.transform.GetComponent<Fire>().dead)
            {
                hitInfo.transform.GetComponent<Fire>().DecreaseFire();
                fireAmmo += 0.5f;
                SetGunParticles(hitInfo.point, gun.transform.position);
            }
            else if (tag == "cisterna" && fireAmmo > 0)
            {
                hitInfo.transform.GetComponent<Cisterna>().IncreaseEmission();
                fireAmmo -= 0.5f;
                SetGunParticles(gun.transform.position, hitInfo.point);
            }
            else
            {
                ps.gameObject.SetActive(false);
            }
        }
        //aggiungi peso 
        else if (Input.GetMouseButton(1) && Physics.Raycast(cam.position, cam.transform.forward, out hitInfo, 5f))
        {
            string tag = hitInfo.collider.tag;
            if (tag == "crate")
            {
                if (hitInfo.transform.position.y > 0.5f && weightAmmo > 0)
                {
                    hitInfo.transform.Translate(Vector3.down * Time.deltaTime * 2f);
                    weightAmmo -= 0.01f;
                    SetGunParticles(gun.transform.position, hitInfo.point);
                }
            }
        }
        else
        {
            ps.gameObject.SetActive(false);
        }

    }

    void SetGunParticles(Vector3 startPos, Vector3 endPos)
    {
        ps.gameObject.SetActive(true);
        ps.transform.position = startPos;
        ps.transform.forward = (endPos - startPos).normalized;
    }

    void FixedUpdate()
    {
        MovePhysics(directionVector.normalized);
        if (jump)
        {
            jump = false;
            rb.AddForce(Vector3.up * 5.5f, ForceMode.VelocityChange);
        }
    }

    void MovePhysics(Vector3 direction)
    {
        Vector3 targetVelocity = direction * (speed / speedMultiplyer);
        targetVelocity.y = rb.velocity.y;

        Vector3 velocityChange = (targetVelocity - rb.velocity);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }



}