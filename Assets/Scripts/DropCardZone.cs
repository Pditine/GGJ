using System.Collections;
using System.Collections.Generic;
using tools;
using UnityEngine;

public class DropCardZone : MonoSingleton<DropCardZone>
{
    public void GiveUpCard(Card theCard,int water)
    {
        GameManager.Instance.ChangeWater(-water,260);
        GameManager.Instance.CheckGameFail(GameManager.Instance.rows.Count-1);
        Destroy(theCard.gameObject);
    }
}
