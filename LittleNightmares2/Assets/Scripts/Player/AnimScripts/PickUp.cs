using UnityEngine;

public class PickUp : MonoBehaviour
{
    private bool CanPickUp;
    private bool IsMoving;
    public float MoveSpeed = 5;
    public Transform Target;
    public Transform PlayerHand;
    public Transform PickupObject;
    public Animator PlayerAnim;


    private void Start()
    {
        IsMoving = false;
        CanPickUp = false;

    }

    private void Update()
    {

        if (CanPickUp == true)
        {


            if (Input.GetKeyDown("p"))
            {
                PlayerAnim.SetTrigger("GrabItem");
                PickupObject.GetComponent<Collider>().enabled = false;
                PickupObject.GetComponent<Rigidbody>().isKinematic = true;


            }


        }

        if (IsMoving)
        {
            PickupObject.position = Vector3.Lerp(PickupObject.position, Target.position, MoveSpeed * Time.deltaTime);
            PickupObject.rotation = Quaternion.RotateTowards(transform.rotation, PlayerHand.rotation, MoveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown("o"))
        {
            PutDown();
            PickupObject.GetComponent<Rigidbody>().isKinematic = false;
            PickupObject.GetComponent<Collider>().enabled = true;
            PlayerAnim.SetTrigger("SetDown");



        }


    }

    public void MoveObj()
    {
        IsMoving = true;

    }

    public void PutDown()
    {
        IsMoving = false;

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PickUpObject")
        {
            CanPickUp = true;
            PickupObject = collision.transform;
            Debug.Log("Coll");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        CanPickUp = false;
    }

}
