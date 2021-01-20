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

    #region Private members

    /// <summary>
    /// The current animation state
    /// </summary>
    private string currentState;

    #endregion

    #region Constents

    // Idel animation
    /// <summary>
    /// Player idel up animation name
    /// </summary>
    const string PLAYER_IDEL_UP = "UpIdel";
    /// <summary>
    /// Player idel right animation name
    /// </summary>
    const string PLAYER_IDEL_RIGHT = "RightIdel";
    /// <summary>
    /// Player idel down animation name
    /// </summary>
    const string PLAYER_IDEL_DOWN = "DownIdel";
    /// <summary>
    /// Player idel left animation name
    /// </summary>
    const string PLAYER_IDEL_LEFT = "LeftIdel";

    // Walking animation
    /// <summary>
    /// Player walking up animation name
    /// </summary>
    const string PLAYER_WALK_UP = "Up";
    /// <summary>
    /// Player walking right animation name
    /// </summary>
    const string PLAYER_WALK_RIGHT = "Right";
    /// <summary>
    /// Player walking down animation name
    /// </summary>
    const string PLAYER_WALK_DOWN = "Down";
    /// <summary>
    /// Player walking left animation name
    /// </summary>
    const string PLAYER_WALK_LEFT = "Left";

    #endregion

    #region Unity methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentState = PLAYER_IDEL_DOWN;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        GetDirection();
        UpdateAnimation();
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
        float moveHorizontal = Input.GetAxisRaw("Horizontal"), 
            moveVertical = Input.GetAxisRaw("Vertical");
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
        if (PDirection != Direction.None)
        {
            int hvelocity = 0, vvelocity = 0;
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
            //rigidbody2d.velocity = new Vector2(hvelocity * Speed, vvelocity * Speed);
            rigidbody2d.MovePosition(transform.position + new Vector3(hvelocity, vvelocity) * Speed * Time.deltaTime);
        }
    }

    #endregion

    #region Animation

    /// <summary>
    /// Based on the player behaviour
    /// </summary>
    private void UpdateAnimation()
    {
        switch (PDirection)
        {
            case Direction.Up:
                ChangeAnimationState(PLAYER_WALK_UP);
                break;
            case Direction.Down:
                ChangeAnimationState(PLAYER_WALK_DOWN);
                break;
            case Direction.Right:
                ChangeAnimationState(PLAYER_WALK_RIGHT);
                break;
            case Direction.Left:
                ChangeAnimationState(PLAYER_WALK_LEFT);
                break;
            default:
                switch (currentState)
                {
                    case PLAYER_WALK_UP:
                        ChangeAnimationState(PLAYER_IDEL_UP);
                        break;
                    case PLAYER_WALK_DOWN:
                        ChangeAnimationState(PLAYER_IDEL_DOWN);
                        break;
                    case PLAYER_WALK_RIGHT:
                        ChangeAnimationState(PLAYER_IDEL_RIGHT);
                        break;
                    case PLAYER_WALK_LEFT:
                        ChangeAnimationState(PLAYER_IDEL_LEFT);
                        break;
                }
                break;
        }
    }

    /// <summary>
    /// Change the player animation state
    /// </summary>
    /// <param name="state">The animation state want to play</param>
    private void ChangeAnimationState(string state)
    {
        // Do nothing if the selected state is already playing
        if (state == currentState)
            return;
        // Play animation state
        animator.Play(state);
        // Save current state
        currentState = state;
    }

    #endregion
}
