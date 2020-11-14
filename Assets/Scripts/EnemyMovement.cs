using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed;
    private Rigidbody rb;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPlayerPos = player.position + new Vector3(0,20,0);
        
        Vector3 direction = player.position - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = Quaternion.Euler(0, 0, 0);
        direction.Normalize();
        movement = direction;
    }
    private void FixedUpdate()
    {
        moveCharacter(movement);
    }
    void moveCharacter(Vector3 direction)
    {
        rb.MovePosition((Vector3)transform.position + (direction * moveSpeed * Time.deltaTime));
        //rb.MovePosition((Vector3)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
    private float OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //moveSpeed = 10;
            
            return moveSpeed;
        }
        return moveSpeed;
    }
}