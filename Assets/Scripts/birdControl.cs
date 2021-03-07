using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class birdControl : MonoBehaviour
{
    public Sprite[] birdSprite;
    SpriteRenderer spriteRenderer;
    bool movementControl=true;
    int birdCounter=0, score=0, highScore = 0;
    float birdAnimationTime = 0;
    Rigidbody2D physics;
    public Text scoreText;
    bool gameOverCheck = false;
    gameControl gamecontrol;
    AudioSource[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        physics = GetComponent<Rigidbody2D>();
        gamecontrol = GameObject.FindGameObjectWithTag("gamecontrol").GetComponent<gameControl>();
        sounds = GetComponents<AudioSource>();
        highScore = PlayerPrefs.GetInt("highScore");
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOverCheck == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                physics.velocity = new Vector2(0, 0);
                physics.AddForce(new Vector2(0, 200));
                sounds[2].Play();
            }
            if (physics.velocity.y > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 35);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, -35);
            } 
        }
        Animation();
    }
    void Animation() {
        birdAnimationTime += Time.deltaTime;
        if(birdAnimationTime>0.25f)
        {
            birdAnimationTime = 0;
            if (movementControl)
            {
                spriteRenderer.sprite = birdSprite[birdCounter];
                birdCounter++;
                if (birdCounter == birdSprite.Length)
                {
                    birdCounter--;
                    movementControl = false;
                }
            }
            else
            {
                birdCounter--;
                spriteRenderer.sprite = birdSprite[birdCounter];
                if (birdCounter == 0)
                {
                    birdCounter++;
                    movementControl = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameOverCheck == false)
        {
            if (collision.gameObject.tag == "score")
            {
                score++;
                scoreText.text = " Score = " + score;
                sounds[1].Play();
                if (score > highScore)
                {
                    highScore = score;
                    PlayerPrefs.SetInt("highScore", highScore);
                }
            }
            if (collision.gameObject.tag == "obstacle")
            {
                gamecontrol.gameOver();
                sounds[0].Play();
                gameOverCheck = true;
                Invoke("goMainMenu", 2);
            }
        }
    }

    void goMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
