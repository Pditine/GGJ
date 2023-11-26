using System.Collections;
using System.Collections.Generic;
using tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BookIcon : MonoSingleton<BookIcon>,IPointerClickHandler
{
    public Image book;
    public Image lamp;

    public void OnPointerClick(PointerEventData eventData)
    {
        book.gameObject.SetActive(!book.gameObject.activeSelf);
        AudioManager.Instance.openBook.Play();
        lamp.gameObject.SetActive(false);
    }

    public void IconGoing()
    {
        StartCoroutine(nameof(IEIconGoing), GetComponent<Image>());
        lamp.gameObject.SetActive(true);
    }
    
    private IEnumerator IEIconGoing(Image waterIcon)
    {
        //lamp.gameObject.SetActive(true);
        for (int i = 0; i < 10; i++)
        {
            waterIcon.transform.localScale *= 1.04f;
            yield return new WaitForSeconds(0.02f);
        }

        for (int i = 0; i < 10; i++)
        {
            waterIcon.transform.localScale /= 1.04f;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
