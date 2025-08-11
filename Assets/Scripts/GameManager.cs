using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public event EventHandler OnPlay;
    public event EventHandler OnJackpot;

    public int[] nums;

    [SerializeField] public int coins;

    private enum Symbol
    {
        cherry,
        watermelon,
        diamond,
        star,
        bar,
        seven
    }

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
        nums = new int[3];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (coins >= 3)
            {
                coins -= 3;
                GetRandomNumbers();
                OnPlay?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                Debug.Log("Not enough coins");
            }
        }
    }

    private void GetRandomNumbers()
    {
        for (int i = 0; i < nums.Length; i++)
        {
            nums[i] = UnityEngine.Random.Range(0, 6);
        }

        // Debug.Log("num1: " + nums[0] + " num2: " + nums[1] + " num3: " + nums[2]);
        CheckResult(nums[0], nums[1], nums[2]);
    }

    private void CheckResult(int num1, int num2, int num3)
    {
        // Three in a row
        if (num1 == num2 && num1 == num3)
        {
            // Debug.Log("Jackpot!");
            switch (num1)
            {
                case (int)Symbol.cherry:
                    coins += 6;
                    break;
                case (int)Symbol.watermelon:
                    coins += 12;
                    break;
                case (int)Symbol.diamond:
                    coins += 25;
                    break;
                case (int)Symbol.star:
                    coins += 50;
                    break;
                case (int)Symbol.bar:
                    coins += 100;
                    break;
                case (int)Symbol.seven:
                    coins += 300;
                    break;
            }

            OnJackpot?.Invoke(this, EventArgs.Empty);
        }
    }
}
