using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public enum STATE
    {
        IDLE,
        AGGRO
    }

    protected STATE state = STATE.IDLE;
    protected float stateTime = 0.0f;

    void Start()
    {
        OnInit();
    }
    void FixedUpdate()
    {
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

    protected virtual void OnInit()
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

    protected float GetDistanceBetweenMeAndPlayer()
    {
        float distance = 0.0f;

        Vector2 playerPos = GetPlayerCoordinate();
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        distance = Vector2.Distance(pos, playerPos);

        return distance;
    }
    protected Vector2 GetPlayerCoordinate()
    {
        Vector2 playerCoord = Vector2.zero;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player)
        {
            playerCoord = new Vector2(player.transform.position.x, player.transform.position.z);
        }

        return playerCoord;
    }
}
