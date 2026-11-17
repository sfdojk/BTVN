using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Card : MonoBehaviour
{
    public int cardID;
    public GameObject backFaceObj;
    public GameObject frontFaceObj;
    public Image iconImage;

    public bool isFlipped;
    public bool isMatched;

    public void Setup(int id, Sprite logoSprite)
    {
        cardID = id;
        iconImage.sprite = logoSprite;
        backFaceObj.SetActive(true);
        frontFaceObj.SetActive(false);
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (!GameManager.Instance.isPlaying || isFlipped || isMatched || GameManager.Instance.isChecking) return;

        isFlipped = true;
        GameManager.Instance.RegisterCard(this);
        StartCoroutine(FlipAnim(true));
    }

    public IEnumerator FlipAnim(bool toFront)
    {
        for (float t = 0; t < 0.15f; t += Time.deltaTime)
        {
            transform.localScale = new Vector3(Mathf.Lerp(1, 0, t / 0.15f), 1, 1);
            yield return null;
        }

        backFaceObj.SetActive(!toFront);
        frontFaceObj.SetActive(toFront);
        isFlipped = toFront;

        for (float t = 0; t < 0.15f; t += Time.deltaTime)
        {
            transform.localScale = new Vector3(Mathf.Lerp(0, 1, t / 0.15f), 1, 1);
            yield return null;
        }
        transform.localScale = Vector3.one;
    }

    public void Hide()
    {
        isMatched = true;
        backFaceObj.SetActive(false);
        frontFaceObj.SetActive(false);
        GetComponent<Button>().enabled = false;
        if (TryGetComponent(out Image img)) img.enabled = false;
    }
}