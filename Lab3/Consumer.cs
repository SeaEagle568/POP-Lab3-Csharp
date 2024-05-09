using System;

public class Consumer(int itemsNeeded, string name, Storage storage)
{
    private readonly int itemsNeeded = itemsNeeded;
    private readonly Storage storage = storage;
    private readonly string name = name;

    public void Run()
    {
        try
        {
            for (int i = 0; i < itemsNeeded; i++)
            {
                storage.RetrieveItem(name);
            }
        }
        catch (ThreadInterruptedException)
        {
            Console.WriteLine("Consumer was interrupted while waiting");
            Thread.CurrentThread.Interrupt();
        }
    }
}
