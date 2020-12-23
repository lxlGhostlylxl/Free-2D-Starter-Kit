using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelGen : MonoBehaviour
{
    public bool UpToDown;
    public bool LeftToRight;
    bool maxHeight;
    bool minHeight;
    public Transform[] topToBottomStartPos;
    public Transform[] leftRightStartPos;
    public GameObject[] Rooms; // index 0 --> LR index 1 -->LRB index 2 --> LRT index 3 --> LRTB
    int randStartingPos;
    int direction;
    int moveAmount = 20;
    int randomRoom;
    float timeBtwRooms;
    float startTimeBtwRooms = .25f;
    public int minX, maxX, minY, maxY;
    public bool madeExit;
    public GameObject startRoom;
    public GameObject exitRoom;
    public LayerMask room;
    int downCounter;
    int upCounter;
    public bool canSpawnBossRoom;


    void Start()
    {
        if (UpToDown)
        {
            randStartingPos = Random.Range(0, topToBottomStartPos.Length);
            transform.position = topToBottomStartPos[randStartingPos].position;
            Instantiate(startRoom, transform.position, transform.rotation);


            if (transform.position.x > minX && transform.position.x < maxX)
            {
                direction = Random.Range(1, 7);
            }
            else if (transform.position.x < minX)
            {
                direction = 2;
            }
            else
            {
                direction = 4;
            }
        }
        if (LeftToRight)
        {
           
                randStartingPos = Random.Range(0, leftRightStartPos.Length);
                transform.position = leftRightStartPos[randStartingPos].position;
                Instantiate(startRoom, transform.position, transform.rotation);


                if (transform.position.x > minY && transform.position.x < maxY)
                {
                    direction = Random.Range(1, 7);
                }
                else if (transform.position.x < minY)
                {
                    direction = 7;
                }
                else
                {
                    direction = 1;
                }
            }


    }

  
    void Update()
    {
        if(transform.position.y >= maxY)
        {
            maxHeight = true;
        }
        else
        {
            maxHeight = false;
        }

        if (transform.position.y <= minY)
        {
            minHeight = true;
        }
        else
        {
            minHeight = false;
        }


        if (!madeExit && timeBtwRooms <= 0)
        {
            if (UpToDown)
            {
                moveDownWard();
            }
            if (LeftToRight)
            {
                moveRightWard();
            }
            
            timeBtwRooms = startTimeBtwRooms;

        }
        else if (timeBtwRooms >= 0)
        {
            timeBtwRooms -= Time.deltaTime;
        }
            
    }

    void moveDownWard()
    {
        if(direction == 1 || direction == 2 || direction == 3) // move right
        {
            
            if(transform.position.x < maxX)
            {

                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;
              
                


                direction = Random.Range(1, 8);
                if(direction == 4)
                {
                    direction = 2;
                } else if (direction == 6)
                {
                    direction = 7;
                } else if (direction == 5)
                {
                    direction = 2;
                }
            }
            else
            {
                direction = 7;
            }
          
        }
        else if (direction == 4 || direction == 5 || direction == 6) // move left
        {
           
            if (transform.position.x > minX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;
                randomRoom = Random.Range(0, Rooms.Length);
                Instantiate(Rooms[randomRoom], transform.position, Quaternion.identity);

                direction = Random.Range(4, 8);
            
            }
            else
            {
                direction = 7;
            }
           
        }
        else if (direction == 7) // move down
        {
            downCounter++;

            Collider2D roomDection = Physics2D.OverlapCircle(transform.position, 1, room);
            if (transform.position.y > minY)
            {

                if(roomDection.GetComponent<roomType>().RoomType != 1 || roomDection.GetComponent<roomType>().RoomType != 3)
                {
                    if(downCounter >= 2)
                    {
                        roomDection.GetComponent<roomType>().destroyRoom();
                        Instantiate(Rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomDection.GetComponent<roomType>().destroyRoom();

                        int randomBottomRoom = Random.Range(1, 4);

                        if (randomBottomRoom == 2)
                        {
                            randomBottomRoom = 1;
                        }

                        Instantiate(Rooms[randomBottomRoom], transform.position, Quaternion.identity);
                    }
                  
                }
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - (moveAmount/2));
                transform.position = newPos;

                randomRoom = Random.Range(2, 4);
              
                Instantiate(Rooms[randomRoom], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else if(transform.position.y <= minY)
            {
               
                roomDection.GetComponent<roomType>().destroyRoom();
                print(roomDection);
                Instantiate(exitRoom, transform.position, Quaternion.identity);
                madeExit = true;
            }
            
        }
   
    }

    void moveRightWard()
    {

        if (direction == 1 || direction == 2 || direction == 3) // move right
        {

            if (transform.position.x < maxX)
            {
                upCounter = 0;
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;
            
                if (!maxHeight)
                {
                    randomRoom = Random.Range(0, Rooms.Length);
                    Instantiate(Rooms[randomRoom], transform.position, Quaternion.identity);
                }
                else
                {
                    int newRand = Random.Range(0, 2);
                    if (newRand == 0)
                    {
                        Instantiate(Rooms[0], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Rooms[1], transform.position, Quaternion.identity);
                    }
                }

                direction = Random.Range(1, 8);
            }

            else if (transform.position.x >= maxX)
            {
                Collider2D roomDection = Physics2D.OverlapCircle(transform.position, 1, room);
                roomDection.GetComponent<roomType>().destroyRoom();
                print(roomDection);
                Instantiate(exitRoom, transform.position, Quaternion.identity);
                madeExit = true;
            }

        }
  

        

        else if (direction == 4 || direction == 5 || direction == 6) // move Up
        {

            if (transform.position.y < maxY && upCounter == 0 && downCounter == 0)
            {
                upCounter++;
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y + (moveAmount / 2));
                transform.position = newPos;
                randomRoom = Random.Range(0, Rooms.Length);
                Instantiate(Rooms[randomRoom], transform.position, Quaternion.identity);

                direction = Random.Range(1, 7);

            }
            else
            {
                direction = 1;
            }

        }
        else if (direction == 7 && downCounter == 0 && upCounter == 0) // move down
        {
            upCounter = 0;
            downCounter++;
            //totalDownCounter++;
            Collider2D roomDection = Physics2D.OverlapCircle(transform.position, 1, room);
            if (transform.position.y > minY)
            {

                if (roomDection.GetComponent<roomType>().RoomType != 1 || roomDection.GetComponent<roomType>().RoomType != 3)
                {
                    if (downCounter >= 2)
                    {
                        roomDection.GetComponent<roomType>().destroyRoom();
                        Instantiate(Rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomDection.GetComponent<roomType>().destroyRoom();

                        int randomBottomRoom = Random.Range(1, 4);

                        if (randomBottomRoom == 2)
                        {
                            randomBottomRoom = 1;
                        }

                        Instantiate(Rooms[randomBottomRoom], transform.position, Quaternion.identity);
                    }

                }
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - (moveAmount / 2));
                transform.position = newPos;

                randomRoom = Random.Range(2, 4);

                Instantiate(Rooms[randomRoom], transform.position, Quaternion.identity);

                direction = Random.Range(1, 3);
            }
            else
            {
                direction = 1;
            }

        }

    }

}
