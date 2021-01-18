using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The direction the player can move to
/// </summary>
public enum Direction { None, Up, Right, Down, Left }

/// <summary>
/// The player scripts: Movement, Attack...
/// NOTE : Maybe this script will be split into multiple scripts if the code is big
/// </summary>
public class Player : MonoBehaviour
{
    #region Public properties

    /// <summary>
    /// The speed the player move with
    /// </summary>
    public float Speed = 1.2f;

    /// <summary>
    /// The direction the player moving to
    /// </summary>
    public Direction PDirection = Direction.Down;

    #endregion

    #region Components

    /// <summary>
    /// Rigid body of the player
    /// </summary>
    private Rigidbody2D rigidbody2d;

    /// <summary>
    /// Player animator
    /// </summary>
    private Animator animator;

    #endregion

    #region Unity methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.speed = 0;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        GetDirection();
        ApplyAnimation();
    }

    /// <summary>
    /// Frame-rate independent MonoBehaviour.FixedUpdate message for physics calculations
    /// </summary>
    void FixedUpdate()
    {
        Movement();
    }

    #endregion

    #region Movement

    /// <summary>
    /// Check which direction the player want to move to, or just stay standing
    /// </summary>
    private void GetDirection()
    {
        // Which direction inputs are active now
        float moveHorizontal = Input.GetAxis("Horizontal"), 
            moveVertical = Input.GetAxis("Vertical");
        PDirection = Direction.None;
        // Check direction
        if (Mathf.Abs(moveHorizontal) - Mathf.Abs(moveVertical) < 0)
        {
            if (moveVertical > 0)
                PDirection = Direction.Up;
            else if (moveVertical < 0)
                PDirection = Direction.Down;
        }
        else
        {
            if (moveHorizontal > 0)
                PDirection = Direction.Right;
            else if (moveHorizontal < 0)
                PDirection = Direction.Left;
        }
    }

    /// <summary>
    /// Move the player to the direction the user-chosen
    /// </summary>
    private void Movement()
    {
        float hvelocity = 0, vvelocity = 0;
        switch (PDirection)
        {
            case Direction.Up:
                vvelocity = 1;
                break;
            case Direction.Down:
                vvelocity = -1;
                break;
            case Direction.Right:
                hvelocity = 1;
                break;
            case Direction.Left:
                hvelocity = -1;
                break;
        }
        rigidbody2d.velocity = new Vector2(hvelocity * Speed, vvelocity * Speed);
    }

    #endregion

    #region Animation

    /// <summary>
    /// Based on the player behaviour
    /// </summary>
    private void ApplyAnimation()
    {
        animator.speed = 1;
        switch (PDirection)
        {
            case Direction.Up:
                animator.SetTrigger("GoUp");
                break;
            case Direction.Down:
                animator.SetTrigger("GoDown");
                break;
            case Direction.Right:
                animator.SetTrigger("GoRight");
                break;
            case Direction.Left:
                animator.SetTrigger("GoLeft");
                break;
            default:
                animator.speed = 0;
                break;
        }
    }

    #endregion
}
