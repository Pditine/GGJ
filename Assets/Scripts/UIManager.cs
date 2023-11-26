using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using tools;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public Image theWaterImage;
    public Image waterIcon;
    public Image whitePanel,otherWhitePanel;
    public Image pauseMenu;
    public Image loseMenu;
    
    public void ChangeWater(int water,int x)
    {
        Image newImage = Instantiate(waterIcon, theWaterImage.transform.position + new Vector3(x, 0, 0),
            quaternion.identity,theWaterImage.transform);
        newImage.GetComponentInChildren<Text>().text = water.ToString();
        StartCoroutine(nameof(MoveWater),newImage);
    }

    private IEnumerator MoveWater(Graphic water)
    {
        for (var i = 0; i < 30; i++)
        {
            water.transform.position += new Vector3(0, 6, 0);
            water.color -= new Color(0, 0, 0, 0.03f);
            water.GetComponentInChildren<Text>().color -= new Color(0, 0, 0, 0.03f);
            yield return new WaitForSeconds(0.03f);
        }
        Destroy(water.gameObject);
    }

    public void WaterIsNotEnough()
    {
        theWaterImage.GetComponentInChildren<Text>().transform.localScale *= 2;
        theWaterImage.GetComponentInChildren<Text>().color = Color.red;
        theWaterImage.GetComponentInChildren<Text>().transform.position += new Vector3(100, 0, 0);
        StartCoroutine(nameof(RecoverWaterUI));
    }

    private IEnumerator RecoverWaterUI()
    {
        yield return new WaitForSeconds(0.7f);
        theWaterImage.GetComponentInChildren<Text>().transform.localScale /= 2;
        theWaterImage.GetComponentInChildren<Text>().color = Color.black;
        theWaterImage.GetComponentInChildren<Text>().transform.position -= new Vector3(100, 0, 0);
    }

    public void FadeInThenChangeScene(int scene)
    {
        whitePanel.raycastTarget = true;
        StartCoroutine(nameof(FadeIn),scene);
    }

    private IEnumerator FadeIn(int scene)
    {
        while (whitePanel.color.a<0.95f)
        {
            whitePanel.color += new Color(0,0,0,0.018f);
            yield return new WaitForSeconds(0.06f);
        }
        MySceneManager.Instance.LoadScene(scene);
    }

    public void SwitchPauseMenu()
    {
        pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
        otherWhitePanel.gameObject.SetActive(!otherWhitePanel.gameObject.activeSelf);
    }

    public void SwitchLoseMenu(int kind )
    {
        loseMenu.gameObject.SetActive(true);
        otherWhitePanel.gameObject.SetActive(true);
        if (kind==1)
        {
            loseMenu.GetComponentInChildren<Text>().text = "植物的水分消耗殆尽";
        }
        else
        if (kind==2)
        {
            loseMenu.GetComponentInChildren<Text>().text = "植物的根缠绕在了一起，已经无法继续向下";
        }
    }
}
