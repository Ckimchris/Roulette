using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class IterationTest : MonoBehaviour
{
    public Roulette roulette;
    public int iterationCount;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpinIteration(iterationCount);
        }
    }

    void SpinIteration(int count)
    {
        for(int i = 0; i < count; i++)
        {
            roulette.Spin(true);
        }

        roulette.RecordWins();
        CreateText();
    }

    void CreateText()
    {
        string path = Application.dataPath + "/IterationLog.txt";

        if(File.Exists(path))
        {
            File.Delete(path);
            File.WriteAllText(path, "Iteration Count Log \n\n");            
        }
        else
        {
            File.WriteAllText(path, "Iteration Count Log \n\n");            
        }

        using(StreamWriter sw = File.AppendText(path))
        {
            for(int i = 0; i < roulette.prizes.Count; i ++)
            {
                sw.WriteLine(string.Format("Prize Name: {0} Win Count {1}", roulette.prizes[i].name, roulette.prizeWinCount[i]));
            }
        }
    }
}
