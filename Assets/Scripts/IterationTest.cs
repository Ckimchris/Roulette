using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
