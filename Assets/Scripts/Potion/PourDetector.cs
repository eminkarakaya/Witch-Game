using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class PourDetector : MonoBehaviour
{
    AudioSource source;
    PotionMovement movement;
    private int pourThreshold = 10;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    private Stream currentStream = null;
    private bool isPouring = false;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = true;
        movement = FindObjectOfType<PotionMovement>();
        if(movement != null && movement.clip != null)
            source.clip = movement.clip;
    }
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
        if(source.clip != null)
            source.Play();
        currentStream = CreateStream();
        currentStream.Begin(GetComponent<Potion>());
    }

    private void EndPour()
    {
        if (source.clip != null)
            source.Stop ();
        currentStream.End();
        currentStream = null;
    }

    private float CalculatePourAngle()
    {
        return transform.forward.y * Mathf.Rad2Deg;
    } 
    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity,transform);
        streamObject.GetComponent<Stream>().lineRenderer.material.color = GetComponent<Potion>().color;

        return streamObject.GetComponent<Stream>();
    }
}

