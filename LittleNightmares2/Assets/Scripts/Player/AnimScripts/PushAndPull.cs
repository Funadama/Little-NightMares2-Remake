using UnityEngine;

public class PushAndPull : MonoBehaviour
{
    private bool CanMove;
    private bool IsHolding;

    private PlayerMovement move;

    public float MoveSpeed;

    public Animator PlayerAnim;
    public Transform TargetS;
    public Transform TargetW;
    public Transform PushObj;
    public Transform Player;


    private void Start()
    {
        CanMove = false;
        IsHolding = false;
        move = GetComponent<PlayerMovement>();

    }

    private void Update()
    {
        if (CanMove)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                IsHolding = true;



                if (IsHolding)
                {
                    PlayerAnim.SetTrigger("Grab");
                    PushObj.GetComponent<Rigidbody>().isKinematic = true;


                    if (Input.GetKey("d"))
                    {
                        MoveForward();
                        //PlayerAnim.SetBool("Push", true);
                        //PlayerAnim.SetTrigger("Push");
                    }

                    if (Input.GetKey("a"))
                    {
                        MoveBackwards();
                        //PlayerAnim.SetBool("Pull", true);
                    }

                }


            }

            if (!(Input.GetKey(KeyCode.Mouse0)))
            {
                LetGo();
                //PlayerAnim.SetBool("Pull", false);
                //PlayerAnim.SetBool("Push", false);
            }

        }
    }

    void LetGo()
    {
        IsHolding = false;
        PlayerAnim.SetTrigger("LetGo");
        Debug.Log("LetGo");
    }

    void MoveForward()
    {
        PushObj.position = Vector3.Lerp(PushObj.position, TargetW.position, MoveSpeed * Time.deltaTime);
        Player.position = Vector3.Lerp(Player.position, TargetW.position, MoveSpeed * Time.deltaTime);

    }

    void MoveBackwards()
    {
        PushObj.position = Vector3.Lerp(PushObj.position, TargetS.position, MoveSpeed * Time.deltaTime);
        Player.position = Vector3.Lerp(Player.position, TargetS.position, MoveSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PushObj")
        {
            CanMove = true;
            PushObj = collision.transform;

        }
    }
    private void OnTriggerExit(Collider collision)
    {
        CanMove = false;
        LetGo();
    }
}
