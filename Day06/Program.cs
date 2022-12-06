string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

int StartOfPacket(string packet, int packetLength)
{
    for (var i = 0; i < packet.Length - packetLength; i++)
    {
        var subPacket = packet.Substring(i, packetLength);
        if (subPacket.DistinctBy(x => x).Count() == packetLength)
        {
            return i + packetLength;
        }
    }

    throw new InvalidDataException("Invalid Packet");
}

var lines = File.ReadAllLines(testDataPath).ToList();

lines.ForEach(x => Console.WriteLine($"Index if first packet: {StartOfPacket(x,4)}"));
lines.ForEach(x => Console.WriteLine($"Index if first message: {StartOfPacket(x,14)}"));