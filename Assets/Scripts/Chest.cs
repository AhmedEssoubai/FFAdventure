using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    #region Public properties

    /// <summary>
    /// Is the chest opened and what inside already been taken
    /// </summary>
    public bool IsOpened = false;

    /// <summary>
    /// The chest sprite when it's closed
    /// </summary>
    public Sprite ClosedSprite;

    /// <summary>
    /// The chest sprite when it's opened
    /// </summary>
    public Sprite OpenedSprite;

    #endregion

    #region Components

    /// <summary>
    /// Chest sprite
    /// </summary>
    private SpriteRenderer sprite;

    /// <summary>
    /// Chest collider
    /// </summary>
    private CircleCollider2D collider2d;

    #endregion

    #region Unity methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<CircleCollider2D>();
        if (IsOpened)
        {
            sprite.sprite = OpenedSprite;
            collider2d.enabled = false;
        }
        else
            sprite.sprite = ClosedSprite;
    }

    /// <summary>
    /// When the player inspect the chest
    /// </summary>
    /// <param name="collision">The player collision (Maybe)</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !IsOpened)
        {
            IsOpened = true;
            FindObjectOfType<StorageSystem>().AddKey();
            sprite.sprite = OpenedSprite;
            collider2d.enabled = false;
        }
    }

    #endregion
}
