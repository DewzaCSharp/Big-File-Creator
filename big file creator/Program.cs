using System;
using System.IO;
using System.Text;

class FileGenerator
{
    static void Main()
    {
        string pattern = "!@#$%^&*()_+-=[]{}|;:,.<>?/~`ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        int patternLength = pattern.Length;

        Console.Write("Enter the target file size in GB: ");

        double targetSizeGb = double.Parse(Console.ReadLine());
        long targetSizeBytes = (long)(targetSizeGb * Math.Pow(1024, 3));

        int chunkSize = 10 * 1000 * 1000;

        long numChunks = targetSizeBytes / chunkSize;
        long remainingBytes = targetSizeBytes % chunkSize;

        string filePath = "ud.bat";

        Console.WriteLine("File Type:\n1 = .exe\n2 = .bat\n3 = .txt\n4 = .py\n5 = .js\n6 = .cs\n7 = .cpp\ninput:");
        string choice = Console.ReadLine();
        if (choice == "1") filePath = "UD.exe";
        else if (choice == "2") filePath = "ud.bat";
        else if (choice == "3") filePath = "ud.txt";
        else if (choice == "4") filePath = "ud.py";
        else if (choice == "5") filePath = "ud.js";
        else if (choice == "6") filePath = "ud.cs";
        else if (choice == "7") filePath = "ud.cpp";

        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            byte[] chunkContent = new byte[chunkSize];
            int currentIndex = 0;
            while (currentIndex < chunkSize)
            {
                int toCopy = Math.Min(pattern.Length, chunkSize - currentIndex);
                Encoding.UTF8.GetBytes(pattern, 0, toCopy, chunkContent, currentIndex);
                currentIndex += toCopy;
            }

            for (long i = 0; i < numChunks; i++)
            {
                fs.Write(chunkContent, 0, chunkSize);

                double progress = ((double)(i + 1) / (numChunks + (remainingBytes > 0 ? 1 : 0))) * 100;

                Console.Write($"\rProgress: {progress:F2}% ({i + 1}/{numChunks + (remainingBytes > 0 ? 1 : 0)}) ");
                Console.Write(new string(' ', 10));
                Console.Write("\r");
            }

            if (remainingBytes > 0)
            {
                byte[] remainingContent = new byte[remainingBytes];
                for (long i = 0; i < remainingBytes; i++)
                {
                    remainingContent[i] = (byte)pattern[(int)(i % patternLength)];
                }
                fs.Write(remainingContent, 0, (int)remainingBytes);

                Console.Write($"\rProgress: 100.00% ({numChunks + 1}/{numChunks + (remainingBytes > 0 ? 1 : 0)}) ");
            }
        }
        Console.WriteLine("\n\nFile creation completed!");
        Console.WriteLine($"File created: {filePath}");
        Console.WriteLine($"File size: {new FileInfo(filePath).Length / Math.Pow(1024, 3):F2} GB");
        Console.ReadKey();
    }
}