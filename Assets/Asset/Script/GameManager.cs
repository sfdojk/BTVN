using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Card cardPrefab;
    public Transform cardGrid;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public GameObject startScreen;
    public List<Sprite> twelveFaces;
    public bool isChecking;
    public bool isPlaying;
    private int score;
    public float maxTime = 60f;
    private float timeLeft;
    private Card firstCard, secondCard;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        isPlaying = false;
        startScreen.SetActive(true);
    }

    public void StartGame()
    {
        StopAllCoroutines();
        startScreen.SetActive(false);
        ClearCards();
        ResetState();
        SpawnCards();
        isPlaying = true;
    }

    public void EndGame()
    {
        StopAllCoroutines();
        isPlaying = false;
        isChecking = false;
        startScreen.SetActive(true);
    }

    private void Update()
    {
        if (!isPlaying) return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            UpdateTimeText();
            EndGame();
            return;
        }

        UpdateTimeText();
    }

    private void UpdateTimeText()
    {
        int m = Mathf.FloorToInt(timeLeft / 60);
        int s = Mathf.FloorToInt(timeLeft % 60);
        timeText.text = string.Format("{0:00}:{1:00}", m, s);
    }

    public void RegisterCard(Card clickedCard)
    {
        if (firstCard == null)
        {
            firstCard = clickedCard;
            return;
        }

        if (clickedCard == firstCard) return;

        secondCard = clickedCard;
        isChecking = true;
        StartCoroutine(Compare());
    }

    private IEnumerator Compare()
    {
        yield return new WaitForSeconds(0.8f);

        if (firstCard.cardID == secondCard.cardID)
        {
            score += 10;
            scoreText.text = "Điểm: " + score;
            firstCard.Hide();
            secondCard.Hide();

            if (score >= twelveFaces.Count * 10)
            {
                EndGame();
                yield break;
            }
        }
        else
        {
            StartCoroutine(firstCard.FlipAnim(false));
            StartCoroutine(secondCard.FlipAnim(false));
            yield return new WaitForSeconds(0.35f);
        }

        firstCard = null;
        secondCard = null;
        isChecking = false;
    }

    private void ClearCards()
    {
        foreach (Transform child in cardGrid)
        {
            Destroy(child.gameObject);
        }
    }

    private void ResetState()
    {
        score = 0;
        scoreText.text = "Điểm: 0";
        timeLeft = maxTime;
        isChecking = false;
        firstCard = null;
        secondCard = null;
    }

    private void SpawnCards()
    {
        List<int> ids = new List<int>();
        for (int i = 0; i < twelveFaces.Count; i++)
        {
            ids.Add(i);
            ids.Add(i);
        }

        for (int i = 0; i < ids.Count; i++)
        {
            int randomIndex = Random.Range(i, ids.Count);
            int temp = ids[i];
            ids[i] = ids[randomIndex];
            ids[randomIndex] = temp;
        }

        foreach (int id in ids)
        {
            Card newCard = Instantiate(cardPrefab, cardGrid);
            newCard.Setup(id, twelveFaces[id]);
        }
    }
}