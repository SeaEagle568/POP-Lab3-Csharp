using System;
using System.Collections.Concurrent;
using System.Threading;

public class Storage(int capacity, int occupancy)
{
    private readonly Semaphore isNotOccupied = new Semaphore(occupancy, occupancy);
    private readonly Semaphore isNotEmpty = new Semaphore(0, capacity);
    private readonly Semaphore isNotFull = new Semaphore(capacity, capacity);
    private readonly ConcurrentQueue<Item> queue = new();

    public void PutItem(Item item)
    {
        isNotFull.WaitOne();
        isNotOccupied.WaitOne();
        queue.Enqueue(item);
        Console.WriteLine($"Producer {item.ProducedBy} has provided an item {item.SerialNumber}");
        isNotEmpty.Release();
        isNotOccupied.Release();
    }

    public void RetrieveItem(string asker)
    {
        isNotEmpty.WaitOne();
        isNotOccupied.WaitOne();
        queue.TryDequeue(out var item);
        Console.WriteLine($"Consumer {asker} has received an item {item?.SerialNumber}");
        isNotFull.Release();
        isNotOccupied.Release();
        return;
    }
}
