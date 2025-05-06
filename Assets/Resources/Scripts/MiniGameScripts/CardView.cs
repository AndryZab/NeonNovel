using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class CardView : MonoBehaviour
{
    public string CardName { get; private set; }
    public event Action<CardView> OnCardFlipped;
    public Image backSide;

    private Image[] cards;

    
    public Button button;
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        cards = GetComponentsInChildren<Image>();

    }
   
    public bool IsAnimating { get; private set; }

    public void AssignCard(string name)
    {
        CardName = name;
        button.onClick.AddListener(Flip);
        backSide.enabled = true;
    }

    private void Flip()
    {
        if (!button.interactable || !backSide.enabled) return;
        button.interactable = false;
        animator.SetTrigger("CardAnim");
        OnCardFlipped?.Invoke(this);
    }
    public void EventTriggerActivateCards()
    {
        foreach (var card in cards)
        {
            if (card.name == CardName)
            {
                card.enabled = true;
            }
        }
        backSide.enabled = false;

    }
    public void DeactivateCards()
    {
        foreach (var card in cards)
        {
            if (card.name == CardName)
            {
                card.enabled = false;
                backSide.enabled = true;
            }
        }
    }

    public void FlipBack()
    {

        IsAnimating = true;
        StartCoroutine(FlipBackRoutine());
    }

    private IEnumerator FlipBackRoutine()
    {
        animator.SetTrigger("CardAnimReverse");
        yield return new WaitForSeconds(0.5f);
        DeactivateCards();
        IsAnimating = false;
    }
}
