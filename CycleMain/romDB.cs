using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

[XmlRoot("database")]
public class Database
{
    [XmlAttribute("version")]
    public string Version { get; set; }

    [XmlAttribute("conformance")]
    public string Conformance { get; set; }

    [XmlAttribute("agent")]
    public string Agent { get; set; }

    [XmlAttribute("author")]
    public string Author { get; set; }

    [XmlAttribute("timestamp")]
    public string Timestamp { get; set; }

    [XmlElement("game")]
    public List<Game> Games { get; set; }
}

public class Game
{
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("altname")]
    public string AltName { get; set; }

    [XmlAttribute("class")]
    public string Class { get; set; }

    [XmlAttribute("catalog")]
    public string Catalog { get; set; }

    [XmlAttribute("publisher")]
    public string Publisher { get; set; }

    [XmlAttribute("developer")]
    public string Developer { get; set; }

    [XmlAttribute("region")]
    public string Region { get; set; }

    [XmlAttribute("players")]
    public string Players { get; set; }

    [XmlAttribute("date")]
    public string Date { get; set; }

    [XmlElement("cartridge")]
    public Cartridge Cartridge { get; set; }
}

public class Cartridge
{
    [XmlAttribute("system")]
    public string System { get; set; }

    [XmlAttribute("crc")]
    public string CRC { get; set; }

    [XmlAttribute("sha1")]
    public string SHA1 { get; set; }

    [XmlAttribute("dump")]
    public string Dump { get; set; }

    [XmlAttribute("dumper")]
    public string Dumper { get; set; }

    [XmlAttribute("datedumped")]
    public string DateDumped { get; set; }

    [XmlElement("board")]
    public Board Board { get; set; }
}

public class Board
{
    [XmlAttribute("type")]
    public string Type { get; set; }

    [XmlAttribute("pcb")]
    public string PCB { get; set; }

    [XmlAttribute("mapper")]
    public string Mapper { get; set; }

    [XmlElement("prg")]
    public Prg PRG { get; set; }

    [XmlElement("vram")]
    public Vram VRAM { get; set; }

    [XmlElement("chip")]
    public Chip Chip { get; set; }
}

public class Prg
{
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("size")]
    public string Size { get; set; }

    [XmlAttribute("crc")]
    public string CRC { get; set; }

    [XmlAttribute("sha1")]
    public string SHA1 { get; set; }
}

public class Vram
{
    [XmlAttribute("size")]
    public string Size { get; set; }
}

public class Chip
{
    [XmlAttribute("type")]
    public string Type { get; set; }
}

class romDB
{
    public static Dictionary<string, string> GetCRCGameDictionary()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nescartsdDB.xml");
        return GetCRCGameDictionary(filePath);
    }

    public static Dictionary<string, string> GetCRCGameDictionary(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("XML file not found.");
        }

        string xmlData = File.ReadAllText(filePath);
        XmlSerializer serializer = new XmlSerializer(typeof(Database));

        using (StringReader reader = new StringReader(xmlData))
        {
            Database database = (Database)serializer.Deserialize(reader);
            Dictionary<string, string> crcGameDictionary = new Dictionary<string, string>();

            foreach (Game game in database.Games)
            {
                Cartridge cartridge = game.Cartridge;
                string crc = cartridge.CRC;
                string gameName = game.Name;
                string region = game.Region;
                crcGameDictionary[crc] = $"{gameName} (Region: {region})";
            }

            return crcGameDictionary;
        }
    }

    public static string FindGameNameByCRC(string targetCRC)
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nescartsdDB.xml");
        return FindGameNameByCRC(filePath, targetCRC);
    }

    public static string FindGameNameByCRC(string filePath, string targetCRC)
    {
        Dictionary<string, string> crcGameDictionary = GetCRCGameDictionary(filePath);

        if (crcGameDictionary.ContainsKey(targetCRC))
        {
            return crcGameDictionary[targetCRC];
        }
        else
        {
            return "Unnamed ROM Image";
        }
    }
}
