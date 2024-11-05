using UnityEngine;
using TMPro;

// manages the opening and closing of the zone brackets upon button press
public class ZoneController : MonoBehaviour {

    public TextMeshProUGUI zoneText;
    public string openText;
    public string closedText;
    public KeyCode keyToPress;

    void Update() {
        // when the proper button is pressed,
        // change the text to the closed version of the brackets
        if (Input.GetKeyDown(keyToPress)) {
            zoneText.text = closedText;
        }
        // when the button is released,
        // change the text back to the open version of the brackets
        if (Input.GetKeyUp(keyToPress)) {
            zoneText.text = openText;
        }
    }
}
