# ParallelLoopState

I en synkron løkke kan man hoppe ud af den vha. `break`. I et forsøg på at lave noget der ligner, har både `Parallel.For` og `Parallel.ForEach` en overload, der tager imod et `ParallelLoopState` objekt, der giver mulighed for at sende et signal til delegate-metoden om at den skal standse med at oprette nye tasks, samt spørge på status.

Her ses et eksempel, der ligner en normal `break` lidt. Men det man standser er blot processen med at starte nye tasks – dem der er i gang vil stadigvæk gøre deres arbejde færdigt:

```C#
Random rnd = new Random();

Parallel.For(0, 100, (i, loopState) =>
{
    if (rnd.Next(1, 50) == 1)
    {
        Console.WriteLine("Task Id: {0} : Breaking on i = {1} and no more new Tasks are instantiated!", Task.CurrentId, i);
        loopState.Break();
        return;

    }
    Console.WriteLine("Task Id: {0} : {1}", Task.CurrentId, i);
});
```

Hvis man blot ønsker at standse løkken så hurtigt som muligt og kan leve med at tidligere gennemløb måske ikke er helt afsluttede, så kan man benytte `Stop()` i stedet for `Break()`:

```C#
Random rnd = new Random();

Parallel.For(0, 100, (i, loopState) =>
{
    if (rnd.Next(1, 50) == 1)
    {
        Console.WriteLine("Task Id: {0} : Stopping on i= {1} and only ongoing iterations are fullfilled", Task.CurrentId, i);
        loopState.Stop();
        return;

    }

    Console.WriteLine("{0} : {1}", Task.CurrentId, i);
});
```

Nu vil igangværende tasks ikke prøve at køre flere iterationer, kun igangværende iterationer kører færdigt.

ParallelLoopState objektet har en property `IsCompleted`, der bliver false hvis enten en `Stop` eller `Break` har været aktiveret.

