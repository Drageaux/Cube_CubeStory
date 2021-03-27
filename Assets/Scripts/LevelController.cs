using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class LevelController : MonoBehaviour
{
    private LevelTimer levelTimer;
    private LevelStarReward levelStarReward;

    private CanvasGroup levelEndCanvasGroup;
    public GameObject levelEndCanvas;

    private CanvasGroup mainCanvasGroup;
    public GameObject mainCanvas;

    public GameObject gameManager;
    private Orders orders;

    private MusicController musicController;

    // Start is called before the first frame update
    void Start()
    {
        this.levelTimer = gameObject.GetComponent<LevelTimer>();
        this.levelStarReward = gameObject.GetComponent<LevelStarReward>();
        this.levelEndCanvasGroup = levelEndCanvas.GetComponent<CanvasGroup>();
        this.mainCanvasGroup = mainCanvas.GetComponent<CanvasGroup>();
        this.musicController = gameObject.GetComponent<MusicController>();
        this.orders = gameManager.GetComponent<Orders>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.levelTimer.GetCurrTimer() <= 0 && this.levelTimer.IsTimerActive())
        {
            this.levelTimer.SetTimerActive(false);
            this.levelStarReward.ComputeStarReward(false, this.levelTimer.GetCurrTimer(), this.levelTimer.startTime);
            this.levelStarReward.SetStarRewardText();
            this.musicController.TriggerEndLevelMusic();
            EndLevel();
        }

        if (this.orders.orders.Count == 0)
        {
            this.levelTimer.SetTimerActive(false);
            this.levelStarReward.ComputeStarReward(true, this.levelTimer.GetCurrTimer(), this.levelTimer.startTime);
            this.levelStarReward.SetStarRewardText();
            this.musicController.TriggerEndLevelMusic();
            EndLevel();
        }
    }

    void EndLevel()
    {
        if (!levelEndCanvasGroup.interactable)
        {
            ToggleCanvas(levelEndCanvasGroup, true);
            ToggleCanvas(mainCanvasGroup, false);
            Time.timeScale = 0f;
        }
    }

    void ToggleCanvas(CanvasGroup canvas, bool decision)
    {
        canvas.interactable = decision;
        canvas.blocksRaycasts = decision;
        canvas.alpha = (decision == false) ? 0f : 1f;
    }
}
