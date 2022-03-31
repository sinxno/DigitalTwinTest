public class Instrument
{
    public string instrumentName { get; set; }
    public float LRV { get; set; }
    public float URV { get; set; }
    public float AL { get; set; }
    public float AH { get; set; }

    public Instrument(string instrumentName)
    {
        this.instrumentName = instrumentName;
        LRV = URV=AL=AH=0;
    }

    public override string ToString()
    {
        return "Instrument Name: " + instrumentName + "\nLRV: " + LRV + "\nURV: " + URV + "\nAL: " + AL + "\nAH: " + AH;
    }
}
