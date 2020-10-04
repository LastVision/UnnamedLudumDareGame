﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public enum STATE
    {
        IDLE,
        AGGRO,
        DEAD
    }

    protected STATE state = STATE.IDLE;
    protected float stateTime = 0.0f;
    private float despawnTime = 5.0f;

    public AudioClip idleSound;
    public AudioClip aggroSound;
    void Start()
    {
        OnInit();
    }
    void FixedUpdate()
    {
        if(state == STATE.DEAD)
        {
            float currentTime = (Time.fixedTime - stateTime);
            if(currentTime > despawnTime)
            {
                Destroy(gameObject);
            }
            return;
        }
        switch (state)
        {
            case STATE.IDLE:
            {
                OnUpdateIdleState();
                break;
            }
            case STATE.AGGRO:
            {
                OnUpdateAggroState();
                break;
            }
            default:
            {
                Debug.Log("Error: the state "+ state.ToString() + " is not a valid state");
                break;
            }
        }
    }

    public void Kill()
    {
        ChangeState(STATE.DEAD, Time.fixedTime);
        OnDeath();
    }

    protected virtual void OnInit()
    {

    }

    protected virtual void OnDeath()
    {

    }
    protected virtual void OnUpdateIdleState()
    {

    }

    protected virtual void OnUpdateAggroState()
    {

    }

    protected virtual void OnPreStateChanged(STATE newState, float newStateTime)
    {

    }

    protected virtual void OnPostStateChanged(STATE newState, float newStateTime)
    {

    }

    protected void ChangeState(STATE newState, float newStateTime)
    {
        if(state != newState)
        {
            OnPreStateChanged(newState, newStateTime);
            state = newState;
            stateTime = newStateTime;
            OnPostStateChanged(newState, newStateTime);
        }
    }

    protected float GetPlayerDist()
    {
        float distance = 0.0f;

        Vector3 playerPos = GetPlayerCoordinate();
        Vector3 pos = transform.position;

        distance = Vector3.Distance(pos, playerPos);

        return distance;
    }
    protected Vector3 GetPlayerCoordinate()
    {
        Vector3 playerCoord = Vector3.zero;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player)
        {
            playerCoord = player.transform.position;
            playerCoord.y -= 1.5f;
        }

        return playerCoord;
    }
}
