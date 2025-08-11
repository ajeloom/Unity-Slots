using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] private int coins;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetRandomNumbers();
        }
    }

    private void GetRandomNumbers()
    {
        int num1 = UnityEngine.Random.Range(0, 6);
        int num2 = UnityEngine.Random.Range(0, 6);
        int num3 = UnityEngine.Random.Range(0, 6);

        Debug.Log("num1: " + num1 + " num2: " + num2 + " num3: " + num3);
        CheckResult(num1, num2, num3);
    }

    private void CheckResult(int num1, int num2, int num3)
    {
        if (num1 == num2 && num1 == num3)
        {
            Debug.Log("Jackpot!");
        }
    }
}
