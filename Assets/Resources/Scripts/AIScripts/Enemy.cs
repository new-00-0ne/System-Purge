﻿using UnityEngine;
using System.Collections;
using GameUtils;
using System;

public class Enemy : Controller{
    public bool isAir;
    private state currentState;
	private Animator mAnimator;
    public LayerMask enemyMask;
    public bool rightdirection;
    public float[] time = new float[10];
	public bool idle;
	public bool wander;

    // Use this for initialization
    void Start ()
    {
		time [0] = 2.0f;
		time [1] = 4.0f;
        print("start");
		mAnimator = GetComponent<Animator> ();
        changestate(new idle());
	}
	// Update is called once per frame
	void Update ()
    {
		mAnimator.SetFloat ("Speed", GetComponent<Rigidbody2D> ().velocity.magnitude);
		//mAnimator.SetFloat ("Player Health", GameObject.FindWithTag ("Scene_Object").GetComponent<Rigidbody2D>());
        currentState.Execute();
	}
    public void changestate(state newstate)
    {
        if(currentState != null)
            currentState.Exit();
        currentState = newstate;
        currentState.Enter(this);   
    }
}
