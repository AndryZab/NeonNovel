using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    public Image cardHeart;
    public Image cardGirl;
    public Image cardBanana;
    public Image backSide;
    public Button button;
    public string cardName;
    public Animator animator;
    public int countclicks = 0;
    private void Start()
    {
        animator = GetComponent<Animator>();
        button = GetComponent<Button>();
        backSide = GetComponent<Image>();
    }

    public void CardAnim()
    {
        animator.SetTrigger("CardAnim");
        button.interactable = false;
        countclicks++;
        CardManager.Instance.SetOffCards();
    }
   
    public void ActivateCard()
    {
        switch (cardName)
        {
            case "cardHeart":
                cardHeart.enabled = true;
                backSide.enabled = false;
                break;
            case "cardBanana":
                cardBanana.enabled = true;
                backSide.enabled = false;
                break;
            case "cardGirl":
                cardGirl.enabled = true;
                backSide.enabled = false;
                break;
            default:
                break;
        }
    }
    public void DeactivateCard()
    {
        button.interactable = true;
        backSide.enabled = true;
        cardHeart.enabled = false;
        cardBanana.enabled = false;
        cardGirl.enabled = false;

    }



}
