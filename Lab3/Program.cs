using System;
using System.Collections.Generic;
using System.Threading;

public class Program
{
    private const string ErrInputMismatch = "Found invalid value in input - expected integer 0 < x <= 26.\nPlease try again:";
    private static readonly Random random = new();

    public static void Main()
    {
        int capacity = ExtractFromStdin("the storage capacity", true);
        int totalItems = ExtractFromStdin("the total number of items", true);
        int consumers = ExtractFromStdin("the number of consumers", true);
        int producers = ExtractFromStdin("the number of producers", true);

        Storage storage = new(capacity, 1);

        int leftToDistribute = totalItems;
        List<Thread> threads = [];

        for (int i = 0; i < consumers; i++)
        {
            int items = random.Next(1, leftToDistribute - consumers + i + 2);
            if (i == consumers - 1) {
                items = leftToDistribute;
            }
            leftToDistribute -= items;
            var consumer = new Consumer(items, $"{i+1}", storage);
            Thread consumerThread = new Thread(new ThreadStart(consumer.Run));
            threads.Add(consumerThread);
            consumerThread.Start();
        }

        leftToDistribute = totalItems;
        for (int i = 0; i < producers; i++)
        {
            int items = random.Next(1, leftToDistribute - producers + i + 2);
            if (i == producers - 1) {
                items = leftToDistribute;
            }
            leftToDistribute -= items;
            var producer = new Producer(items, RandomName(), storage);
            Thread producerThread = new Thread(new ThreadStart(producer.Run));
            threads.Add(producerThread);
            producerThread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }
    }

    private static int ExtractFromStdin(string ask, bool validate)
    {
        Console.WriteLine($"Please enter {ask}:");
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int input))
            {
                if (validate && (input < 1 || input > 26))
                {
                    Console.Error.WriteLine(ErrInputMismatch);
                    continue;
                }
                return input;
            }
            else
            {
                Console.Error.WriteLine(ErrInputMismatch);
            }
        }
    }

    private static string RandomName()
    {
        string[] names = [
            "Alfa", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel", "India",
            "Juliett", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa", "Quebec", "Romeo",
            "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "X-ray", "Yankee", "Zulu"
        ];
        return names[random.Next(names.Length)];
    }
}
