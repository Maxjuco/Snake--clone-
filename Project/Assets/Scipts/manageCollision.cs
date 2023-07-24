using Assets.classes;
using UnityEngine;

public class manageCollision : MonoBehaviour
{

    public SnakeMovement snake;


   

    /*function which chek if in the next frame if the snake will collide with it self*/
    public bool CollideOnItself()
    {
        snake.getHeadDirection();
        //we try to obtain the next position of the head in the table using is actual direction : 
        //Vector2 nextHeadPosition = (snake.graphicsBody[0].transform.position / snake.cellSize) + snake.getHeadDirection();
        int nextHeadPositionX = Mathf.RoundToInt(MovementTable.instance.actualHeadPosition.x + snake.getHeadDirection().x);
        int nextHeadPositionY = Mathf.RoundToInt(MovementTable.instance.actualHeadPosition.y + snake.getHeadDirection().y);

        Vector2 nextHeadTablePosition = MovementTable.instance.getPositionHead(ref nextHeadPositionX, ref nextHeadPositionY);

        MovementTable temp = MovementTable.instance;

        int countParrt = snake.graphicsBody.Count;
        int countFull = 0; 
        for(int i =0; i< MovementTable.instance.size; i++)
        {
            for (int j = 0; j < MovementTable.instance.size; j++)
            {
                if (!MovementTable.instance.isEmpty(i, j))
                {
                    countFull++;
                }
            }
        }

        if (!MovementTable.instance.isEmpty(nextHeadPositionX, nextHeadPositionY))
        {
            /*Game over the snake eat its ass...*/

            Debug.Log("game over");

            //the snake die : 
            Die();
            return true;
        }

        return false;
 

    }


    public void Die()
    {
        //desactivate snake movement : 
        SnakeMovement.instance.enabled = false;

        //display game over menu : 
        GameOverManager.instance.OnSnakeDeath();
    }
}
