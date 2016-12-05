using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Exporter : MonoBehaviour {

    private static GameObject[,] tileMemory;
    private static string[,] chunkMemory;

    private static List<string> chunks = new List<string>();
    private static List<int> episodes = new List<int>();

    private static int index;
    private static bool addToList;
    private static bool addToExisting;


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

    public void Start() {
        condition = _condition;
        chunks.Clear();
        episodes.Clear();
    }

    // Sets the current chunk where the index corresponds to the position of a tile inside the chunk.
    public static void setChunk(GameObject[,] posMem) {
        tileMemory = posMem;
    }

    // Logs the tile in the current chunk.
    public static void Set(GameObject chunk) {

        if (chunks.Count != 0) {
            for (int i = 0; i < chunks.Count; i++) {
                // If [true] increment episodes, or if [false] permit a new entry.
                if (chunks[i] == chunk.name) {
                    addToExisting = true;
                    index = i;
                } else {
                    addToList = true;
                }
            }
        }

        // If the entry is permitted, add the current chunk to the chunks-array.
        if (addToList && !addToExisting || chunks.Count == 0) {
            chunks.Add(chunk.name);
            episodes.Add(1);
            index = chunks.Count - 1;
        } else if (addToExisting) {
            episodes[index]++;
        }

        addToList = false;
        addToExisting = false;

        for (int j = 0; j < chunk.GetComponent<ChunkMemory>().memLayer1.GetLength(1); j++) {
            for (int i = 0; i < chunk.GetComponent<ChunkMemory>().memLayer1.GetLength(0); i++) {

                if (chunk.GetComponent<ChunkMemory>().memLayer1[i, j] != null) {
                    Write(chunk.GetComponent<ChunkMemory>().memLayer1[i, j], chunk, chunk.GetComponent<PrefabDescription>().instance, chunk.GetComponent<ChunkMemory>().memLayer1[i, j].GetComponent<TileChunkBridge>().level, chunk.GetComponent<ChunkMemory>().memLayer1[i, j].GetComponent<TileChunkBridge>().state, new Vector2(i, j), chunk.GetComponent<ChunkMemory>().memLayer1[i, j].GetComponent<TileChunkBridge>().playerPosX, chunk.GetComponent<ChunkMemory>().memLayer1[i, j].GetComponent<TileChunkBridge>().playerPosY);
                }

                if (chunk.GetComponent<ChunkMemory>().memLayer2[i, j] != null) {
                    Write(chunk.GetComponent<ChunkMemory>().memLayer2[i, j], chunk, chunk.GetComponent<PrefabDescription>().instance, chunk.GetComponent<ChunkMemory>().memLayer2[i, j].GetComponent<TileChunkBridge>().level, chunk.GetComponent<ChunkMemory>().memLayer2[i, j].GetComponent<TileChunkBridge>().state, new Vector2(i, j), chunk.GetComponent<ChunkMemory>().memLayer2[i, j].GetComponent<TileChunkBridge>().playerPosX, chunk.GetComponent<ChunkMemory>().memLayer2[i, j].GetComponent<TileChunkBridge>().playerPosY);
                }
            }
        }

        print(chunk);
    }

    // Write to the chunk memory.

    public static int condition;
    public int _condition;

    private static void Write(GameObject tile, GameObject chunk, int instance, int currentLevel, int result, Vector2 tilePos, string PlayerPosX, string PlayerPosY) {
        string log = tile.ToString() + "," + chunk.ToString() + "," + instance + "," + currentLevel + "," + ScoreManager.session + "," + episodes[index] + "," + result + "," + tilePos.x + "," + tilePos.y + "," + PlayerPosX + "," + PlayerPosY + "\n";

        switch (condition) {
            case 1:
                if (!File.Exists("Data/Premade1.csv")) {
                    File.WriteAllText("Data/Premade1.csv", log);
                } else {
                    File.AppendAllText("Data/Premade1.csv", log);
                }
                break;
            case 2:
                if (!File.Exists("Data/Premade2.csv")) {
                    File.WriteAllText("Data/Premade2.csv", log);
                } else {
                    File.AppendAllText("Data/Premade2.csv", log);
                }
                break;
            case 3:
                if (!File.Exists("Data/Controlled.csv")) {
                    File.WriteAllText("Data/Controlled.csv", log);
                } else {
                    File.AppendAllText("Data/Controlled.csv", log);
                }
                break;
            case 4:
                if (!File.Exists("Data/Experimental.csv")) {
                    File.WriteAllText("Data/Experimental.csv", log);
                } else {
                    File.AppendAllText("Data/Experimental.csv", log);
                }
                break;
        }
    }

    // Write chunk memory to .csv file.
    public static void WriteToCSV() {


        for (int j = 0; j < chunkMemory.GetLength(1); j++) {
            for (int i = 0; i < chunkMemory.GetLength(0); i++) {

                if (!File.Exists("Data.csv")) {
                    File.WriteAllText("Data.csv", chunkMemory[i, j]);

                } else {
                    File.AppendAllText("Data.csv", chunkMemory[i, j]);
                }
            }
        }
    }
}
