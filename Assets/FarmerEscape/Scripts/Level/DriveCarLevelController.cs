using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class DriveCarLevelController : MonoBehaviour
{
    public UnityEvent GameStarted;
    public UnityEvent GameWon;
    public UnityEvent GameLosed;
    public bool IsEnd;
    public bool IsStart;

    public void OnStartGame()
    {
        IsStart = true;
        GameStarted?.Invoke();
    }
    public void OnWinGame()
    {
        IsEnd = true;
        GameWon?.Invoke();
    }
    public void OnLoseGame()
    {
        IsEnd = true;
        GameLosed?.Invoke();
    }
}
