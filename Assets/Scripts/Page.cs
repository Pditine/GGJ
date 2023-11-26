using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    public void Left()
    {
        BookManager.Instance.pages[BookManager.CurrentPageIndex].gameObject.SetActive(false);
        BookManager.CurrentPageIndex--;
        if (BookManager.CurrentPageIndex<0)
        {
            BookManager.CurrentPageIndex = BookManager.Instance.pages.Count - 1;
        }
        AudioManager.Instance.fs.Play();
        BookManager.Instance.pages[BookManager.CurrentPageIndex].gameObject.SetActive(true);
    }

    public void Right()
    {
        BookManager.Instance.pages[BookManager.CurrentPageIndex].gameObject.SetActive(false);
        BookManager.CurrentPageIndex++;
        if (BookManager.CurrentPageIndex>BookManager.Instance.pages.Count - 1)
        {
            BookManager.CurrentPageIndex = 0;
        }
        AudioManager.Instance.fs.Play();
        BookManager.Instance.pages[BookManager.CurrentPageIndex].gameObject.SetActive(true);
    }
}
