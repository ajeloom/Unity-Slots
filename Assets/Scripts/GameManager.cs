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

    public void PlayOneCoin()
    {
        if (coins >= 1 && !isRolling)
        {
            isRolling = true;
            gameType = GameType.oneCoin;
            coins -= 1;
            HideMatchingLines();
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
            HideMatchingLines();
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
            HideMatchingLines();
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
        bool isMiddleMatching = CheckThreeMatching(nums[(int)SlotPosition.left], nums[(int)SlotPosition.middle], nums[(int)SlotPosition.right], (int)GameType.oneCoin);

        if (isMiddleMatching)
        {
            transform.Find("Line_Middle").gameObject.SetActive(true);
        }

        if (gameType == GameType.oneCoin)
        {
            return;
        }

        bool isTopMatching = CheckThreeMatching(nums[(int)SlotPosition.topLeft], nums[(int)SlotPosition.topMiddle], nums[(int)SlotPosition.topRight], (int)GameType.twoCoin);
        bool isBottomMatching = CheckThreeMatching(nums[(int)SlotPosition.bottomLeft], nums[(int)SlotPosition.bottomMiddle], nums[(int)SlotPosition.bottomRight], (int)GameType.twoCoin);

        if (isTopMatching)
        {
            transform.Find("Line_Top").gameObject.SetActive(true);
        }

        if (isBottomMatching)
        {
            transform.Find("Line_Bottom").gameObject.SetActive(true);
        }

        if (gameType == GameType.twoCoin)
        {
            return;
        }

        bool isDiagonalMatching = CheckThreeMatching(nums[(int)SlotPosition.topLeft], nums[(int)SlotPosition.middle], nums[(int)SlotPosition.bottomRight], (int)GameType.threeCoin);
        bool isDiagonalMatching2 = CheckThreeMatching(nums[(int)SlotPosition.bottomLeft], nums[(int)SlotPosition.middle], nums[(int)SlotPosition.topRight], (int)GameType.threeCoin);

        if (isDiagonalMatching)
        {
            transform.Find("Line_Diagonal1").gameObject.SetActive(true);
        }

        if (isDiagonalMatching2)
        {
            transform.Find("Line_Diagonal2").gameObject.SetActive(true);
        }
    }

    private bool CheckThreeMatching(int num1, int num2, int num3, int multiplier)
    {
        if (num1 == num2 && num1 == num3)
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
            return true;
        }

        return false;
    }

    private void HideMatchingLines()
    {
        transform.Find("Line_Middle").gameObject.SetActive(false);
        transform.Find("Line_Top").gameObject.SetActive(false);
        transform.Find("Line_Bottom").gameObject.SetActive(false);
        transform.Find("Line_Diagonal1").gameObject.SetActive(false);
        transform.Find("Line_Diagonal2").gameObject.SetActive(false);
    }
}
