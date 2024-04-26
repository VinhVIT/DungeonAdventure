using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected Vector3 originalSize;
    protected BoxCollider2D boxcol;
    protected Vector3 movedelta;
    protected RaycastHit2D hit;
    protected Vector2 move;
    protected float xSpeed = 0.5f;
    protected float ySpeed = 0.35f;

    protected virtual void Start()
    {
        boxcol = GetComponent<BoxCollider2D>();
        originalSize = transform.localScale;
    }
    protected virtual void UpdateMotor(Vector3 input)
    {
        //set speed for player
        if (gameObject.tag == "Player")
        {
            movedelta = new Vector3(input.x * xSpeed * 1.5f, input.y * ySpeed * 1.5f, 0);

            Vector3 mousePosition = Input.mousePosition;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            if (mouseWorldPosition.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            movedelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

            if (movedelta.x > 0)
            {
                transform.localScale = originalSize;
            }
            else if (movedelta.x < 0)
            {
                transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);//if press "A" or "left" player flip
            }
        }
        movedelta += pushDirection;//Add pushing
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);//reduct pusforce
        hit = Physics2D.BoxCast(transform.position, boxcol.size, 0, new Vector2(0, movedelta.y), Mathf.Abs(movedelta.y * Time.deltaTime), LayerMask.GetMask("Player", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, movedelta.y * Time.deltaTime, 0);//make player move up down
        }
        hit = Physics2D.BoxCast(transform.position, boxcol.size, 0, new Vector2(movedelta.x, 0), Mathf.Abs(movedelta.x * Time.deltaTime), LayerMask.GetMask("Player", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(movedelta.x * Time.deltaTime, 0, 0);//make player move left right
        }
    }
}
