# Nested Loops

Som vi tidlige oplevede ved beregning af __PI__, så er det langt fra altid at en parallel loop giver den store forbedring af performance. Fordi hvert eneste gennemløb kalder delegate-metoden, tilføjes et _overhead af clock cycles_, som tager tid.

Hvis man i stedet deler opgaven op i en udvendig loop, der kører parallelt og en indre loop, der kører synkront, kan man ofte optimere processen. Så vi har to nestede synkrone loops:
```C#
for (int i = 0; i < 500; i++)
{
    for (int j = 0; j < 100; j++)
    {
        // Do Stuff
    }
}
```

Den yderste loop køres _parallelt_, mens den nestede stadig er _synkron_:
```C#
Parallel.For(0, 500, i =>
{
    for (int j = 0; j < 100; j++)
    {
        // Do Stuff
    }
});
```

Selv om man kun har en løkke i ét niveau, kan man altid bryde den op i en nested loop, som vist her:
```C#
// Single loop
for (int i = 0; i < 50000; i++)
{
    // Do Stuff
}
 
// Nested loop
for (int i = 0; i < 500; i++)
{
    for (int j = 0; j < 100; j++)
    {
        // Do Stuff
    }
}
```
