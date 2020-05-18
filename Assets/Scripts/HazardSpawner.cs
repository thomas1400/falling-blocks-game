using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class HazardSpawner : MonoBehaviour
{

    public GameObject hazard;
    public Vector2 gapTimeMinMax = new Vector2(0.25f, 1.0f);
    public float gapTimeVariability = 0.2f;
    public Vector2 scaleMinMax = new Vector2(0.75f, 2f);
    
    private float gapTime;
    private float lastSpawnTime;
    private float screenHalfHeight;
    private float screenHalfWidth;

    void Start()
    {
        SetNewGapTime();

        if (Camera.main == null) return;
        
        Camera main = Camera.main;
        screenHalfHeight = main.orthographicSize;
        screenHalfWidth = screenHalfHeight * main.aspect;
    }

    private void SetNewGapTime()
    {
        gapTime = Mathf.Lerp(gapTimeMinMax[1], gapTimeMinMax[0], Difficulty.GetDifficultyPercent())
            + Random.Range(-1, 1) * gapTimeVariability;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawnTime >= gapTime)
        {
            SpawnHazard();
            lastSpawnTime = Time.time;
            SetNewGapTime();
        }
    }

    private void SpawnHazard()
    { 
        float scale = Random.Range(scaleMinMax[0], scaleMinMax[1]); 
        Quaternion rotation = Random.rotationUniform; 
        Vector2 position = new Vector2(
            Random.Range(-screenHalfWidth + scale/2.0f, screenHalfWidth - scale/2.0f), 
            screenHalfHeight + scale); 
        
        GameObject newHazard = Instantiate(hazard, position, rotation, transform); 
        newHazard.transform.localScale = Vector3.one * scale;
        
        Rigidbody rb = newHazard.GetComponent<Rigidbody>();
        if (rb == null) return;
        rb.angularVelocity = new Vector3(Random.value, Random.value, Random.value);
    }
}
