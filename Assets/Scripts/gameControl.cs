using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameControl : MonoBehaviour
{
    public GameObject sky1, sky2;
    Rigidbody2D physics1, physics2;
    float length = 0;
    public float backgroundSpeed = -2f;
    public GameObject obstacle;
    public int obstacleQuantity = 5;
    GameObject[] obstacles;
    float obstacleCreationTime;
    int obstacleCounter = 0;
    bool gameOverCheck = false; //öldükten sonra engel oluşturmaya devam etmesin
    // Start is called before the first frame update
    void Start()
    {
        physics1 = sky1.GetComponent<Rigidbody2D>();
        physics2 = sky2.GetComponent<Rigidbody2D>();
        physics1.velocity = new Vector2(backgroundSpeed, 0);
        physics2.velocity = new Vector2(backgroundSpeed, 0);
        length = sky1.GetComponent<BoxCollider2D>().size.x;
       
        obstacles = new GameObject[obstacleQuantity];
        for(int i=0; i<obstacles.Length; i++)
        {
            obstacles[i]=Instantiate(obstacle,new Vector2(-20,-20), Quaternion.identity);
            Rigidbody2D physicsObstacle = obstacles[i].AddComponent<Rigidbody2D>();
            physicsObstacle.gravityScale = 0;
            physicsObstacle.velocity = new Vector2(backgroundSpeed, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sky1.transform.position.x <= -length)
        {
            sky1.transform.position += new Vector3(length * 2, 0); 
        }
        if (sky2.transform.position.x <= -length)
        {
            sky2.transform.position += new Vector3(length * 2, 0);
        }
        //--------Obstacle operations
        obstacleCreationTime += Time.deltaTime;
        if(obstacleCreationTime>1.75f&&gameOverCheck==false)
        {
            obstacleCreationTime = 0;
            float AxisY = Random.Range(-1.15f,1.25f);
            obstacles[obstacleCounter].transform.position = new Vector3(12f,AxisY);
            obstacleCounter++;
            if(obstacleCounter>=obstacles.Length)
            {
                obstacleCounter = 0;
            }
        }

    }
    public void gameOver()
    {
        for (int i=0; i < obstacles.Length; i++)
        {
            obstacles[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            physics1.velocity = Vector2.zero;
            physics2.velocity = Vector2.zero;
            gameOverCheck = true;
        }  
    }
}
