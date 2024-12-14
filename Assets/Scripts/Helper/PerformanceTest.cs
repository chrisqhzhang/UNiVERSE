using System.Diagnostics;
using UnityEngine;

public class PerformanceTest : MonoBehaviour
{
    private void Start()
    {
        TestPerformance();
    }

    private void TestPerformance()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        stopwatch.Start();

        FunctionToTest();
        
        stopwatch.Stop();

        long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        long elapsedTicks = stopwatch.ElapsedTicks;
        
        print($"Elapsed time in milliseconds : {elapsedMilliseconds}");
        print($"Elapsed ticks : {elapsedTicks}");
    }

    private void FunctionToTest()
    {
        int numberOfIterrations = 1;

        int counter = 0;
        while (counter < numberOfIterrations)
        {
            counter++;
            GameObject.FindGameObjectWithTag("Player");
        }
    }
}
