using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    
    public Vector2 speedMinMax = new Vector2(10.0f, 13.0f);
    public float speedVariability = 0.25f;
    public float horizontalSpeedVariability = 1.0f;

    private float screenHalfHeight;
    private Vector3 velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        float speed = Mathf.Lerp(speedMinMax[0], speedMinMax[1], Difficulty.GetDifficultyPercent()) 
                      + (2*Random.value-1) * speedVariability;
        Vector2 xVelocity = Vector2.right * (2 * Random.value - 1) * horizontalSpeedVariability;
        Vector2 yVelocity = Vector2.down * (speed * (1 + (1 - Random.value * speedVariability)));
        velocity = xVelocity + yVelocity;
        
        if (Camera.main != null)
        {
            screenHalfHeight = Camera.main.orthographicSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;

        DestroyIfOffScreen();
    }

    void DestroyIfOffScreen()
    {
        if (transform.position.y < -(screenHalfHeight + transform.localScale.y))
        {
            Destroy(gameObject);
        }
    }
}
