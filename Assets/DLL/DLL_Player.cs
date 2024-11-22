using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DLL_Player : MonoBehaviour
{
    public PlayerController playerController;

    private Dictionary<string, float> moveDistances;

    void Start()
    {
        Debug.Log("PluginTester has started!");

        LoadMoveDistancesFromCSV();

        ChangeMoveDistance("1Tile"); // Sets our default movement to 1 tile space
    }

    void LoadMoveDistancesFromCSV()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "MoveDistances.csv");
        Debug.Log("Attempting to load CSV from: " + filePath);

        if (File.Exists(filePath))
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                Debug.Log("CSV file loaded. Number of lines: " + lines.Length);

                string[] headers = lines[0].Split(',');
                Debug.Log("Distance change codes found: " + string.Join(", ", headers.Skip(1)));

                moveDistances = new Dictionary<string, float>();

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] columns = lines[i].Split(',');
                    string key = columns[0].Trim();

                    if (key == "moveDistance")
                    {

                        for (int j = 1; j < headers.Length; j++)
                        {
                            string moveDistanceCode = headers[j].Trim(); // e.g., "en", "es", "fr"
                            if (float.TryParse(columns[j].Trim(), out float distance))
                            {
                                moveDistances[moveDistanceCode] = distance;
                                Debug.Log("Added moveDistance: " + moveDistanceCode + " = " + distance);
                            }
                            else
                            {
                                Debug.LogWarning("Invalid moveDistance value for " + moveDistanceCode + ": " + columns[j]);
                            }
                        }
                    }
                }

                Debug.Log("CSV file loaded and parsed successfully.");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error loading or parsing CSV file: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("CSV file not found at: " + filePath);
        }
    }

    void ChangeMoveDistance(string moveDistanceCode)
    {
        if (moveDistances == null)
        {
            Debug.LogError("moveDistances dictionary is null! CSV might not have been loaded correctly.");
            return;
        }

        if (!moveDistances.ContainsKey(moveDistanceCode))
        {
            Debug.LogWarning("Move distance not found for move distance code: " + moveDistanceCode);
            return;
        }

        if (playerController == null)
        {
            Debug.LogError("PlayerController reference is missing! Please assign the PlayerController in the inspector.");
            return;
        }

        // Set the moveDistance in PlayerController based on the move distance code
        playerController.moveDistance = moveDistances[moveDistanceCode];
        Debug.Log("Move distance changed to: " + playerController.moveDistance + " for move distance code: " + moveDistanceCode);
    }

    // Method to change the moveDistance when a button is clicked
    public void OnMoveDistanceChangeButtonClicked(string newMoveDistanceCode)
    {
        ChangeMoveDistance(newMoveDistanceCode);
    }
}
