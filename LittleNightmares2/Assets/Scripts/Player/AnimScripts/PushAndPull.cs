using UnityEngine;

public class PushAndPull : MonoBehaviour
{
    private bool AnimationPull;
    private bool AnimationPush;
    private bool CanMove;
    private PlayerMovement move;
    private float currentAngle;
    private float RotationSpeed;

    public float MoveSpeed;
    public Animator PlayerAnim;
    public Transform TargetS;
    public Transform TargetW;
    public Transform PushObj;
    public Transform Player;

    public bool IsHolding;


    private void Start()
    {
        CanMove = false;
        IsHolding = false;
        move = GetComponent<PlayerMovement>();

    }

    private void Update()
    {
        if (CanMove && Input.GetKey(KeyCode.Mouse0))
        {

            IsHolding = true;

            PlayerAnim.SetTrigger("Grab");
            PushObj.GetComponent<Rigidbody>().isKinematic = true;


            if (Input.GetKey("d"))
            {
                Vector3 Dir = (Player.position - TargetW.position).normalized;
                PushObj.position = Vector3.Lerp(PushObj.position, TargetW.position, MoveSpeed * Time.deltaTime);
                Player.position = Vector3.Lerp(Player.position, TargetW.position + Dir * 2.5f, MoveSpeed * Time.deltaTime);
                if (!(AnimationPull))
                {
                    AnimationPull = true;
                    AnimationPush = false;
                    PlayerAnim.SetTrigger("IsPushing");
                    Debug.Log("test1");
                }
            }

            if (Input.GetKey("a"))
            {
                Vector3 Dir = (Player.position - TargetS.position).normalized;

                PushObj.position = Vector3.Lerp(PushObj.position, TargetS.position, MoveSpeed * Time.deltaTime);
                Player.position = Vector3.Lerp(Player.position, TargetS.position + Dir * 2.5f, MoveSpeed * Time.deltaTime);
                if (!(AnimationPush))
                {
                    AnimationPush = true;
                    AnimationPull = false;
                    PlayerAnim.SetTrigger("IsPulling");
                    Debug.Log("test2");
                }
            }




        }
        else if (IsHolding)
        {
            IsHolding = false;
            AnimationPush = false;
            AnimationPull = false;
            PlayerAnim.SetTrigger("LetGo");
        }


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
        if (collision.gameObject.tag == "PushObj")
        {
            CanMove = false;
            IsHolding = false;
            PlayerAnim.SetTrigger("LetGo");
        }
    }
}
