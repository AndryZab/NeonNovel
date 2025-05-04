using Naninovel.FX;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Naninovel;
using System.Threading.Tasks;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    private string cardBanana = "cardBanana";
    private string cardGirl = "cardGirl";
    private string cardHeart = "cardHeart";

    private int cardNameIndex = 0;
    private int countIsActivedCards = 0;
    private CardView[] cardView;
    private bool IsAnim = false;
    public GameObject GameCompleted;
    public GameObject CanvasMiniGame;

    public void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        cardView = FindObjectsOfType<CardView>();

        List<string> cardNames = new List<string> { "cardBanana", "cardGirl", "cardHeart" };

        List<string> shuffledCardNames = new List<string>();
        foreach (var name in cardNames)
        {
            for (int i = 0; i < 4; i++)
            {
                shuffledCardNames.Add(name);
            }
        }

        shuffledCardNames = shuffledCardNames.OrderBy(x => Random.Range(0, 100)).ToList();

        int index = 0;
        foreach (CardView cardViewItem in cardView)
        {
            if (index < shuffledCardNames.Count)
            {
                cardViewItem.cardName = shuffledCardNames[index];
                index++;
            }
        }
    }

    private List<CardView> matchedCards = new List<CardView>(); 

    private void Update()
    {
        CardsPlay();
        GameEnded();
    }
    private void CardsPlay()
    {
        CardView firstCard = null;
        CardView secondCard = null;

        for (int i = 0; i < cardView.Length; i++)
        {
            if (!cardView[i].backSide.enabled && !matchedCards.Contains(cardView[i]))
            {
                if (firstCard == null)
                {
                    firstCard = cardView[i];
                }
                else if (secondCard == null && cardView[i] != firstCard)
                {
                    secondCard = cardView[i];
                }
            }
        }

        if (firstCard != null && secondCard != null)
        {
            if (firstCard.cardName != secondCard.cardName && !IsAnim)
            {
                StartCoroutine(CardsNotTheSame(firstCard, secondCard));

            }
            else if (firstCard.cardName == secondCard.cardName)
            {
                matchedCards.Add(firstCard);
                matchedCards.Add(secondCard);
                SetOnCards();

            }
        }
    }
    private  void GameEnded()
    {
        foreach (CardView cardView in cardView)
        {
            if (cardView.backSide.enabled)
            {
                return;
            }
        }

        GameCompleted.SetActive(true);
        StartCoroutine(LoadRoom2());
    }
    private IEnumerator LoadRoom2()
    {
        yield return new WaitForSeconds(1);
        var task = PlayScriptAsync();
        while (!task.IsCompleted) yield return null;
    }

    private async Task PlayScriptAsync()
    {
        var player = Engine.GetService<IScriptPlayer>();
        await player.PreloadAndPlayAsync("Final");
        CanvasMiniGame.SetActive(false);
    }

    private IEnumerator CardsNotTheSame(CardView cardToAnimFirst, CardView cardToAnimSeconds)
    {
        IsAnim = true;
        yield return new WaitForSeconds(1f);
        cardToAnimFirst.animator.SetTrigger("CardAnimReverse");
        yield return new WaitForSeconds(0.5f);
        cardToAnimSeconds.animator.SetTrigger("CardAnimReverse");

        while (cardToAnimSeconds.animator.GetCurrentAnimatorStateInfo(0).IsName("CardAnimReverse") &&
         cardToAnimSeconds.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }

        SetOnCards();   

        IsAnim = false;

    }
    public void SetOffCards()
    {
        int countIsActivedCards = 0;

        foreach (CardView cardsView in cardView)
        {
            if (cardsView.countclicks > 0)
            {
                countIsActivedCards++;
            }
        }

        if (countIsActivedCards >= 2)
        {
            foreach (CardView cardsView in cardView)
            {
                cardsView.button.interactable = false;
                cardsView.countclicks = 0;
            }
        }

    }

    private void SetOnCards()
    {
        foreach (CardView cardsView in cardView)
        {
            if (cardsView.backSide.enabled)
            {
                cardsView.button.interactable = true;

            }

        }
    }
}
