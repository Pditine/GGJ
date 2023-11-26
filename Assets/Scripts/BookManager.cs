using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using tools;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BookManager : MonoSingleton<BookManager>
    {
        public List<string> words = new();
        public Image bookImage;
        public Text bookWordText;
        public List<int> usedWordIndex = new();
        public List<Image> pages = new();
        public Image page;
        public static int CurrentPageIndex;
        public GameObject book;

        public void UpdateBook(int rowIndex)
        {
            switch (rowIndex)
            {                
                case 1:AddText(0);
                    break;
                case 5:AddText(1);
                    break;
                case 10:AddText(2);
                    break;
                case 20:AddText(3);
                    break;
                case 35:AddText(4);
                    break;
                case 45:AddText(5);
                    break;
            }
        }

        private void AddText(int wordIndex)
        {
            if (usedWordIndex.Any(t => wordIndex == t))
                return;
            AudioManager.Instance.sx.Play(); 
            BookIcon.Instance.IconGoing();
            usedWordIndex.Add(wordIndex);
            var newPage = Instantiate(page,book.transform);
            newPage.gameObject.SetActive(false);
            newPage.GetComponentInChildren<Text>().text = words[wordIndex];
            pages.Add(newPage);
            // newText.text = words[wordIndex];
            // StartCoroutine(nameof(TextFadeOut),newText);
        }

        private IEnumerator TextFadeOut(Text text)
        {
            text.color = new Color(0, 0, 0, 0);
            while (text.color.a<0.95f)
            {
                text.color += new Color(0, 0, 0, 0.4f);
                yield return new WaitForSeconds(0.06f);
            }
        }
    }
