using UnityEngine;

/// <summary>
/// Grid windows camera
/// </summary>
public class CameraMan : MonoBehaviour
{
    #region Public properties

    /// <summary>
    /// The size of each window (Camera window size)
    /// </summary>
    public Vector2 WindowSize;

    /// <summary>
    /// The target that the camera man follow
    /// </summary>
    public GameObject Target;

    /// <summary>
    /// The size of the world based on grid windows
    /// </summary>
    public Vector2 GridSize;

    /// <summary>
    /// From where the grid started (Top-Left)
    /// </summary>
    public Vector2 GridOffset;

    #endregion

    #region Private members

    /// <summary>
    /// ??
    /// </summary>
    private Vector2 gridStartPosition;

    /// <summary>
    /// ??
    /// </summary>
    private Vector2 gridEndPosition;

    /// <summary>
    /// ??
    /// </summary>
    private Vector2 gridEndPositionNoOffset;

    /// <summary>
    /// The position of the current window in the grid
    /// </summary>
    private Vector2 currentCell;

    #endregion

    #region Unity methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        gridStartPosition = GridOffset * WindowSize;
        gridEndPositionNoOffset = WindowSize * GridSize;
        gridEndPosition = gridStartPosition + gridEndPositionNoOffset;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        Vector2 index = GetTargetIndex();
        if (index.x != currentCell.x || index.y != currentCell.y)
        {
            Vector2 pos = gridStartPosition + index * WindowSize + WindowSize / 2;
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
            currentCell = index;
        }
    }

    #endregion

    #region Helper methods

    private Vector2 GetTargetIndex()
    {
        Vector2 index = (new Vector2(Mathf.Clamp(Target.transform.position.x, gridStartPosition.x, gridEndPosition.x - 1),
            Mathf.Clamp(Target.transform.position.y, gridStartPosition.y, gridEndPosition.y - 1)) + (GridOffset * -1 * WindowSize)) / gridEndPositionNoOffset * GridSize;
        return new Vector2(Mathf.FloorToInt(index.x), Mathf.FloorToInt(index.y));
    }

    #endregion
}
