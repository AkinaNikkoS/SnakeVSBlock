using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{
    public Player Controls;
    public GameObject Loss;
    public GameObject Win;
    public GameObject ConfetyLeft;
    public GameObject ConfetyRight;
    public Text LevelNumber;
    public int Level = 1;
    public enum State
    {
        Playing,
        Won,
        Loss,
    }

    private AudioSource _audio;
    
    public State CurrentState { get; private set; }

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        LevelNumber.text = "Level " + Level.ToString();
    }
    public void OnPlayerDied()
    {
        if (CurrentState != State.Playing) return;       
        CurrentState = State.Loss;
        Controls.enabled = false;
        Debug.Log("Game Over!");
        Loss.SetActive(true);
        _audio.Stop();
    }


    public void OnPlayerWon()
    {
        if (CurrentState != State.Playing) return;
        ConfetyLeft.SetActive(true);
        ConfetyRight.SetActive(true);
        CurrentState = State.Won;
        Controls.enabled = false;
        Debug.Log("You Won!");
        Win.SetActive(true);
        _audio.Stop();
        Level++;
        if (Level > 3)
        { 
            Level = 1;
        }
        LevelNumber.text = "Level " + Level.ToString();
    }

    public void OnPlayerRestart()
    {
        Loss.SetActive(false);
        ReloadLevel(Level);
        _audio.Play();
        LevelNumber.text = "Level " + Level.ToString();
    }
    public void OnPlayerNextLevel()
    {
        _audio.Stop();
        if (CurrentState != State.Won) return;
        if (Level == 1) ReloadLevel(Level);
        else
        {
            Win.SetActive(false);
            _audio.Play();
            CurrentState = State.Playing;
            Controls.enabled = true;
        }

        ConfetyLeft.SetActive(false);
        ConfetyRight.SetActive(false);
    }

    public void ReloadLevel(int level)
    {
        if (level == 2)
        {            
            SceneManager.LoadScene("Level2");
            LevelNumber.text = "Level " + level.ToString();
        }
        if (level == 3)
        {
            SceneManager.LoadScene("Level3");
            LevelNumber.text = "Level " + level.ToString();
        }
        if (level == 1)
        {
            SceneManager.LoadScene("Level1");
            LevelNumber.text = "Level " + level.ToString();
        }
    }
}
