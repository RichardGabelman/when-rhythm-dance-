using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    public AudioSource music;
    public AudioSource hitNoise;

    private int currentScore;
    private int scorePerNote = 1000;
    private int scorePerGoodNote = 1250;
    private int scorePerPerfectNote = 1500;

    private int currentMultiplier;
    private int multiplierTracker;
    public int[] multiplierThresholds;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiplierText;

    private float totalNotes;
    private float okHits;
    private float goodHits;
    private float perfectHits;
    private float misses;

    public GameObject resultsScreen;
    public TextMeshProUGUI percentHitText, oksText, goodsText, perfectsText, missesTexts, rankText, finalScoreText;

    public static GameManager instance;

    public TerminalManager terminalManager;

    void Start() {
        instance = this; // set this to be the only instance of GameManager

        music.Play(); // play the song right when we load

        // initialize the score and multiplier
        scoreText.text = "0";
        currentMultiplier = 1;
    }

    void Update() {
        // once the music has stopped, we activate the results screen once
        if (!music.isPlaying && !resultsScreen.activeInHierarchy) {
            resultsScreen.SetActive(true);

            // calculate various metrics
            totalNotes = okHits + goodHits + perfectHits + misses;
            float totalHit = okHits + goodHits + perfectHits;
            float percentHit = (totalHit / totalNotes) * 100f;

            float peakRankVal = totalNotes * scorePerPerfectNote;
            // rank val is score * note, for each category
            float currRankVal = (scorePerNote * okHits) + (scorePerGoodNote * goodHits) + (scorePerPerfectNote * perfectHits);
            float currRankFactor = currRankVal / peakRankVal;

            // display the metrics
            oksText.text = okHits.ToString();
            goodsText.text = goodHits.ToString();
            perfectsText.text = perfectHits.ToString();
            missesTexts.text = misses.ToString();
            finalScoreText.text = currentScore.ToString();
            percentHitText.text = percentHit.ToString("F2") + "%";

            // Anything below 0.6 is an F
            string rankVal = "F";
            // 0.6 - 0.699 is D
            if (currRankFactor >= 0.6) {
                rankVal = "D";
            }
            // 0.7 - 0.799 is C
            if (currRankFactor >= 0.7) {
                rankVal = "C";
            }
            // 0.8 - 0.899 is B
            if (currRankFactor >= 0.8) {
                rankVal = "B";
            }
            // 0.9 - 0.959 is A
            if (currRankFactor >= 0.9) {
                rankVal = "A";
            }
            // 0.96 - 0.979 is S
            if (currRankFactor >= 0.96) {
                rankVal = "S";
            }
            // 0.98 - 0.999 is SS
            if (currRankFactor >= 0.98) {
                rankVal = "SS";
            }
            // 1 is SSS
            if (currRankFactor == 1) {
                rankVal = "SSS";
            }
            rankText.text = rankVal;
            
        }
    }

    // every note hit triggers this function
    public void CodeNoteHit() {
        hitNoise.Play(); // plays the key typing noise

        // tracks changing the multiplier
        if (currentMultiplier - 1 < multiplierThresholds.Length) {
            multiplierTracker++;

            // each value in the threshold represnts how many you need to hit in a row to get to the next multiplier level
            if (multiplierTracker >= multiplierThresholds[currentMultiplier - 1]) {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        // updates the multiplier and score text
        multiplierText.text = currentMultiplier.ToString() + "x";
        scoreText.text = currentScore.ToString();
    }

    public void OkHit() {
        // Ok hits are 1000 * currentMultiplier
        currentScore += scorePerNote * currentMultiplier;
        CodeNoteHit();
        okHits++;
        // terminal message for ok hit
        terminalManager.AddLine("Ok hit... +" + (scorePerNote * currentMultiplier));
    }

    public void GoodHit() {
        // Good hits are 1250 * currentMultiplier
        currentScore += scorePerGoodNote * currentMultiplier;
        CodeNoteHit();
        goodHits++;
        // terminal message for good hit
        terminalManager.AddLine("Good hit! +" + (scorePerGoodNote * currentMultiplier));
    }

    public void PerfectHit() {
        // Perfect hits are 1500 * currentMultiplier
        currentScore += scorePerPerfectNote * currentMultiplier;
        CodeNoteHit();
        perfectHits++;
        // terminal message for perfect hit
        terminalManager.AddLine("PERFECT HIT!!! +" + (scorePerPerfectNote * currentMultiplier));
    }

    public void NoteMissed() {
        // missing a note resets the multiplier back to the first stage
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiplierText.text = "1x";

        misses++;
        // terminal message for missed hit
        terminalManager.AddLine("NOTE MISSED =(");
    }
}
