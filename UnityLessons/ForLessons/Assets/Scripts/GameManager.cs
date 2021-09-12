using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int TotalScore;
    
    public void AddScore(int Count)
    {
        TotalScore += Count;
        Debug.Log("»тоговый счЄт: " + TotalScore);
    }
}
