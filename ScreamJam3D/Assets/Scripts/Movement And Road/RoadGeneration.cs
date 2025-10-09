using System.Collections.Generic;
using UnityEngine;

public class RoadGeneration : MonoBehaviour
{
    // Fields
    [Header("Editable Values")]
    public int maxRoadPieces;

    [Header("Scene Objects/Prefabs")]
    public Transform truckTransform;
    public Transform roadParent;
    public List<GameObject> roadPieces;

    // Road pieces present in scene
    private List<GameObject> placedRoad;

    // Place 1st road piece, create more as needed to fill list
    void Start()
    {
        placedRoad = new List<GameObject>(maxRoadPieces + 1);
        for (int i = 0; i < maxRoadPieces; i++)
            NextRoadPiece(null);
    }

    /// <summary>
    /// Place a road piece and have it align with previous pieces if they exist
    /// </summary>
    public void NextRoadPiece(GameObject crossedRoad)
    {
        int index = -1;
        // Ensure road crossed isn't the back, allowing for deletion of back and placement of new road
        if (crossedRoad != null && placedRoad.Count > 0)
        {
            for (int i = 0; i < placedRoad.Count; i++)
            {
                if (crossedRoad == placedRoad[i])
                {
                    index = i;
                    break;
                }
            }
        }
        // Pick a random new road piece and add it to the list of placed road pieces
        if (index != 0)
        {
            GameObject newRoadPiece = Object.Instantiate(roadPieces[Random.Range(0, roadPieces.Count)], Vector3.zero, Quaternion.identity, roadParent);
            Transform newRoadTransform = newRoadPiece.transform;
            placedRoad.Add(newRoadPiece);
            // Overflow, remove last road
            if (placedRoad.Count > maxRoadPieces && index > 0)
            {
                GameObject roadToDelete = placedRoad[0];
                placedRoad.RemoveAt(0);
                Object.Destroy(roadToDelete);
            }
            // If this is the 1st piece, put the truck on its spawn point
            if (placedRoad.Count == 1)
            {
                Transform spawn = GetTransform(newRoadTransform, "Spawn");
                roadParent.position = truckTransform.position + Vector3.down * (truckTransform.localScale.y / 2 + newRoadTransform.localScale.y / 2)
                    + (newRoadTransform.position - spawn.position);
            }
            // Otherwise, place new piece such that its connected aligns with the previous piece's pivot
            else
            {
                Transform previousPiece = placedRoad[placedRoad.Count - 2].transform;
                Transform previousPivot = GetTransform(previousPiece, "Pivot");
                newRoadTransform.rotation = previousPivot.rotation;
                newRoadTransform.position = previousPivot.position + (newRoadTransform.position - GetTransform(newRoadTransform, "Connector").position);
            }
        }
    }

    /// <summary>
    /// Gets the child transform of an object with a given tag
    /// </summary>
    /// <param name="parent">The parent transform</param>
    /// <param name="tag">The tag of the child transform</param>
    /// <returns>The desired child transform, or the parent transform if the child doesn't exist</returns>
    private Transform GetTransform(Transform parent, string tag)
    {
        Transform[] childTransforms = parent.GetComponentsInChildren<Transform>();
        foreach (Transform childTransform in childTransforms)
        {
            if (childTransform.tag == tag)
                return childTransform;
        }
        return parent;
    }
}