using Assets.classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GainPlacment : MonoBehaviour
{

    //the table of possible position : 
    private MovementTable PositionTable;



    private Vector3 Position;

    public SnakeMovement snake;



    // Start is called before the first frame update
    void Start()
    {
        PositionTable =  new MovementTable(snake.TableSize, snake.cellSize);

        //we choose a random position for the first gain to eat : 
        Position = PositionTable.getPosition(Mathf.RoundToInt(Random.Range(0, snake.TableSize)), Mathf.RoundToInt(Random.Range(0, snake.TableSize)));

        this.transform.position = Position ;

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //we don't check the collider2d because the only one on the scene is the snake head ...


        // move the gain to an other point :
        Position = PositionTable.getPosition(Mathf.RoundToInt(Random.Range(0, snake.TableSize)), Mathf.RoundToInt(Random.Range(0, snake.TableSize)));
        //(but we avoid to put it on the snake body :  
        for (int i = 0; i< snake.graphicsBody.Count; i++)
        {
            //if the gain is miss located we relocate it :
            if (Position.Equals(snake.graphicsBody[i].transform.position))
            {
                Position = PositionTable.getPosition(Mathf.RoundToInt(Random.Range(0, snake.TableSize)), Mathf.RoundToInt(Random.Range(0, snake.TableSize)));
            }
        }
        
        
        this.transform.position = Position;


        //score + 1 : 
        ScoreScript.instance.addScore();
    }


  
}

