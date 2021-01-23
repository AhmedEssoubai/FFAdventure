using UnityEngine;

public class StorageSystem : MonoBehaviour
{
    #region Private members

    /// <summary>
    /// The number of keys in the player inventory
    /// </summary>
    private uint keys = 0;

    #endregion

    #region Key helpers

    /// <summary>
    /// Check if the player has a key in his inventory
    /// </summary>
    /// <returns>Is the player has a key</returns>
    public bool HasKey()
    {
        return keys > 0;
    }

    /// <summary>
    /// Use a key from the player inventory
    /// </summary>
    /// <returns>Is the a key has been used</returns>
    public bool UseKey()
    {
        if (keys == 0)
            return false;
        keys--;
        return true;
    }

    /// <summary>
    /// Add a key to the inventory
    /// </summary>
    public void AddKey()
    {
        keys++;
    }

    #endregion
}
