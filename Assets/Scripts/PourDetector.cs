using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    private Stream currentStream = null;
    private bool isPouring = false;
    private void Update()
    {
        bool pourCheck = CalculatePourAngle() < pourThreshold;
        if(isPouring != pourCheck)
        {
            isPouring = pourCheck;
            if(isPouring)
            {
                StartPour();
            }
            else
                EndPour();
        }
    }

    private void StartPour()
    {
        Debug.Log("START");
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    {
        currentStream.End();
        currentStream = null;
        Debug.Log("END");
    }

    private float CalculatePourAngle()
    {
        return transform.forward.y * Mathf.Rad2Deg;
    } 
    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity,transform);
        return streamObject.GetComponent<Stream>();
    }
}

