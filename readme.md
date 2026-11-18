# Task Parallel Library (TPL)

MSDN: Parallel Computing: https://msdn.microsoft.com/da-dk/vstudio/bb964701

Benytter TPL (Task Parallel Library), som findes i namespacet `System.Threading.Tasks`

TPL benytter _CLR thread pool_. Det betyder at den selv opdeler opgaven, styrer threads og state management og andre lowlevel detaljer.

`System.Threading.Tasks.Parallel` klassen indeholder tre static metoder:

- `Parallel.Invoke()`
- `Parallel.For()`
- `Parallel.ForEach()`

Klassen `ParallelOptions` har nogle properties, der giver mulighed for nogle få indstillinger:

- `MaxDegreeOfParallelism`: angiver maksimalt antal tasks, der må kører i parallel). Ofte vil det være det samme, som antal cpu-kerner.
- `CancellationToken`: Giver mulighed for at forhindre at nye tasks startes. Igangværende køres færdig.
- `TaskScheduler`: Giver mulighed for at skrive sin egen Taskscheduler.

## Parallel Loops
Umiddelbart skulle man tro at en løkke er en god kandidat for parallel-processing – men det er ikke altid tilfældet. Det kræver at rækkefølgen af afviklingen kan tåle at være tilfældig. En algoritme til beregning af f.eks. [Fibonacci-tallene](https://matnat.dk/matematik/aritmetik-og-algebra/tal/fibonacci-tal) vil slet ikke kunne leve op til dette.

### Parallel.For
```C#
Parallel.For(0, 20, i =>
{
    Console.WriteLine(i);
});
```
TPL prøver at estimere hvor mange tasks, der er nødvendigt for at løse opgaven. Tager det længere tid, sætter den flere tasks i gang.

Se to andre eksempler i __2.ParallelFor__, som viser eksempler på en algoritme til beregning af PI, hvor mange af problemerne ved parallel-beregninger viser sig.
