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

    public enum SlotPosition
    {
        topLeft,
        topMiddle,
        topRight,
        left,
        middle,
        right,
        bottomLeft,
        bottomMiddle,
        bottomRight,
        outerLeft,
        outerMiddle,
        outerRight
    }

    public int scrollSpeed = 20;

    private enum GameType
    {
        oneCoin = 1,
        twoCoin = 2,
        threeCoin = 3
    }

    private GameType gameType = GameType.oneCoin;

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
    // void Update()
    // {

    // }

    public void PlayOneCoin()
    {
        if (coins >= 1 && !isRolling)
        {
            isRolling = true;
            gameType = GameType.oneCoin;
            coins -= 1;
            GetRandomNumbers();
            OnPlay?.Invoke(this, EventArgs.Empty);
        }
    }

    public void PlayTwoCoins()
    {
        if (coins >= 2 && !isRolling)
        {
            isRolling = true;
            gameType = GameType.twoCoin;
            coins -= 2;
            GetRandomNumbers();
            OnPlay?.Invoke(this, EventArgs.Empty);
        }
    }

    public void PlayThreeCoins()
    {
        if (coins >= 3 && !isRolling)
        {
            isRolling = true;
            gameType = GameType.threeCoin;
            coins -= 3;
            GetRandomNumbers();
            OnPlay?.Invoke(this, EventArgs.Empty);
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
        if (gameType == GameType.oneCoin)
        {
            // Check if the middle has three matching
            CheckThreeMatching(nums[(int)SlotPosition.left], nums[(int)SlotPosition.middle], nums[(int)SlotPosition.right], (int)GameType.oneCoin);
        }
        else if (gameType == GameType.twoCoin)
        {
            // Check if each row has three matching
            CheckThreeMatching(nums[(int)SlotPosition.topLeft], nums[(int)SlotPosition.topMiddle], nums[(int)SlotPosition.topRight], (int)GameType.twoCoin);
            CheckThreeMatching(nums[(int)SlotPosition.left], nums[(int)SlotPosition.middle], nums[(int)SlotPosition.right], (int)GameType.oneCoin);
            CheckThreeMatching(nums[(int)SlotPosition.bottomLeft], nums[(int)SlotPosition.bottomMiddle], nums[(int)SlotPosition.bottomRight], (int)GameType.twoCoin);
        }
        else if (gameType == GameType.threeCoin)
        {
            // Check if each row has three matching
            CheckThreeMatching(nums[(int)SlotPosition.topLeft], nums[(int)SlotPosition.topMiddle], nums[(int)SlotPosition.topRight], (int)GameType.twoCoin);
            CheckThreeMatching(nums[(int)SlotPosition.left], nums[(int)SlotPosition.middle], nums[(int)SlotPosition.right], (int)GameType.oneCoin);
            CheckThreeMatching(nums[(int)SlotPosition.bottomLeft], nums[(int)SlotPosition.bottomMiddle], nums[(int)SlotPosition.bottomRight], (int)GameType.twoCoin);

            // Check if the two diagonals has three matching
            CheckThreeMatching(nums[(int)SlotPosition.topLeft], nums[(int)SlotPosition.middle], nums[(int)SlotPosition.bottomRight], (int)GameType.threeCoin);
            CheckThreeMatching(nums[(int)SlotPosition.bottomLeft], nums[(int)SlotPosition.middle], nums[(int)SlotPosition.topRight], (int)GameType.threeCoin);
        }
    }

    private void CheckThreeMatching(int num1, int num2, int num3, int multiplier)
    {
        if (num1 == num2
                && num1 == num3)
        {
            switch (num1)
            {
                case (int)Symbol.cherry:
                    coins += 3 * multiplier;
                    break;
                case (int)Symbol.watermelon:
                    coins += 6 * multiplier;
                    break;
                case (int)Symbol.diamond:
                    coins += 12 * multiplier;
                    break;
                case (int)Symbol.star:
                    coins += 25 * multiplier;
                    break;
                case (int)Symbol.bar:
                    coins += 50 * multiplier;
                    break;
                case (int)Symbol.seven:
                    coins += 100 * multiplier;
                    break;
            }

            OnJackpot?.Invoke(this, EventArgs.Empty);
        }
    }
}
