using System;

public class Producer(int itemsNeeded, string name, Storage storage)
{
    private readonly int itemsNeeded = itemsNeeded;
    private readonly string name = name;
    private readonly Storage storage = storage;

    public void Run()
    {
        try
        {
            for (int i = 1; i <= itemsNeeded; i++)
            {
                storage.PutItem(new Item(name, i));
            }
        }
        catch (ThreadInterruptedException)
        {
            Console.WriteLine("Producer was interrupted while waiting");
            Thread.CurrentThread.Interrupt();
        }
    }
}
