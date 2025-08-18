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
        if (transform.GetSiblingIndex() == (int)GameManager.SlotPosition.topLeft
                || transform.GetSiblingIndex() == (int)GameManager.SlotPosition.left
                || transform.GetSiblingIndex() == (int)GameManager.SlotPosition.bottomLeft
                || transform.GetSiblingIndex() == (int)GameManager.SlotPosition.outerLeft)
        {
            yield return new WaitForSecondsRealtime(0.0f);
        }
        else if (transform.GetSiblingIndex() == (int)GameManager.SlotPosition.topMiddle
                || transform.GetSiblingIndex() == (int)GameManager.SlotPosition.middle
                || transform.GetSiblingIndex() == (int)GameManager.SlotPosition.bottomMiddle
                || transform.GetSiblingIndex() == (int)GameManager.SlotPosition.outerMiddle)
        {
            yield return new WaitForSecondsRealtime(0.5f);
        }
        else if (transform.GetSiblingIndex() == (int)GameManager.SlotPosition.topRight
                || transform.GetSiblingIndex() == (int)GameManager.SlotPosition.right
                || transform.GetSiblingIndex() == (int)GameManager.SlotPosition.bottomRight
                || transform.GetSiblingIndex() == (int)GameManager.SlotPosition.outerRight)
        {
            yield return new WaitForSecondsRealtime(1.0f);
        }

        while (loop < 10)
        {
            Vector3 temp = transform.position;
            for (float i = temp.y; i > -4.0f; i -= Time.deltaTime * GameManager.instance.scrollSpeed)
            {
                transform.position = new Vector3(transform.position.x, i, transform.position.z);
                yield return null;
            }
            transform.position = new Vector3(transform.position.x, 4.0f, transform.position.z);
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
