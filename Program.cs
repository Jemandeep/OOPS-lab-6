using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class Event
{
    public int EventNumber { get; set; }
    public string Location { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        
        Event myEvent = new Event { EventNumber = 1, Location = "Calgary" };

        
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream("event.txt", FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, myEvent);
        stream.Close();

        
        stream = new FileStream("event.txt", FileMode.Open, FileAccess.Read);
        Event deserializedEvent = (Event)formatter.Deserialize(stream);
        Console.WriteLine(deserializedEvent.EventNumber);
        Console.WriteLine(deserializedEvent.Location);
        stream.Close();

        
        ReadFromFile();
    }

    public static void ReadFromFile()
    {
        
        using (StreamWriter writer = new StreamWriter("event.txt"))
        {
            writer.Write("Hackathon");
        }

        
        using (FileStream fs = new FileStream("event.txt", FileMode.Open))
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                long length = fs.Length;
                br.BaseStream.Seek(0, SeekOrigin.Begin); 
                char firstChar = br.ReadChar();

                br.BaseStream.Seek(length / 2, SeekOrigin.Begin); 
                char middleChar = br.ReadChar();

                br.BaseStream.Seek(length - 1, SeekOrigin.Begin); 
                char lastChar = br.ReadChar();

                Console.WriteLine("In Word: Hackathon");
                Console.WriteLine($"The First Character is: \"{firstChar}\"");
                Console.WriteLine($"The Middle Character is: \"{middleChar}\"");
                Console.WriteLine($"The Last Character is: \"{lastChar}\"");
            }
        }
    }
}
