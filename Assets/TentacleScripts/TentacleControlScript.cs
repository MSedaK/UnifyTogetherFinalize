using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleControlScript : MonoBehaviour
{
    [SerializeField] List<Tentacle> tentacles;
    [SerializeField] float AttackInterval = 2f;
    public void InitGame()
    {
        InvokeRepeating(nameof(EnableTentacle),0f, AttackInterval);
    }
    private void EnableTentacle()
    {
        Debug.Log("Attack");
        int random = Random.Range(0, tentacles.Count);
        tentacles[random].AttackCommand();
    }

    void Start()
    {
        InitGame();
    }

}



