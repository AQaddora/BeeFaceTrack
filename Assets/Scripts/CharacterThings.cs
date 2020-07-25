using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterThings : MonoBehaviour
{
    public static CharacterThings Instance;
    public static bool isControlling = true;
    public static float speedMultiplier = 1;
    private int _Score = 0;
    public int Score
	{
		get { return _Score; }
		set
        {
            scoreText.text = value.ToString();
            if(PlayerPrefs.GetInt("BS", 0) < value)
			{
                PlayerPrefs.SetInt("BS", value);
			}
            _Score = value;
        }
	}

    private int _tries = 5;
    public int Tries
	{
        get { return _tries; }
		set
		{
            if(value <= 0)
			{
                scoreTextInLoseScreen.text = Score.ToString();
                bestScoreText.text = "BEST: " + PlayerPrefs.GetInt("BS",0).ToString();
                loseScreen.SetActive(true);
                Time.timeScale = 0;
			}
            triesText.text = value.ToString();
            _tries = value;
		}
	}
    public TextMeshProUGUI scoreText, scoreTextInLoseScreen, bestScoreText;
    public TextMeshProUGUI triesText;
    public GameObject loseScreen;

    public float Speed = 125f;
    public float RightEndPoint, LeftEndPoint;
    public bool toRight = false, toLeft = false;

	private void Awake()
	{
        Instance = this;
        if (scoreText == null) scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        if (triesText == null) triesText = GameObject.Find("TriesText").GetComponent<TextMeshProUGUI>();
        scoreText.text = Score.ToString();
        triesText.text = Tries.ToString();
	}
	private void Update()
    {
        speedMultiplier += Time.deltaTime / 100.0f;
      
        if (toRight || Input.GetKey(KeyCode.RightArrow))
        {   
            if (transform.localPosition.x < RightEndPoint)
            {
                transform.position += transform.right * Time.deltaTime * Speed * speedMultiplier;
                transform.localRotation = Quaternion.identity;
            }
        }
        else if (toLeft || Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.localPosition.x > LeftEndPoint)
            {
                
                transform.position += transform.right * Time.deltaTime * Speed * speedMultiplier;
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    public void ToRight()
    {
        toRight = true;
    }
    public void EndToRight()
    {
        toRight = false;
    }
    public void ToLeft()
    {
        toLeft = true;
    }
    public void EndToLeft()
    {
        toLeft = false;
    }
    
    public void retry()
	{
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}