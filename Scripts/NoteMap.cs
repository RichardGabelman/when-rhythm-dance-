using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoteData {
    public float spawnTime;
    public int column;
}

// NoteMap is responsible for generating and placing the notes in the correct spots at the correct times
public class NoteMap : MonoBehaviour {

    public GameObject notePrefab;

    public float spawnHeight;

    public List<NoteData> noteMap;

    public float songStartTime = 0f;

    private int colGap = 248; // spacing between the note/zone columns

    private float firstCol = -788; // pos of the first col (the A column)

    private int nextNoteIndex = 0;

    public RectTransform parentTransform;

    void Start() {
        // records the time at which the song starts
        songStartTime = Time.time;
        // generates the list of notes to play
        noteMap = GenerateNodeMap();
    }

    void Update() {
        float elapsedTime = Time.time - songStartTime;
        // if there is another note in the noteMap, and the time the note is supposed to be played
        // is here or passed, place the note in the correct column
        if (nextNoteIndex < noteMap.Count && elapsedTime >= noteMap[nextNoteIndex].spawnTime) {
            SpawnNoteInColumn(noteMap[nextNoteIndex].column);
            nextNoteIndex++;
        }
    }

    private List<NoteData> GenerateNodeMap() {
        float lastTime = 36f; // around where notes should stop being played to match the ending of the music
        // 0.2s is around where I determined the first beat starts
        // the song is 121 bpm so each beat should occur roughly at 0.496s increments
        float currTime = 0.2f + 0.496f;

        List<NoteData> newMap = new List<NoteData>();
        // while there is time for notes to be placed
        while (currTime <= lastTime) {
            // pick a random column index
            int randCol = Random.Range(0, 4);
            // generate a note for this time and the col
            NoteData noteData = new NoteData {
                spawnTime = currTime,
                column = randCol
            };
            // add the note to our noteMap
            newMap.Add(noteData);
            // and increment our time to where the next beat should be
            currTime += 0.496f;
        }
        return newMap;
    }

    // determines the proper xPos for the indicated column
    // and spawns the note at that xPos and the set heightPos
    private void SpawnNoteInColumn(int col) {
        // firstCol xPos + the colGap for each col after
        float xPos = (col * colGap) + firstCol;
        // the 2D pos where the note should be spawned
        Vector3 spawnPosition = new Vector2(xPos, spawnHeight);
        // instatiate the note
        GameObject note = Instantiate(notePrefab, parentTransform);
        // and then place it in the correct spot using the RectTransform component
        RectTransform rectTransform = note.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = spawnPosition;

        // set the key that the note will respond to via which col it is placed in
        KeyCode key = KeyCode.None;
        switch(col) {
            case 0:
                key = KeyCode.A;
                break;
            case 1:
                key = KeyCode.W;
                break;
            case 2:
                key = KeyCode.S;
                break;
            case 3:
                key = KeyCode.D;
                break;
        }
        note.GetComponent<CodeNoteObject>().keyToBePress = key;
    }
}
