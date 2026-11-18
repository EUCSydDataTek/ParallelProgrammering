using System.Diagnostics;

int sumX = 0;
int sumY = 0;
int sumZ = 0;

CancellationTokenSource cts = new CancellationTokenSource();
//cts.CancelAfter(2000);
Stopwatch sw = new Stopwatch();

try
{
    sw.Start();
    // Executes SumX, SumY and SumZ in parallel
    Parallel.Invoke(new ParallelOptions() { CancellationToken = cts.Token, MaxDegreeOfParallelism = 1 },
       // () => { throw new Exception("Boom!"); },
       () => sumX = SumX(cts.Token),
       () => sumY = SumY(cts.Token),
       () => sumZ = SumZ(cts.Token)
     );

    int total = sumX + sumY + sumZ;

    sw.Stop();
    Console.WriteLine("Sum = " + total);
    Console.WriteLine(sw.Elapsed.TotalSeconds + " s");
}
catch (OperationCanceledException operationCanceled)
{
    Console.WriteLine(sw.Elapsed.TotalSeconds + " s");
    Console.WriteLine(operationCanceled.Message);
}
catch (AggregateException errors)
{
    foreach (Exception error in errors.Flatten().InnerExceptions)
    {
        Console.WriteLine(error.Message);
    }
}

#region metoder
static int SumX(CancellationToken ct)
{
    Console.WriteLine($"SumX on threadID {Thread.CurrentThread.ManagedThreadId}");
    if (ct.IsCancellationRequested)
    {
        return 0;
    }
    Thread.Sleep(5000);
    return 3;
}

static int SumY(CancellationToken ct)
{
    Console.WriteLine($"SumY on threadID {Thread.CurrentThread.ManagedThreadId}");
    if (ct.IsCancellationRequested)
    {
        return 0;
    }
    Thread.Sleep(5000);
    return 3;
}

static int SumZ(CancellationToken ct)
{
    Console.WriteLine($"SumZ on threadID {Thread.CurrentThread.ManagedThreadId}");
    if (ct.IsCancellationRequested)
    {
        return 0;
    }
    Thread.Sleep(5000);
    return 3;
}

#endregion metoder