using UnityEngine;

/// <summary>
/// A door that blocks the player way
/// </summary>
public class Door : MonoBehaviour
{
    #region Components

    /// <summary>
    /// The storage system of the game
    /// </summary>
    private StorageSystem storage;

    #endregion

    #region Unity methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        storage = FindObjectOfType<StorageSystem>();
    }

    /// <summary>
    /// When the player get near the door
    /// </summary>
    /// <param name="collision">The player collision (Maybe)</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (storage.HasKey())
            {
                storage.UseKey();
                Destroy(gameObject);
            }
        }
    }

    #endregion
}
