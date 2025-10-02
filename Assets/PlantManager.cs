using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;  // NEW: Added this line

public class PlantManager : MonoBehaviour
{
    [Header("Plant Settings")]
    public GameObject plantPrefab;
    public Key plantKey = Key.P;  // Changed from KeyCode to Key

    [Header("References")]
    public Transform player;
    public float detectionRadius = 1.5f;

    void Update()
    {
        // Try simpler key detection
        if (Keyboard.current != null)
        {
            if (Keyboard.current.pKey.wasPressedThisFrame)
            {
                Debug.Log("P key detected!");
                TryPlantSeed();
            }
        }
    }

    void TryPlantSeed()
    {
        GameObject closestTile = FindClosestTile();

        if (closestTile == null)
        {
            Debug.Log("No tile nearby!");
            return;
        }

        FarmTileControl tileScript = closestTile.GetComponent<FarmTileControl>();

        if (tileScript != null && tileScript.tileCond == FarmTileControl.FarmTileCond.Watered)
        {
            if (IsTileOccupied(closestTile))
            {
                Debug.Log("Already planted here!");
                return;
            }

            SpawnPlant(closestTile.transform.position);
        }
        else
        {
            Debug.Log("Tile must be watered first!");
        }
    }

    GameObject FindClosestTile()
    {
        // ADD THESE DEBUG LINES
        Debug.Log("Player position: " + player.position);
        Debug.Log("Detection radius: " + detectionRadius);

        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Farm Tile");
        Debug.Log("Total tiles found: " + tiles.Length);  // ADD THIS TOO

        GameObject closest = null;
        float closestDistance = detectionRadius;

        foreach (GameObject tile in tiles)
        {
            float distance = Vector3.Distance(player.position, tile.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = tile;
            }
        }

        return closest;
    }

    bool IsTileOccupied(GameObject tile)
    {
        foreach (Transform child in transform)
        {
            if (Vector3.Distance(child.position, tile.transform.position) < 0.5f)
            {
                return true;
            }
        }
        return false;
    }

    void SpawnPlant(Vector3 position)
    {
        Vector3 spawnPos = position + Vector3.up * 0.1f;
        GameObject newPlant = Instantiate(plantPrefab, spawnPos, Quaternion.identity);
        newPlant.transform.parent = transform;

        Debug.Log("Plant spawned!");
    }
}