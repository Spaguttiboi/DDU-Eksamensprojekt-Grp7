using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodSymbolScript : MonoBehaviour
{
    SpriteRenderer moodSpriteRenderer;
    public Sprite[] moodSprites = new Sprite[4];
    private PlayerMovement movementScript;

    bool numb;
    bool angry;
    bool anxious;
    bool fear;

    float duration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = gameObject.GetComponentInParent<PlayerMovement>();
        moodSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        numb = movementScript.numb;
        angry = movementScript.angry;
        anxious = movementScript.anxious;
        fear = movementScript.fear;

        SpriteChanger(numb, angry, anxious, fear, moodSpriteRenderer);
    }

    void SpriteChanger(bool numb, bool angry, bool anxious, bool fear, SpriteRenderer moodSpriteRenderer)
    {
        if (Input.GetKey(KeyCode.Alpha1) && numb == false && movementScript.IsGrounded())
        {
            moodSpriteRenderer.sprite = moodSprites[0];
            StartCoroutine(FadeSpriteOut(moodSpriteRenderer));
        }
        if (Input.GetKey(KeyCode.Alpha2) && angry == false && movementScript.IsGrounded())
        {
            moodSpriteRenderer.sprite = moodSprites[1];
            StartCoroutine(FadeSpriteOut(moodSpriteRenderer));
        }
        if (Input.GetKey(KeyCode.Alpha3) && anxious == false && movementScript.IsGrounded())
        {
            moodSpriteRenderer.sprite = moodSprites[2];
            StartCoroutine(FadeSpriteOut(moodSpriteRenderer));
        }
        if (Input.GetKey(KeyCode.Alpha4) && fear == false && movementScript.IsGrounded())
        {
            moodSpriteRenderer.sprite = moodSprites[3];
            StartCoroutine(FadeSpriteOut(moodSpriteRenderer));
        }
    }

    IEnumerator FadeSpriteOut(SpriteRenderer currentSprite)
    {
        float counter = 0;
        //Get current color
        Color spriteColor = currentSprite.material.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, counter / duration);

            //Change alpha only
            currentSprite.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
    }
}
