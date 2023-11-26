using System;
using System.Collections;
using System.Collections.Generic;
using tools;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoSingleton<GameManager>
{
    public List<Card> allCards = new();
    public List<Row> rows = new();
    public Row row;
    public Transform content;
    public List<Sprite> shallowRow = new();
    public List<Sprite> middleRow = new();
    public List<Sprite> deepRow = new();
    public int water=10;
    public Image theWaterImage,theFloorImage;
    public List<HandGrid> handGrids = new();


    private void Start()
    {
        InitHandCards();
    }

    private void InitHandCards()
    {
        ReplenishHandCards(1,-1);
        ReplenishHandCards(2,-1);
        ReplenishHandCards(3,-1);
    }
    
    public void AddRow(int index)
    {
        if (rows.Count >= index + 2) return;
        var newRow= Instantiate(row,content);
        newRow.index = rows.Count;
        newRow.GetComponent<Image>().sprite = index switch
        {
            <= 10 => shallowRow[Random.Range(0, shallowRow.Count)],
            > 10 and <= 20 => middleRow[Random.Range(0, middleRow.Count)],
            > 20 and <= 50 => deepRow[Random.Range(0, deepRow.Count)],
            _ => newRow.GetComponent<Image>().sprite
        };
        if (index==49)
            GameOver();
        rows.Add(newRow);
    }

    public void ReplenishHandCards(int kind,int lastCardIndex)

    {
        switch (kind)
        {
            case 1:
                int num1 = Random.Range(0, 4);
                 while (num1==lastCardIndex)
                {
                    num1 = Random.Range(0, 4);
                }
                Instantiate(allCards[num1],handGrids[0].transform.position- new Vector3(20,0,0),quaternion.identity,
                    handGrids[0].transform);
                break;
            case 2:
                int num2 = Random.Range(10,14);
                while (num2==lastCardIndex)
                {
                    num2 = Random.Range(10,14);
                }
                Instantiate(allCards[Random.Range( 10,14)],handGrids[1].transform.position- new Vector3(20,0,0),quaternion.identity,
                    handGrids[1].transform);
                break;
            default:
                int num3 = Random.Range(4, 10);
                while (num3==lastCardIndex)
                {
                    num3 = Random.Range(4, 10);
                }
                Instantiate(allCards[Random.Range(4, 10)],handGrids[2].transform.position- new Vector3(20,0,0),quaternion.identity,
                    handGrids[2].transform);
                break;
        }
    }
    
    public void ChangeWater(int water,int y)
    {
        this.water += water;
        theWaterImage.GetComponentInChildren<Text>().text = this.water.ToString();
        UIManager.Instance.ChangeWater(water,y);
    }

    public void ChangeFloor(int rowIndex)
    {
        if (rowIndex>int.Parse(theFloorImage.GetComponentInChildren<Text>().text))
        {
            theFloorImage.GetComponentInChildren<Text>().text = rowIndex.ToString();
        }
    }

    private void GameOver()
    
    {
        UIManager.Instance.FadeInThenChangeScene(2);
    }

    public void CheckGameFail(int rowIndex)
    {
        if (water<=0&&rowIndex<50)
        {
            UIManager.Instance.SwitchLoseMenu(1);
        }
        else if(rows[rowIndex].grids[0].isOccupied&&rows[rowIndex].grids[1].isOccupied&&rows[rowIndex].grids[2].isOccupied&&
                !rows[rowIndex].nodes[8]&&!rows[rowIndex].nodes[9]&&!rows[rowIndex].nodes[10])
        {
            UIManager.Instance.SwitchLoseMenu(2);
            //TODO:失败，格子占满了
        }
    }
}
