using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    public float speed = 10;

    private float screenHalfWidthInWorldUnits = 0;
    public event Action OnPlayerDeath;
    
    // Start is called before the first frame update
    void Start()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            float halfPlayerWidth = transform.localScale.x / 2.0f;
            screenHalfWidthInWorldUnits = mainCamera.aspect * mainCamera.orthographicSize + halfPlayerWidth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Wrap();

    }

    void Move()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(input.normalized * (speed * Time.deltaTime));
    }

    void Wrap()
    {
        Vector2 position = transform.position;
        if (position.x > screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(-screenHalfWidthInWorldUnits, position.y);
        }
        else if (position.x < -screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(screenHalfWidthInWorldUnits, position.y);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            OnPlayerDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
