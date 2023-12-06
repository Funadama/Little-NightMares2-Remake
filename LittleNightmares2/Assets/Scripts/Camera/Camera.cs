using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(transform.position, Player.transform.position));
        if (Vector3.Distance(transform.forward, Vector3.Normalize(Player.transform.position - transform.position)) > 0.1f)
        {
            transform.forward = Vector3.MoveTowards(transform.forward, Player.transform.position - transform.position, 0.1f);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, Mathf.Min(9, Player.transform.position.y + 7), transform.position.z), 0.1f);
        }

        if (Vector3.Distance(transform.position, new Vector3(transform.position.x, transform.position.y, Player.transform.position.z - 18)) > 3)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, Mathf.Max(-17, Player.transform.position.z - 18)), 0.1f);

        }
    }
}