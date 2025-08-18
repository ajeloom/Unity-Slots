using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        tmp = transform.Find("CoinsText").GetComponent<TextMeshProUGUI>();
        tmp.text = GameManager.instance.coins.ToString();
        GameManager.instance.OnPlay += Event_OnPlay;
        GameManager.instance.OnJackpot += Event_OnJackpot;
    }

    private void Event_OnPlay(object sender, EventArgs e)
    {
        tmp.text = GameManager.instance.coins.ToString();
        transform.Find("Jackpot").gameObject.SetActive(false);
        StopAllCoroutines();
    }

    private void Event_OnJackpot(object sender, EventArgs e)
    {
        // Update coin counter
        tmp.text = GameManager.instance.coins.ToString();

        transform.Find("Jackpot").gameObject.SetActive(true);
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        GameObject obj = transform.Find("Jackpot").gameObject;

        while (true)
        {
            obj.SetActive(true);

            yield return new WaitForSecondsRealtime(0.5f);

            obj.SetActive(false);

            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
}
