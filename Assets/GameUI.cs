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
        tmp.text = "Coins: " + GameManager.instance.coins.ToString();
        GameManager.instance.OnPlay += Event_OnPlay;
        GameManager.instance.OnJackpot += Event_OnJackpot;
    }

    private void Event_OnPlay(object sender, EventArgs e)
    {
        tmp.text = "Coins: " + GameManager.instance.coins.ToString();
    }

    private void Event_OnJackpot(object sender, EventArgs e)
    {
        // Update coin counter
        tmp.text = "Coins: " + GameManager.instance.coins.ToString();
        
        transform.Find("JackpotText").gameObject.SetActive(true);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        TextMeshProUGUI text = transform.Find("JackpotText").GetComponent<TextMeshProUGUI>();

        for (float i = 1; i > 0; i -= Time.deltaTime / 2)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, i);
            yield return null;
        }
        
        transform.Find("JackpotText").gameObject.SetActive(false);
    }
}
