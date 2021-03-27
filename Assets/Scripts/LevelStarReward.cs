using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelStarReward : MonoBehaviour
{
    public TextMeshProUGUI starRewardText;
    private int starRewarded;

    public float ThreeStarThreshold;
    public float TwoStarThreshold;
    public float OneStarThreshold;

    // Start is called before the first frame update
    void Start()
    {
        // default thresholds
        this.ThreeStarThreshold = 0.6f;
        this.TwoStarThreshold = 0.4f;
        this.OneStarThreshold = 0.2f;
        this.starRewarded = -1;
    }

    public void ComputeStarReward(bool levelComplete, double timeLeft, double totalTime)
    {
        if (levelComplete == false)
        {
            this.starRewarded = 0;
        } else
        {
            double timeCompleteRatio = timeLeft / totalTime;
            if (timeCompleteRatio >= this.ThreeStarThreshold)
            {
                this.starRewarded = 3;
            } else if (timeCompleteRatio >= this.TwoStarThreshold && timeCompleteRatio < this.ThreeStarThreshold)
            {
                this.starRewarded = 2;
            } else if (timeCompleteRatio >= this.OneStarThreshold && timeCompleteRatio < this.TwoStarThreshold)
            {
                this.starRewarded = 1;
            } else
            {
                this.starRewarded = 0;
            }
        }
    }

    public void SetStarRewardText()
    {
        this.starRewardText.text = "Star Reward: " + this.starRewarded.ToString();
    }
}
