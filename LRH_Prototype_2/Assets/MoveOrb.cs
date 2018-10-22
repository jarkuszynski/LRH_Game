using System.Collections;
using System.Security.Policy;
using UnityEngine;

public class MoveOrb : MonoBehaviour
{

    private int JUMP_VEL = 5;

    public KeyCode moveL;
    public KeyCode moveR;
    public KeyCode jump;
    public KeyCode moveUp;
    public KeyCode moveDown;
    public float horizVel = 0;
    public float vertVel = 0;
    public float zVel = 0;
    public int laneNum = 2;
    public bool controlLocked = false;

    private Coroutine slidingCoroutine;
    private Coroutine zCoroutine;
    public bool canJump;

    // Use this for initialization
    private void Start()
    {

    }

    void FixedUpdate()
    {

    }
    // Update is called once per frame
    private void Update()
    {
        var orb = GetComponent<Rigidbody>();
        orb.velocity = new Vector3(horizVel, vertVel, 5 + zVel);
        
        Physics.gravity = new Vector3(0f, -120.0F, 0f);
        if ((Input.GetKeyDown(moveL))) //&& (laneNum > 0) && (!controlLocked))
        {
            handleSideMovement(moveL);
        }

        if ((Input.GetKeyDown(moveR))) // && (laneNum < 4) && (!controlLocked))
        {
            handleSideMovement(moveR);
        }

        if (Input.GetKeyDown(moveUp))
        {
            handleZMovement(moveUp);
        }

        if (Input.GetKeyDown(moveDown))
        {
            handleZMovement(moveDown);
        }

        if (Input.GetKeyDown(jump) && canJump)
        {
            canJump = false;
            vertVel = JUMP_VEL;
            StartCoroutine(stopJump());
        }
        

        if (GetComponent<Transform>().position.y < 0)
        {
            Destroy(GetComponent<Rigidbody>().gameObject);
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }

    //private void OnCollisionExit(Collision other)
    //{
    //    if (other.gameObject.tag != "Ground")
    //    {
    //        canJump = false;
    //    }
    //}

    private void handleSideMovement(KeyCode moveButton)
    {
        horizVel = moveButton == moveR ? 2 : -2;
        if (slidingCoroutine != null) StopCoroutine(slidingCoroutine);
        slidingCoroutine = StartCoroutine(stopSlide(moveButton));
    }

    private void handleZMovement(KeyCode moveButton)
    {
        zVel = moveButton == moveUp ? 2 : -2;
        if (zCoroutine != null) StopCoroutine(zCoroutine);
        zCoroutine = StartCoroutine(stopZMovement(moveButton));
    }

    private IEnumerator stopSlide(KeyCode moveButton)
    {
        yield return new WaitWhile(() => Input.GetKey(moveButton));
        horizVel = 0;
        controlLocked = false;
    }

    private IEnumerator stopZMovement(KeyCode moveButton)
    {
        yield return new WaitWhile(() => Input.GetKey(moveButton));
        zVel = 0;
        controlLocked = false;
    }

    private IEnumerator stopJump()
    {
        yield return new WaitForSeconds(.3f);
        //vertVel = -JUMP_VEL;
        vertVel = 0;
    }
}