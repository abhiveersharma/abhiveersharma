// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
oddNumbers(2, 5);
static List<int>oddNumbers(int l, int r)
{
    List<int> result = new List<int>();
    if (r % 2 == 1)
    {
        result.Add(r);
    }
    for(int i = l; i < r; i++)
    {
        if (i % 2 == 1)
        {
            result.Add(i);
        }

    }
    return result;
}
