﻿using UnityEngine;
using System.Collections;
using System;

public class wander : state {
    private Enemy enemy;
    public LayerMask enemyMask;
    public float speed = 5;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth, myHeight;
    private float time=0;

    public void Execute()
    {   
        time += Time.deltaTime;
        if (time >= 5)
            enemy.changestate(new idle());
        //check to see if there's ground in front of us before moving forward
        Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight;
        lineCastPos.y = lineCastPos.y - (myHeight * 1.2f);
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * 0.05f);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * 0.05f, enemyMask);
        //if no ground turn around
        if (!isGrounded || isBlocked)
        {
            Vector3 currentRot = myTrans.eulerAngles;
            currentRot.y += 180;
            myTrans.eulerAngles = currentRot;
        }
        //always move forward
        Vector2 myVel = myBody.velocity;
        myVel.x = -myTrans.right.x * speed;
        myBody.velocity = myVel;
    }

    public void Enter(Enemy enemy)
    {
        Debug.Log("Wander");
        time = 0;
        this.enemy = enemy;
        myTrans = enemy.transform;
        myBody = enemy.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = enemy.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
    }

    public void Exit()
    {
    }

    public void onTriggerEnter(Collider2D other)
    {
    }
}