using System;
using System.Collections;
using tools;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CGManager : MonoSingleton<CGManager>
{
    public Image whitePanel;
    private void Start()
    {
        StartCoroutine(nameof(CGGoing));
    }

    IEnumerator CGGoing()
    {
        while (whitePanel.color.a>0.05f)
        {
            whitePanel.color -= new Color(0, 0, 0, 0.03f);
            yield return new WaitForSeconds(0.06f);
        }

        yield return new WaitForSeconds(4);
        while (whitePanel.color.a<0.955f)
        {
            whitePanel.color += new Color(0, 0, 0, 0.03f);
            yield return new WaitForSeconds(0.06f);
        }
        SceneManager.LoadScene(3);
    }
}
