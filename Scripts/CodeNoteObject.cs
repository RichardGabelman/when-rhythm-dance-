using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeNoteObject : MonoBehaviour {

    private bool canBePressed;

    public KeyCode keyToBePress;

    private bool obtained = false; // false means note hasn't been collected yet

    void Update() {
        float heightOfScoreZone = 160f;
        if (Input.GetKeyDown(keyToBePress)) {
            if (canBePressed) {
                float heightDiff = Mathf.Abs(transform.position.y - heightOfScoreZone); // note height - scoreZone height at time of press
                if (heightDiff > 15) {
                    // Ok hit
                    GameManager.instance.OkHit();
                } else if (heightDiff > 6) {
                    // Good hit
                    GameManager.instance.GoodHit();
                } else {
                    // Perfect hit
                    GameManager.instance.PerfectHit();
                }

                obtained = true;

                gameObject.SetActive(false); // note is deactivated
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D entering) {
        // if note is in the score zone, we can collect it
        if (entering.tag == "Zone") {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D leaving) {
        if (leaving.tag == "Zone") {
            canBePressed = false;
        }

        // if it hasn't been obtained by the time it has left the score zone, the note has been missed
        if (!obtained) {
            // Missed
            GameManager.instance.NoteMissed();
            gameObject.SetActive(false);
        }
    }
}
