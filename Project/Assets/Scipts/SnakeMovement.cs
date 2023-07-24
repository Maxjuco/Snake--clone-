using Assets.classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class SnakeMovement : MonoBehaviour
{

    //to control the direction : 
    Vector3 up = new Vector3(0, 1, 0);
    Vector3 down = new Vector3(0, -1, 0);
    Vector3 right = new Vector3(1, 0, 0);
    Vector3 left = new Vector3(-1, 0, 0);
    private Vector3 direction;


    //to control the sprite rotation : 
    Vector3 upRotation = new Vector3(0,0,90);
    Vector3 downRotation = new Vector3(0, 0, 270);
    Vector3 rightRotation = new Vector3(0, 0, 0);
    Vector3 leftRotation = new Vector3(0, 0, 180);
    Vector3 directionRotation;


    public List<GameObject> graphicsBody;
    public List<manageSprit> spritsBody;


    //to have all the sprites : 
    public Sprite[] SpritesList;

   
    public int TableSize;
    public float cellSize;

    public float moveSpeed;
    private float _time;
    
    public int maxSize;

    public GainPlacment gainEntity;



    //create a Singleton instance 
    public static SnakeMovement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Two instance of GameOverManager are in the scene !");
            return;
        }

        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        //initialize the direction face by the head : 
        direction = right;
        MovementTable.instance = new MovementTable(TableSize, cellSize);
        MovementTable.instance = new MovementTable(TableSize, cellSize);
        //we set the state of the cell on which are the body parts : 
        for(int i = 0; i<graphicsBody.Count; i++)
        {
            MovementTable.instance.SetFull(true, Mathf.RoundToInt(graphicsBody[i].transform.position.x / cellSize), Mathf.RoundToInt(graphicsBody[i].transform.position.y / cellSize));
        }

        //give the position in the table of the head : 
        MovementTable.instance.actualHeadPosition = new Vector2(2, 0);
        _time = 30 / moveSpeed;



    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        switchDirection();

        //calculate the movement : 


        autoMove();


        

    }


    public void switchDirection()
    {

     
        //go up : 
        if (direction != up && direction != down && checkKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.UpArrow) && graphicsBody[1].transform.rotation.eulerAngles != downRotation)
        {


            direction = up;
            directionRotation = upRotation;
            
            //turn the head with the right sprite : 
            switch (graphicsBody[1].transform.rotation.eulerAngles.z)
            {
                //if -> 
                case 0:
                    spritsBody[0].changeSprite(SpritesList[4]);
                    break;

                case 180:
                    spritsBody[0].changeSprite(SpritesList[5]);
                    break;
            }

            //rotation : 
            graphicsBody[0].transform.rotation = Quaternion.Euler(0, 0, directionRotation.z);
            
            //headturn = index 4
            //headturn 2 = index 5


            Vector3 headPosition = MovementTable.instance.actualHeadPosition;
            int HeadX = Mathf.RoundToInt(headPosition.x);
            int HeadY = Mathf.RoundToInt(headPosition.y+1);

            MovementTable.instance.getPositionHead(ref HeadX, ref HeadY);

            if (MovementTable.instance.containTable[HeadX, HeadY] == 'F')
            {
                if (graphicsBody[0].GetComponent<manageCollision>().CollideOnItself())
                {
                    //play the animation of smoke : 
                    //graphicsBody[i]
                }
            }
        }

        else if (direction != up && direction != down && checkKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.DownArrow) && graphicsBody[1].transform.rotation.eulerAngles != upRotation)
        {
            direction = down;
            directionRotation = downRotation;


            //turn the head with the right sprite : 
            switch (graphicsBody[1].transform.rotation.eulerAngles.z)
            {
                //if -> 
                case 0:
                    spritsBody[0].changeSprite(SpritesList[5]);
                    break;

                case 180:
                    spritsBody[0].changeSprite(SpritesList[4]);
                    break;
            }

            //rotation : 
            graphicsBody[0].transform.rotation = Quaternion.Euler(0, 0, directionRotation.z);



            Vector3 headPosition = MovementTable.instance.actualHeadPosition;
            int HeadX = Mathf.RoundToInt(headPosition.x);
            int HeadY = Mathf.RoundToInt(headPosition.y-1);

            MovementTable.instance.getPositionHead(ref HeadX, ref HeadY);

            if (MovementTable.instance.containTable[HeadX, HeadY] == 'F')
            {
                if (graphicsBody[0].GetComponent<manageCollision>().CollideOnItself())
                {
                    //play the animation of smoke : 
                    //graphicsBody[i]
                }
            }
        }

        else if (direction != left && direction != right && checkKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.RightArrow) && graphicsBody[1].transform.rotation.eulerAngles != leftRotation)
        {
            direction = right;
            directionRotation = rightRotation;

            //turn the head with the right sprite : 
            switch (graphicsBody[1].transform.rotation.eulerAngles.z)
            {
                //if -> 
                case 90:
                    spritsBody[0].changeSprite(SpritesList[5]);
                    break;

                case 270:
                    spritsBody[0].changeSprite(SpritesList[4]);
                    break;
            }

            //rotation : 
            graphicsBody[0].transform.rotation = Quaternion.Euler(0, 0, directionRotation.z);


            Vector3 headPosition = MovementTable.instance.actualHeadPosition;
            int HeadX = Mathf.RoundToInt(headPosition.x+1);
            int HeadY = Mathf.RoundToInt(headPosition.y);

            MovementTable.instance.getPositionHead(ref HeadX, ref HeadY);

            if (MovementTable.instance.containTable[HeadX, HeadY] == 'F')
            {
                if (graphicsBody[0].GetComponent<manageCollision>().CollideOnItself())
                {
                    //play the animation of smoke : 
                    //graphicsBody[i]
                }
            }
        }

        else if (direction != left && direction != right && checkKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftArrow) && graphicsBody[1].transform.rotation.eulerAngles != rightRotation)
        {
            direction = left;
            directionRotation = leftRotation;


            //turn the head with the right sprite : 
            switch (graphicsBody[1].transform.rotation.eulerAngles.z)
            {
                //if -> 
                case 90:
                    spritsBody[0].changeSprite(SpritesList[4]);
                    break;

                case 270:
                    spritsBody[0].changeSprite(SpritesList[5]);
                    break;
            }

            //rotation : 
            graphicsBody[0].transform.rotation = Quaternion.Euler(0, 0, directionRotation.z);


            Vector3 headPosition = MovementTable.instance.actualHeadPosition;
            int HeadX = Mathf.RoundToInt(headPosition.x-1);
            int HeadY = Mathf.RoundToInt(headPosition.y);

            MovementTable.instance.getPositionHead(ref HeadX, ref HeadY);

            if (MovementTable.instance.containTable[HeadX, HeadY] == 'F')
            {
                if (graphicsBody[0].GetComponent<manageCollision>().CollideOnItself())
                {
                    //play the animation of smoke : 
                    //graphicsBody[i]
                }
            }
        }
        
        
    }


    private bool checkKey(KeyCode keyPressed)
    {
        
        switch(keyPressed)
        {
            //if go up check left and right direction aren't pressed : 
            case KeyCode.UpArrow:
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                    return false;
                else
                    return true;
                break;

            //if go down check left and right direction aren't pressed : 
            case KeyCode.DownArrow:
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                    return false;
                else
                    return true;
                break;

            //if go left check up and down direction aren't pressed : 
            case KeyCode.LeftArrow:
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
                    return false;
                else
                    return true;
                break;

            //if go right check up and down direction aren't pressed : 
            case KeyCode.RightArrow:
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
                    return false;
                else
                    return true;
                break;

        }

        //if not sure do nothing ;)  
        return false;
    }

    public void autoMove()
    {


       

     

        //co routine didn't work for the table lecture  !!
        //StartCoroutine(DelayMovement(20f));

        _time -= 1* Time.deltaTime;
        if(_time <= 0)
        {
            //take the actual head position : 
            Vector3 HeadPosition = MovementTable.instance.actualHeadPosition;

            //so we got the next position in the table : 
            int nextX = Mathf.RoundToInt(HeadPosition.x + direction.x);
            int nextY = Mathf.RoundToInt(HeadPosition.y + direction.y);

            //copy the head position in a 
            Vector2 TempPos = new Vector2(HeadPosition.x, HeadPosition.y);
            //copy the head angle : 
            float tempAngleZ = directionRotation.z;
            //head movement first : 

            graphicsBody[0].transform.position = MovementTable.instance.getPositionHead(ref nextX, ref nextY);


            


            //rotation : 
            graphicsBody[0].transform.rotation = Quaternion.Euler(0, 0, tempAngleZ);

            spritsBody[0].changeSprite(SpritesList[0]);

            //fianly we set the actual cell on Full : 
            MovementTable.instance.SetFull(true, nextX, nextY);


            MovementTable.instance.actualHeadPosition = new Vector2(nextX, nextY);

            GameObject TempBodyPart = graphicsBody[1];

            Sprite SpriteBodyTemp = spritsBody[1].spriteRenderer.sprite;
            //the rest of the body : 
            for(int i = 1; i< graphicsBody.Count; i++)
            {                                
                //we save the values before : 
                float tempAngleZ2 = graphicsBody[i].transform.rotation.eulerAngles.z;
                Vector2 TempPos2 = graphicsBody[i].transform.position/cellSize;

               
                /*BIG PART to manage the turning and addition of a body part :*/
                
                
                //if we are at the tail :
                //and if the score is not accurate with the lenght of the snake : 
                if (i == graphicsBody.Count - 1 && ScoreScript.instance.getScore() != graphicsBody.Count - 3)
                {
                    //we add a part on the position which is empty with the copy of the last part : 
                    AddAPart(TempPos, tempAngleZ, SpriteBodyTemp);

                    //rotate it 
                    graphicsBody[i].transform.rotation = Quaternion.Euler(0, 0, tempAngleZ);
                    //and move it :
                    graphicsBody[i].transform.position = MovementTable.instance.getPosition(Mathf.RoundToInt(TempPos.x), Mathf.RoundToInt((TempPos.y)));
                    //and we do not move the tail... so we get out of the loop : 
                    i = graphicsBody.Count-1;

                    //to fix the tail position : 
                    //graphicsBody[i].transform.rotation = Quaternion.Euler(0, 0, tempAngleZ);
                }
                //if it is just the tail : 
                else if(i == graphicsBody.Count - 1)
                {
                    //we load the tail :
                    spritsBody[i].changeSprite(SpritesList[6]);

                    //rotate it 
                    graphicsBody[i].transform.rotation = Quaternion.Euler(0, 0, tempAngleZ);
                    //and move it :
                    graphicsBody[i].transform.position = MovementTable.instance.getPosition(Mathf.RoundToInt(TempPos.x), Mathf.RoundToInt((TempPos.y)));


                    //fianly we set the previous cell on Empty : 
                    int PrevX = Mathf.RoundToInt(TempPos2.x);
                    int PrevY = Mathf.RoundToInt((TempPos2.y));
                    Vector3 PrevPos = MovementTable.instance.getPositionHead(ref PrevX, ref PrevY);

                    MovementTable.instance.SetFull(false, PrevX, PrevY);

                    

                }
                //if it is a body part : 
                else
                {
                    //if it is not the tail : we still coping the body part in a Temp : 
                    //TempBodyPart = graphicsBody[i];
                    SpriteBodyTemp = spritsBody[i].spriteRenderer.sprite;
                    

                    Turning(i, tempAngleZ);

                    //rotate it 
                    graphicsBody[i].transform.rotation = Quaternion.Euler(0, 0, tempAngleZ);
                    //and move it :
                    graphicsBody[i].transform.position = MovementTable.instance.getPosition(Mathf.RoundToInt(TempPos.x), Mathf.RoundToInt((TempPos.y)));
                }
                /*
                else
                {
                    
                    //if it is not the tail : we still coping the body part in a Temp : 
                    //TempBodyPart = graphicsBody[i];
                    SpriteBodyTemp = spritsBody[i].spriteRenderer.sprite;
                    //we modify the position / rotation of the body part to take the ones of the previous part : 
                    graphicsBody[i].transform.position = MovementTable.instance.getPosition(Mathf.RoundToInt(TempPos.x), Mathf.RoundToInt((TempPos.y)));

                    Turning(i);
               
                    //if the part is a body straight which is turning :
                    if (tempAngleZ != tempAngleZ2 && i != graphicsBody.Count - 1)
                    {
                        //we try to obtain the direction of this part : 
                        Vector2 directionTemp = new Vector2(TempPos.x - TempPos2.x, TempPos.y - TempPos2.y);
                        //switch wich turn the snake take we have to load a diffrent texture : 
                        //so we look both the actual turn angle and the previous direction taken : 

                        bool condDir = ((directionTemp.x > 0 || directionTemp.y > 0));
                        bool condAngle = (tempAngleZ > 0 && tempAngleZ < 270);
                        //first case we load the turn1 sprite
                        if ((condDir && condAngle) || (!condDir && !condAngle))
                        {
                            spritsBody[i].changeSprite(SpritesList[2]);

                            //to fix teh turn on the border : 
                            if (spritsBody[i].transform.position.y == 0 || spritsBody[i].transform.position.y == (TableSize - 1) * cellSize)
                            {
                                if (UseSpriteTurn1(i))
                                {
                                    spritsBody[i].changeSprite(SpritesList[2]);
                                }
                                else
                                {
                                    spritsBody[i].changeSprite(SpritesList[3]);
                                }
                            }
                        }
                        //seconde case we load the turn2 sprite :
                        else if ((condDir && !condAngle) || (!condDir && condAngle))
                        {
                            spritsBody[i].changeSprite(SpritesList[3]);

                            //to fix the turn on the border :
                            if (spritsBody[i].transform.position.y == 0 || spritsBody[i].transform.position.y == (TableSize - 1) * cellSize)
                            {
                                if (UseSpriteTurn1(i))
                                {
                                    spritsBody[i].changeSprite(SpritesList[2]);
                                }
                                else
                                {
                                    spritsBody[i].changeSprite(SpritesList[3]);
                                }
                            }
                        }


                    }
                    //else if the body go straight we set is texure on straight : 
                    else if (tempAngleZ == tempAngleZ2 && i != graphicsBody.Count - 1)
                    {
                        spritsBody[i].changeSprite(SpritesList[1]);
                    }


                  */  
                

                //we take the value of the previous position of the body part : 
                TempPos = TempPos2;
                tempAngleZ = tempAngleZ2;


                //and we check if the head collide in the rest of the body : 
                
            }




        
            graphicsBody[0].GetComponent<manageCollision>().CollideOnItself();


            
            /*TODO : FIX THE BUG OF THE BODY PART USING THE WRONG SPRITE WHEN NEAR x = 0 OR 16 OR Y = 0 OR 16 (SEE THE condAngle AND condDir)*/

            _time = 30 / moveSpeed;
        }
        
    }






    public void AddAPart(Vector2 PositonCopy, float angleZCopy, Sprite SpriteCopy)
    {
        //first we save the tail's data : 
        GameObject TailTemp = graphicsBody[graphicsBody.Count - 1];
        manageSprit TailmanageSprit = spritsBody[spritsBody.Count - 1];
        //we save the angle of the rotation and the position to : 
        float TailAngleZ = graphicsBody[graphicsBody.Count-1].transform.rotation.eulerAngles.z;
        Vector2 TailPosition = graphicsBody[graphicsBody.Count - 1].transform.position/cellSize;

        //copy the components and set up from the copy : 
        GameObject newBodyPart = new GameObject(String.Concat("SnakeBody", ScoreScript.instance.getScore()));
        //we set the reference for the positon : 
        newBodyPart.transform.parent = this.transform;
        //we set the position in the reference : 
        newBodyPart.transform.position = MovementTable.instance.getPosition(Mathf.RoundToInt(PositonCopy.x), Mathf.RoundToInt(PositonCopy.y));

        //then we had the necessary part : 
        //the sprite renderer : 
        SpriteRenderer spriteRenderer = newBodyPart.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        spriteRenderer.sprite = SpriteCopy;

        //the boxcollider2d : 
        BoxCollider2D boxCollider2D = newBodyPart.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;

        //the script for the sprite : 
        manageSprit manageSprit = newBodyPart.AddComponent(typeof(manageSprit)) as manageSprit;

        //finaly we put the sprite with the right angle : 
        newBodyPart.transform.rotation = Quaternion.Euler(0, 0, angleZCopy);

        
        //we replace the tail by a copy of the one before : 
        graphicsBody[graphicsBody.Count - 1] = newBodyPart;

        //we replace the sprite manager of the tail to : 

        spritsBody[spritsBody.Count - 1] = manageSprit;

        //before adding the tail we put it on the right angle : 

        TailTemp.transform.rotation = Quaternion.Euler(0, 0, TailAngleZ);
        TailTemp.transform.position = MovementTable.instance.getPosition(Mathf.RoundToInt(TailPosition.x), Mathf.RoundToInt(TailPosition.y));

        //we add the tail at the end : 
        graphicsBody.Add(TailTemp);
        spritsBody.Add(TailmanageSprit);



    }

    public bool UseSpriteTurn1(int i)
    {

        float TopY = (TableSize - 1) * cellSize;
        
        //south border case :
        if (spritsBody[i].transform.position.y == 0)
        {
            //if the snake come from the north border : 
            if (spritsBody[i+1].transform.position.y == TopY)
            {
                //if the snake turn left :
                if(spritsBody[i - 1].transform.position.x < spritsBody[i].transform.position.x) 
                {
                    return true;
                }
                //else turn right : 
                return false;
            }

            //if the snake come just from above : 
            else if(spritsBody[i + 1].transform.position.y == cellSize)
            {
                return false;
            }
        }
        //north border case : 
        else if(spritsBody[i].transform.position.y == TopY)
        {
            //if the snake come from the south border : 
            if(spritsBody[i + 1].transform.position.y == 0)
            {
                return false;
            }

            //if the snake come just from under : 
            if(spritsBody[i + 1].transform.position.y == TopY - cellSize)
            {
                return true;
            }
        }

        return true;
    }


    //try to make a function to adapt the turn angle sprite ... :
    public void Turning(int index, float previousAngleZ)
    {
        //we evaluate the difference of angle to represent (the angle of the part is taken before changing it): 
        float angleTunringZ = previousAngleZ - graphicsBody[index].transform.rotation.eulerAngles.z;

     
            
            

        switch(Mathf.RoundToInt(angleTunringZ))
        {
            //no turning : 
            case 0:
                //sprite of the straight body :
                spritsBody[index].changeSprite(SpritesList[1]);
                //Debug.Log(string.Concat("turning = go straight !"));
                break;

                
            case -90:
                //sprite turning 2
                spritsBody[index].changeSprite(SpritesList[3]);
                //Debug.Log(string.Concat("turning = 90° to right!"));
                break;

            case -270:
                //sprite turning 1
                spritsBody[index].changeSprite(SpritesList[2]);
                break;

            case 270:
                //sprite turning 2
                spritsBody[index].changeSprite(SpritesList[3]);
                break;
                
            case 90:
                //sprite turning 1
                spritsBody[index].changeSprite(SpritesList[2]);
                //Debug.Log(string.Concat("turning = -270° to right!"));
                break;
            
        }
    }

    public Vector3 getHeadDirection()
    {
        return this.direction;
    }


}
