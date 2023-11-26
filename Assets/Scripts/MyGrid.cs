using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGrid : MonoBehaviour
{
    public Image theCard;
    public int rowIndex;
    public int gridIndex;
    public bool isOccupied;
    
    private void Start()
    {
        var theRow = GetComponentInParent<Row>();
        if (theRow) rowIndex = theRow.index;
        else Debug.Log("空引用" );
    }
    
    public bool CheckPuttable(int water,int startNode,List<int> connoectNode)
    {
        //todo:考验数学
        if (water==1)
        {
            bool node1 = false,node2 = false;
            node1 = startNode switch
            {
                1 => GameManager.Instance.rows[rowIndex].nodes[gridIndex],
                2 => GameManager.Instance.rows[rowIndex].nodes[4 + gridIndex],
                4 => GameManager.Instance.rows[rowIndex].nodes[5 + gridIndex],
                5 => GameManager.Instance.rows[rowIndex].nodes[1 + gridIndex],
                _ => false
            };
            node2 = connoectNode[0] switch
            {
                1 => GameManager.Instance.rows[rowIndex].nodes[gridIndex],
                2 => GameManager.Instance.rows[rowIndex].nodes[4 + gridIndex],
                4 => GameManager.Instance.rows[rowIndex].nodes[5 + gridIndex],
                5 => GameManager.Instance.rows[rowIndex].nodes[1 + gridIndex],
                _ => false
            };
            return node1 || node2;
        }
        else
        {
            switch (startNode)
            {
                case 0: return GameManager.Instance.rows[rowIndex - 1].nodes[8+gridIndex];
                case 2: return GameManager.Instance.rows[rowIndex].nodes[4+gridIndex];
                case 4: return GameManager.Instance.rows[rowIndex].nodes[5+gridIndex];
            }
        }
        Debug.Log("卡牌数据有误");
        return false;
    }
    
    public void ConnectNodes(int water,int startNode,List<int> connectNode)
    {
        if (water ==1)
        {
            SwitchConnectNode(startNode);
            SwitchConnectNode(connectNode[0]);
        }
        else if(water == 2)
        {
            SwitchConnectNode(connectNode[0]);
            SwitchConnectNode(connectNode[1]);
        }else if (water == 3)
        {
            SwitchConnectNode(connectNode[0]);
            SwitchConnectNode(connectNode[1]);
            SwitchConnectNode(connectNode[2]);
        }
        
        GetComponentInParent<Row>().CheckCanEatWater();
        GameManager.Instance.CheckGameFail(rowIndex);
        GameManager.Instance.ChangeFloor(rowIndex);
        BookManager.Instance.UpdateBook(rowIndex);
        AudioManager.Instance.putCard.Play();
    }

    private void SwitchConnectNode(int index)
    {
        switch (index)
        {
            case 1: GameManager.Instance.rows[rowIndex].nodes[gridIndex] = true;
                break;
            case 2: GameManager.Instance.rows[rowIndex].nodes[4+gridIndex] = true;
                break;
            case 3: GameManager.Instance.rows[rowIndex].nodes[8 + gridIndex] = true;
                break;
            case 4: GameManager.Instance.rows[rowIndex].nodes[5+gridIndex] = true;
                break;
            case 5: GameManager.Instance.rows[rowIndex].nodes[1+gridIndex] = true;
                break;
        }
    }
}
