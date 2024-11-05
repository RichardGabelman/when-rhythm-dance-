using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeScroller : MonoBehaviour {

    public float tempo;
    private float noteSpeed;
    public float targetDistance = 970f; // note spawn point to the middle of the score zone

    public TextMeshProUGUI codeNoteText;
    private float changeInterval = 0.05f; // how fast the note changes character
    private float timer = 0;
    private string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ@#$%&*"; // possible chars for the note

    void Start() {
        float secondsPerBeat = 60f / tempo;

        noteSpeed = targetDistance / (6 * secondsPerBeat); // note should take 6 beats to reach the zone from spawning
    }

    void Update() {
        timer += Time.deltaTime;
        // count difference in time until we meet the change interval
        if (timer >= changeInterval) {
            timer = 0;
            // changes the character of the note to a random character
            codeNoteText.text = characters[Random.Range(0, characters.Length)].ToString();
        }

        // lower the note by noteSpeed * Time.deltaTime
        transform.position -= new Vector3(0f, noteSpeed * Time.deltaTime, 0f);

    }
}
