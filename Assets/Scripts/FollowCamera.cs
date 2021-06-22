using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform player_;
    private Vector3 offset_;
    private const float zposition_ = -10.0f;
    private Transform camTransform;

    public Transform Player
    {
        get => player_;
        set => player_ = value;
    }
    void Start()
    {
        camTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        camTransform.position = new Vector3(player_.position.x, player_.position.y, zposition_);
    }
}
