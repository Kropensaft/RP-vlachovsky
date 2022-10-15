using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMovements : MonoBehaviour
{
    private Transform _transform;

    public Vector3 NewPosition;
    public Vector3 FinalPosition;
    [Range(0f, 25f)]
    public float LerpSpeed = 1f;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        NewPosition = _transform.position;
        FinalPosition = NewPosition;
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            NewPosition.x += Time.deltaTime * LerpSpeed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            NewPosition.x -= Time.deltaTime * LerpSpeed;
        }

        //FinalPosition.x = Mathf.Floor (NewPosition.x * 32f) / 32f;
        FinalPosition.x = NewPosition.x;

        _transform.position = FinalPosition;
    }
}
