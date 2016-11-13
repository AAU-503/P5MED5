using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Exporter : MonoBehaviour {

    private static GameObject[,] tileMemory;
    private static string[,] chunkMemory;

    private static List<GameObject> chunks = new List<GameObject>();
    private static List<int> episodes = new List<int>();

    public static int instance;
    private static int index;
    private static bool addToList;

    /* PROCEDURE FOR EVENTLOG
        - Player inside chunk - 
            1. Create or overwrite memory.
            2. Check if chunk is listed.
                [True]: Count episode
                [False]: Add to chunk list 
                        / set episode to 1.
            3. Write tiles to the memory.
        - Player outside chunk - 
            1. Write all indices of chunk to csv. file. 
    */
    
    // Sets the current chunk where the index corresponds to the position of a tile inside the chunk.
    public static void setChunk(GameObject[,] chunk) {
        tileMemory = chunk;
    }

    // Logs the tile in the current chunk.
    public static void LogTile(GameObject tile, GameObject chunk, int result, Vector3 position) {

        if (tileMemory != null) {
            tileMemory[(int)position.x, (int)position.y].GetComponent<Renderer>().material.color = new Vector4(0f, 2f, 0f, 0.1f);

            // If there is information in the chunks-array, check if the current chunk is contained.
            if (chunks.Count != 0) {
                for (int i = 0; i < chunks.Count; i++) {

                    // If [true] increment episodes, or if [false] permit a new entry.
                    if (chunks[i].name == chunk.name) {
                        episodes[i]++;
                        index = i;
                    } else {
                        addToList = true;
                    }
                }
            }

            // If the entry is permitted, add the current chunk to the chunks-array.
            if (addToList || chunks.Count == 0) {
                chunks.Add(chunk);
                episodes.Add(1);
                index = 0;
                addToList = false;
            }

            /*Put data into a buffer*/

            WriteToCSV(tile.ToString(), chunk.ToString(), instance, 0, episodes[index], result, position);
        } else {
            Debug.Log("TileMemory returned a nullpointer exception.");
        }
    }

    // Instantiate or overwrite existing chunk memory. 
    public static void CreateMemory() {
        chunkMemory = new string[tileMemory.GetLength(0), tileMemory.GetLength(1)];
    }

    // Write to the chunk memory.
    public static void WriteToMemory(string tileID, string chunkID, int instance, int session, int episode, int result, Vector3 position) {
        string log = tileID + "," + chunkID + "," + instance + "," + session + "," + episode + "," + result + "," + position.x + "," + position.y + "\n";
    }

    // Write chunk memory to .csv file.
    public static void WriteToCSV(string tileID, string chunkID, int instance, int session, int episode, int result, Vector3 position) {
        //string file = "data.xls";
        string log = tileID + "," + chunkID + "," + instance + "," + ScoreManager.session + "," + episode + "," + result + "," + position.x + "," + position.y + "\n";

        if (!File.Exists("Data.csv")) {
            File.AppendAllText("Data.csv", log);
        } else {
            File.AppendAllText("Data.csv", log);
        }
    }
}
