using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomType : MonoBehaviour
{
    public int RoomType;
    public string openType;

    public void destroyRoom()
    {
        Destroy(gameObject);
    }
}
