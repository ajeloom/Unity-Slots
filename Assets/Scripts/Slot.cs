using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    private Vector3 initialPosition;
    private int loop = 0;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameManager.instance.OnPlay += Event_OnPlay;
    }

    private void Event_OnPlay(object sender, EventArgs e)
    {
        StartCoroutine(SpinAnimation());
    }

    private IEnumerator SpinAnimation()
    {
        if (transform.GetSiblingIndex() == 0
                || transform.GetSiblingIndex() == 3
                || transform.GetSiblingIndex() == 6
                || transform.GetSiblingIndex() == 9)
        {
            yield return new WaitForSecondsRealtime(0.0f);
        }
        else if (transform.GetSiblingIndex() == 1
                || transform.GetSiblingIndex() == 4
                || transform.GetSiblingIndex() == 7
                || transform.GetSiblingIndex() == 10)
        {
            yield return new WaitForSecondsRealtime(0.5f);
        }
        else if (transform.GetSiblingIndex() == 2
                || transform.GetSiblingIndex() == 5
                || transform.GetSiblingIndex() == 8
                || transform.GetSiblingIndex() == 11)
        {
            yield return new WaitForSecondsRealtime(1.0f);
        }

        while (loop < 10)
        {
            Vector3 temp = transform.position;
            for (float i = temp.y; i > -5.0f; i -= Time.deltaTime * GameManager.instance.scrollSpeed)
            {
                transform.position = new Vector3(transform.position.x, i, transform.position.z);
                yield return null;
            }
            transform.position = new Vector3(transform.position.x, 5.0f, transform.position.z);
            spriteRenderer.sprite = sprites[UnityEngine.Random.Range(0, 6)];
            loop++;
        }

        spriteRenderer.sprite = sprites[GameManager.instance.nums[transform.GetSiblingIndex()]];
        for (float i = transform.position.y; i > initialPosition.y; i -= Time.deltaTime * GameManager.instance.scrollSpeed)
        {
            transform.position = new Vector3(transform.position.x, i, transform.position.z);
            yield return null;
        }
        transform.position = initialPosition;
        loop = 0;

        if (transform.GetSiblingIndex() == 2)
        {
            GameManager.instance.isRolling = false;
            GameManager.instance.CheckResult();
        }
    }
}
