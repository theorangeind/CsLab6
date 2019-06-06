using System;
using System.IO;
using System.Threading;

namespace CsLab6
{
    class Program
    {
        static String path = "D:\\lab6-";
        static String charList = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";

        static int sum = 0;

        static void Main(string[] args)
        {
            Random rand = new Random();
            int fileCount = 2;
            for (int i = 1; i <= fileCount; i++)
            {
                writeRandomFile(i);
            }

            Console.WriteLine("Start reading");
            Thread t1 = new Thread(() => checkFile(1));
            Thread t2 = new Thread(() => checkFile(2));
            t1.Start();
            t2.Start();

            while (t1.IsAlive || t2.IsAlive) { }

            Console.WriteLine("\nSum equals " + sum.ToString() + "\n");
        }

        static void writeRandomFile(int fileN)
        {
            fileN = Math.Abs(fileN);
            FileStream fileStream = new FileStream(path + fileN.ToString() + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fileStream);

            Random rand = new Random();
            int r = rand.Next(1, 100);

            for(int i = 0; i < r; i++)
            {
                Random c = new Random();
                writer.Write(charList[c.Next(0, charList.Length-1)]);
            }

            writer.Close();
            fileStream.Close();
        }

        static void checkFile(int fileN)
        {
            FileStream fileStream = new FileStream(path + fileN.ToString() + ".txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fileStream);

            while (!reader.EndOfStream)
            {
                char c = (char)reader.Read();
                sum += c;
                Console.Write(c);
            }

            Console.Write("\n");

            reader.Close();
            fileStream.Close();
        }
    }
}
