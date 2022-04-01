using System.Collections.Generic;

public class Instrument
{
    public string instrumentName { get; set; }
    public float LRV { get; set; }
    public float URV { get; set; }
    public float AL { get; set; }
    public float AH { get; set; }

    private List<Measurement> measurementList;

    public Instrument(string instrumentName)
    {
        this.instrumentName = instrumentName;
        LRV = URV=AL=AH=0;
        measurementList = new List<Measurement>();
    }

    public override string ToString()
    {
        return "Instrument Name: " + instrumentName + "\nLRV: " + LRV + "\nURV: " + URV + "\nAL: " + AL + "\nAH: " + AH;
    }

    public void AddMeasurementToList(Measurement m)
    {
        measurementList.Add(m);
    }

    public void ClearMeasurementList()
    {
        measurementList.Clear();
    }

    public List<Measurement> GetMeasurementList()
    {
        return measurementList;
    }
}
