using UnityEngine;

public class PickUp : MonoBehaviour
{
    private bool CanPickUp;
    private bool IsHolding;
    private float MoveSpeed = 5;

    public Transform Target;
    public Transform PlayerHand;
    public Transform PickupObject;
    public Animator PlayerAnim;

    public bool IsMoving;
    public bool IsPickup;


    private void Start()
    {
        IsMoving = false;
        CanPickUp = false;

    }

    private void Update()
    {

        if (CanPickUp == true)
        {


            if (Input.GetKey(KeyCode.Mouse0) && !(IsHolding))
            {
                IsHolding = true;
                IsPickup = true;
                PlayerAnim.SetTrigger("GrabItem");
                PickupObject.GetComponent<Collider>().enabled = false;
                PickupObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        if (IsPickup)
        {
            PickupObject.position = Vector3.Lerp(PickupObject.position, Target.position, MoveSpeed * Time.deltaTime);
            PickupObject.rotation = Quaternion.RotateTowards(transform.rotation, PlayerHand.rotation, MoveSpeed * Time.deltaTime);
        }
        else if (IsMoving)
        {
            PickupObject.position = Target.position;
            PickupObject.rotation = Quaternion.RotateTowards(transform.rotation, PlayerHand.rotation, MoveSpeed * Time.deltaTime);
        }

        if (!(Input.GetKey(KeyCode.Mouse0)) && IsHolding)
        {
            PutDown();
            IsPickup = false;
            PickupObject.GetComponent<Rigidbody>().isKinematic = false;
            PickupObject.GetComponent<Collider>().enabled = true;
            PlayerAnim.SetTrigger("SetDown");
            IsHolding = false;
        }


    }

    public void MoveObj()
    {
        if (IsHolding)
        {
            IsMoving = true;
            IsPickup = false;
        }
    }

    public void PutDown()
    {
        IsMoving = false;

    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PickUpObject")
        {
            CanPickUp = true;
            PickupObject = collision.transform;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        CanPickUp = false;
    }

}
