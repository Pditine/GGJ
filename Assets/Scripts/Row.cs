using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Row : MonoBehaviour
{
    public List<MyGrid> theGrid = new(); 
    public bool isFirstRow;
    public int index;
    public List<bool> nodes = new(11);
    public List<Transform> nodesTrans = new();
    public List<MyGrid> grids = new(3);
    private readonly Dictionary<int, int> _node0347 = new();
    public Image bigWater, smallWater;
    private const int BigWaterNum = 6;
    private const int SmallWaterNum = 4;
    private Dictionary<int,Image> _thePuttedWater = new();
    
    private void Start()
    {
        if (isFirstRow) return;
        _node0347[0] = ReturnWaterNum(Random.Range(0, 9));
        _node0347[3] = ReturnWaterNum(Random.Range(0, 9));
        _node0347[4] = ReturnWaterNum(Random.Range(0, 9));
        _node0347[7] = ReturnWaterNum(Random.Range(0, 9));
        PutWater(_node0347[0],0);
        PutWater(_node0347[3],3);
        PutWater(_node0347[4],4);
        PutWater(_node0347[7],7);
    }

    //TODO:改概率
    private int ReturnWaterNum(int num)
    {
        if (num < 2) return BigWaterNum;
        if (num < 5) return SmallWaterNum;
        return 0;
    }

    private void PutWater(int water,int index)
    {
        if (water == BigWaterNum) _thePuttedWater[index] = Instantiate(bigWater, nodesTrans[index].position, quaternion.identity,transform);
        if (water == SmallWaterNum) _thePuttedWater[index] = Instantiate(smallWater, nodesTrans[index].position, quaternion.identity,transform);
    }

    public void CheckCanEatWater()
    {
        EatWater(0);
        EatWater(3);
        EatWater(4);
        EatWater(7);
    }
    
    private void EatWater(int node)
    {
        if (_node0347[node] <= 0 || !nodes[node]) return;
        AudioManager.Instance.pp.Play();
        GameManager.Instance.ChangeWater(_node0347[node],190);
        _node0347[node] = 0;
        StartCoroutine(nameof(WaterDisappear), _thePuttedWater[node]);
    }

    private IEnumerator WaterDisappear(Image waterIcon)
    {
        for (int i = 0; i < 10; i++)
        {
            waterIcon.transform.localScale *= 1.05f;
            yield return new WaitForSeconds(0.02f);
        }

        for (int i = 0; i < 20; i++)
        {
            waterIcon.transform.localScale *= 0.9f;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(waterIcon);
    }
}
