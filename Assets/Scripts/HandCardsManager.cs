using System.Collections;
using System.Collections.Generic;
using tools;
using UnityEngine;
using UnityEngine.UI;

public class HandCardsManager : MonoSingleton<HandCardsManager>
{
    public Image dragImage;
    public Card theDraggingCard;
}
