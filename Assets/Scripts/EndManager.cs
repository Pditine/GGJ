using System;
using System.Collections;
using System.Collections.Generic;
using tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndManager : MonoSingleton<EndManager>
{
    public Image anyKeyToContinue;
    [SerializeField] private bool canContinue;
    private void Start()
    {
        AudioManager.Instance.bgm.Stop();
        AudioManager.Instance.endbgm.Play();
        StartCoroutine(nameof(AnyKeyToContinue));
    }

    private void Update()
    {
        if (Input.anyKeyDown&&canContinue)
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator AnyKeyToContinue()
    {
        yield return new WaitForSeconds(2f);
        while (anyKeyToContinue.color.a<0.95f)
        {
            anyKeyToContinue.color += new Color(0, 0, 0, 0.04f);
            yield return new WaitForSeconds(0.06f);
        }

        canContinue = true;
    }
}
