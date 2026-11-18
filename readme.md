# Parallel.ForEach

`Parallel.ForEach` kan benyttes til alt, der implementerer `IEnumeable` (dvs. Lists, Arrays, Collections osv.):

```C#
IEnumerable<int> items = Enumerable.Range(0, 20);

Parallel.ForEach(items, i =>
{
    Console.WriteLine($"TaskId: {Task.CurrentId} - Counter: {i}");
});
```

I modsætning til `Parallel.For` som kun kunne arbejde med en counter af typen `int` eller `long`, så kan man her lave sin egen iterator-metode og derved f.eks. løbe intervallet 0 – 2 igennem med steps på 0.1:

```C#
static void SmallStepsForEach()
{
    Parallel.ForEach(FromTo(1, 2, 0.1), Console.WriteLine);
}

private static IEnumerable<double> FromTo(double start, double end, double step)
{
    for (double current = start; current < end; current += step)
    {
        yield return current;
    }
}
```

Det største problem for `Parallel.ForEach` (i modsætning til `Parallel.For`) er hvis den ikke kender antallet af items, der skal løbes igennem. Hvis collectionen er en ren `IEnumerable` objekt kan det ikke fortælle noget om størrelsen. Det betyder at `Parallel.ForEach` må begynde med kun at give et lille antal items til hver Tasks. Efterhånden vil den dog skrue op for antallet af items/task og denne proces kaldes _Partitioning_.

Derfor prøver den under runtime at finder ud af om `IEnumerable` i virkeligheden er et array eller en `IList<T>`, fordi den så vil kunne benytte henholdsvis `Count` og `Length` properties til at få størrelsen oplyst.

Det er derfor at `Parallel.ForEach` arbejder bedst på enten arrays eller `List<T>`.