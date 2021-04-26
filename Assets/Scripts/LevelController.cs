using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

[RequireComponent(typeof(CanvasGroup))]
public class LevelController : MonoBehaviour
{
    private LevelTimer levelTimer;
    private LevelStarReward levelStarReward;

    private CanvasGroup levelEndCanvasGroup;
    public GameObject levelEndCanvas;

    private CanvasGroup mainCanvasGroup;
    public GameObject mainCanvas;

    private Orders orders;

    public GameObject character;
    private Health health;

    private MusicController musicController;

    // Start is called before the first frame update
    void Start()
    {
        this.levelTimer = gameObject.GetComponent<LevelTimer>();
        this.levelStarReward = gameObject.GetComponent<LevelStarReward>();
        this.levelEndCanvasGroup = levelEndCanvas.GetComponent<CanvasGroup>();
        this.mainCanvasGroup = mainCanvas.GetComponent<CanvasGroup>();
        this.musicController = gameObject.GetComponent<MusicController>();
        this.health = character.GetComponent<Health>();
        this.orders = Orders.instance;

        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if ((!this.health.Alive() || this.levelTimer.GetCurrTimer() <= 0) && this.levelTimer.IsTimerActive())
        {
            if (!this.health.Alive())
            {
                StartCoroutine(DieAndEndLevel());
            }
            else
            {
                EndLevel(false);
            }
            return;
        }
        
        if (this.orders.IsCompleted() && this.levelTimer.IsTimerActive())
        {
            EndLevel(true);
            return;
        }
    }

    IEnumerator DieAndEndLevel()
    {
        this.levelTimer.SetTimerActive(false);
        yield return new WaitForSeconds(4);
        EndLevel(false);
    }

    void StartLevel()
    {
        this.levelTimer.SetTimerActive(true);
        Cursor.visible = false;
        ToggleCanvas(levelEndCanvasGroup, false);
        ToggleCanvas(mainCanvasGroup, true);
        Time.timeScale = 1f;
    }

    void EndLevel(bool doesComputeStar = false)
    {
        this.levelTimer.SetTimerActive(false);
        this.levelStarReward.ComputeStarReward(doesComputeStar, this.levelTimer.GetCurrTimer(), this.levelTimer.startTime);
        this.levelStarReward.SetStarRewardText();
        this.musicController.TriggerEndLevelMusic();
        if (!levelEndCanvasGroup.interactable)
        {
            Cursor.visible = true;
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
