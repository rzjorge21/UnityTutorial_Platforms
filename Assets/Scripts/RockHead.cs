using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHead : MonoBehaviour
{
    [Header("OBLIGATORIO")]
    public bool hasMoreThan2Moves;

    [Header("Marcar solo si hasMoreThan2Moves = FALSE")]
    public bool hasHorizontalMove;

    [Header("Marcar solo si hasMoreThan2Moves = TRUE")]
    public bool clockSide;

    public Transform topCheck;
    public Transform bottomCheck;
    public Transform leftCheck;
    public Transform rightCheck;

    private string direction;

    float checkRadius = 0.2f;

    private bool top;
    private bool bottom;
    private bool left;
    private bool right;

    public LayerMask whatIsGround;


    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!hasMoreThan2Moves)
        {
            RockHead2Movements();
        }
        else
        {
            RockHead4Movements();
        }
        

    }

    void RockHead4Movements()
    {
        left = Physics2D.OverlapCircle(leftCheck.position, checkRadius, whatIsGround);
        right = Physics2D.OverlapCircle(rightCheck.position, checkRadius, whatIsGround);
        top = Physics2D.OverlapCircle(topCheck.position, checkRadius, whatIsGround);
        bottom = Physics2D.OverlapCircle(bottomCheck.position, checkRadius, whatIsGround);

        if (direction == "Top" && top)
        {
            anim.SetTrigger("TopHit");
            if (clockSide)
            {
                direction = "Right";
            }
            else
            {
                direction = "Left";
            }
        }
        else if (direction == "Left" && left)
        {
            anim.SetTrigger("LeftHit");
            if (clockSide)
            {
                direction = "Top";
            }
            else
            {
                direction = "Bottom";
            }
        }
        else if (direction == "Right" && right)
        {
            anim.SetTrigger("RightHit");
            if (clockSide)
            {
                direction = "Bottom";
            }
            else
            {
                direction = "Top";
            }
        }
        else if (direction == "Bottom" && bottom)
        {
            anim.SetTrigger("BottomHit");
            if (clockSide)
            {
                direction = "Left";
            }
            else
            {
                direction = "Right";
            }
        }

        if (left && bottom)
        {
            if (clockSide)
            {
                direction = "Top";
            }
            else
            {
                direction = "Right";
            }
        }
        else if (top && left)
        {
            if (clockSide)
            {
                direction = "Right";
            }
            else
            {
                direction = "Bottom";
            }
        }
        else if (top && right)
        {
            if (clockSide)
            {
                direction = "Bottom";
            }
            else
            {
                direction = "Left";
            }
        }
        else if (bottom && right)
        {
            if (clockSide)
            {
                direction = "Left";
            }
            else
            {
                direction = "Top";
            }
        }
    }

    void RockHead2Movements()
    {
        if (hasHorizontalMove)
        {
            left = Physics2D.OverlapCircle(leftCheck.position, checkRadius, whatIsGround);
            right = Physics2D.OverlapCircle(rightCheck.position, checkRadius, whatIsGround);
        }
        else
        {
            top = Physics2D.OverlapCircle(topCheck.position, checkRadius, whatIsGround);
            bottom = Physics2D.OverlapCircle(bottomCheck.position, checkRadius, whatIsGround);
        }

        if (direction == "Top" && top)
        {
            anim.SetTrigger("TopHit");
            direction = "Bottom";
        }
        else if (direction == "Left" && left)
        {
            anim.SetTrigger("LeftHit");
            direction = "Right";
        }
        else if (direction == "Right" && right)
        {
            anim.SetTrigger("RightHit");
            direction = "Left";
        }
        else if (direction == "Bottom" && bottom)
        {
            anim.SetTrigger("BottomHit");
            direction = "Top";
        }

        if(hasHorizontalMove){
            if(left){
                direction = "Right";
            }
            else if(right){
                direction="Left";
            }
        }else {
            if(top){
                direction = "Bottom";
            }else if(bottom){
                direction = "Top";
            }
        }
    }
}
