using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// manages the progress terminal on the right
public class TerminalManager : MonoBehaviour {

    public TextMeshProUGUI terminalText;
    public int maxLines = 35; // max amount of lines that will fit on the terminal with the current font size
    private List<string> lines = new List<string>();

    public void AddLine(string line) {
        if (lines.Count > 0) {
            // remove the | cursor line of the previous line (if it exists)
            lines[lines.Count - 1] = lines[lines.Count - 1].Substring(0, lines[lines.Count - 1].Length - 1);
        }
        // print the terminal user heading, the given line, and a | representing the terminal waiting for more input
        lines.Add("USER@TERMINAL:~$ " + line + "|");
        // if the terminal reaches capacity, remove from the beginning to show more lines
        if (lines.Count > maxLines) {
            lines.RemoveAt(0);
        }
        // set the text to the text of the lines given (that fit)
        terminalText.text = string.Join("\n", lines);
    }
}
