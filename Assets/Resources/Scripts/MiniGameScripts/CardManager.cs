using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using Naninovel;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    [SerializeField] private GameObject gameCompleted;
    [SerializeField] private GameObject canvasMiniGame;

    private CardView[] allCards;
    private List<CardView> flippedCards = new();
    private List<CardView> matchedCards = new();
    private bool isAnimating;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        allCards = FindObjectsOfType<CardView>(true);

        List<string> baseNames = new List<string> { "CardBanana", "CardGirl", "CardHeart" };
        List<string> deck = baseNames.SelectMany(name => Enumerable.Repeat(name, 4)).ToList();
        deck = deck.OrderBy(_ => Random.value).ToList();

        for (int i = 0; i < allCards.Length; i++)
        {
            allCards[i].AssignCard(deck[i]);
            allCards[i].OnCardFlipped += HandleCardFlipped;
        }
    }
    private void Update()
    {
        CheckGameComplete();
    }

    private void HandleCardFlipped(CardView card)
    {
        if (isAnimating || flippedCards.Contains(card) || matchedCards.Contains(card)) return;

        flippedCards.Add(card);
        Debug.Log(flippedCards.Count);

        if (flippedCards.Count == 2)
        {
            DisableAllCards();
            StartCoroutine(CompareCards(flippedCards[0], flippedCards[1]));
        }
    }

    private void DisableAllCards()
    {
        foreach (var card in allCards)
        {
            card.button.interactable = false;
        }
    }

    private void EnableCardsExceptFlipped()
    {
        foreach (var card in allCards)
        {
            if (card.backSide.enabled)
            {
                card.button.interactable = true;
            }
           
        }
    }

    private IEnumerator CompareCards(CardView first, CardView second)
    {
        isAnimating = true;
        yield return new WaitForSeconds(1f);

        if (first.CardName == second.CardName)
        {
            matchedCards.Add(first);
            matchedCards.Add(second);
            flippedCards.Clear();
        }
        else
        {
            yield return StartCoroutine(FlipBackCards(first, second));
            flippedCards.Clear();
        }
        Debug.Log(matchedCards.Count);

        isAnimating = false;
        EnableCardsExceptFlipped();
    }


    private IEnumerator FlipBackCards(CardView a, CardView b)
    {
        a.FlipBack();
        b.FlipBack();

        while (a.IsAnimating || b.IsAnimating)
            yield return null;
    }

    
    private void CheckGameComplete()
    {
        if (matchedCards.Count == allCards.Length)
        {
            gameCompleted.SetActive(true);
            StartCoroutine(LoadNextLevel());
        }
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1f);
        var task = PlayScriptAsync("room2", "GameEnd");
        canvasMiniGame.SetActive(false);
        while (!task.IsCompleted) yield return null;
    }
    public void startMiniGame()
    {
        var task = PlayScriptAsync("room2", "GameStart");
    }
    private async Task PlayScriptAsync(string lvl, string lbl)
    {
        var player = Engine.GetService<IScriptPlayer>();
        await player.PreloadAndPlayAsync(lvl, lbl);
    }
}
