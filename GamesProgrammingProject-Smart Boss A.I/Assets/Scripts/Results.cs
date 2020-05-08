using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Results : BaseEnemy
{
    public TextMeshProUGUI Miniboss1Feature1Result;
    public TextMeshProUGUI Miniboss1Feature2Result;
    public TextMeshProUGUI Miniboss2Feature1Result;
    public TextMeshProUGUI Miniboss2Feature2Result;

    void Start()
    {
        Miniboss1Feature1Result.text = AmountOfShotsFiredMB1Feature1.ToString();
        Miniboss1Feature2Result.text = AmountOfShotsFiredMB1Feature2.ToString();
        Miniboss2Feature1Result.text = AmountOfShotsFiredMB2Feature1.ToString();
        Miniboss2Feature2Result.text = AmountOfShotsFiredMB2Feature2.ToString();

    }

    void update()
    {
        RunResults();
    }

    public void RunResults()
    {
        Miniboss1Feature1Result.text = AmountOfShotsFiredMB1Feature1.ToString();
        Miniboss1Feature2Result.text = AmountOfShotsFiredMB1Feature2.ToString();
        Miniboss2Feature1Result.text = AmountOfShotsFiredMB2Feature1.ToString();
        Miniboss2Feature2Result.text = AmountOfShotsFiredMB2Feature2.ToString();
        Debug.Log("I should have updated");
    }

}
