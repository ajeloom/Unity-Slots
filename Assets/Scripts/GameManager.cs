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

    public int coins = 300;
    public bool isRolling = false;

    private enum Symbol
    {
        cherry,
        watermelon,
        diamond,
        star,
        bar,
        seven
    }

    private enum SlotPosition
    {
        topLeft,
        topMiddle,
        topRight,
        left,
        middle,
        right,
        bottomLeft,
        bottomMiddle,
        bottomRight
    }

    public int scrollSpeed = 20;

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
        nums = new int[transform.childCount];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isRolling)
        {
            if (coins >= 3)
            {
                isRolling = true;
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
            // Debug.Log("nums" + i + ": " + nums[i]);
        }
    }

    public void CheckResult()
    {
        // Three in a row
        if (nums[(int)SlotPosition.left] == nums[(int)SlotPosition.middle]
                && nums[(int)SlotPosition.left] == nums[(int)SlotPosition.right])
        {
            // Debug.Log("Jackpot!");
            switch (nums[(int)SlotPosition.left])
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
