using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGravityManager : MonoBehaviour
{
    public bool isShifting { get; set; }
    float shiftTimer =0;
    public Vector3 startPos { get; set; }
    public Vector3 endPos { get; set; }
    [SerializeField] float fallSpeed;
    void Update()
    {
        if (isShifting)
        {
            shiftTimer += Time.deltaTime * fallSpeed;
            FallDown();
        }
    }

    public void FallDown( )
    {
        transform.position = Vector2.Lerp(startPos, endPos, shiftTimer);
        if(transform.position == endPos)
        {
            Debug.Log("GettingHere " + shiftTimer);
            isShifting = false;
            shiftTimer = 0;
        }
    }
    public void ShiftTile(Vector3 firstPosition, Vector3 secondPosition)
    {
        startPos = firstPosition;
        endPos = secondPosition;
        isShifting = true;
    }

       
}
