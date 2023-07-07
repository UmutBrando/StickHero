using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StickManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject _scoreText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;

    public float _score;
    public bool gameOver;


    [SerializeField] private StickController sticksPrefab;
    [SerializeField] private PillarManager pillarManager; 
    [SerializeField] private Transform targetRotate;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private float offSetX;
    [SerializeField] private ColliderDetect colliderDetect;

    private StickController _current;

    void Start()
    {

        _score = 0;
        gameOver = false;
        Create();
    
    
    }

    
    public void Create()
    {


        var position = pillarManager.currentPillarPosition;
        position.x += offSetX;

        var stick = Instantiate(sticksPrefab, position, Quaternion.identity);
        _current = stick;          
            
    }


  public void OnPointerDown()
    {
        _current.grow = true;
    }
    

  public void OnPointerUp()
    {
        _current.grow = false;
        
        IEnumerator Do()
        {
            var rotate = animationController.Rotate(_current.transform, targetRotate);
            yield return rotate;
            yield return null;
            colliderDetect.LevelController(_current.colliderPosition.position);
            yield return new WaitForSeconds(0.1f);
            if (colliderDetect.LevelPass)
            {
                UpdateScore();
                pillarManager.NextLevel();
                
            }
            else
                gameOver = true;



        }

        StartCoroutine(Do());
    }

    public void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        _scoreText.SetActive(false);

    }
    private void Update()
    {
        if (gameOver == true) 
        {
            GameOver();
            if (Input.GetMouseButtonDown(0))
                SceneManager.LoadScene(0);
           
            Time.timeScale = 1;
        }
    }

    public void UpdateScore()
    {
        _score += 1f;
        scoreText.text = "Score: " + _score;
        gameOverScoreText.text = "Score: " + _score;
    }

}
