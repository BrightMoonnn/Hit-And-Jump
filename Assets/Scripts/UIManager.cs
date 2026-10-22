using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager obj;

    public TextMeshProUGUI livesLbl;
    public TextMeshProUGUI scoreLbl;

    public Transform UIPanel;

    private void Awake()
    {
        obj = this;
    }

    public void updateLives()
    {
        livesLbl.text = "" + Player.obj.lives;
    }

    public void updateScore()
    {
        scoreLbl.text = "" + Game.obj.score;
    }

    public void startGame()
    {
        AudioManager.obj.playGui();
        
        Game.obj.gamePaused = true;
        Time.timeScale = 0f;
        UIPanel.gameObject.SetActive(true);

    }

    public void hideInitPanel()
    {
        AudioManager.obj.playGui();
        Game.obj.gamePaused = false;
        Time.timeScale = 1f;
        UIPanel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        obj = null;
    }
}
