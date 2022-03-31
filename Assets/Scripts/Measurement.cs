using System;

public class Measurement
{
    public uint timestamp;
    public int valueRaw;
    public float valueScaled;
    public bool al_active;
    public bool ah_active;

    public Measurement(uint timestamp, int valueRaw, float valueScaled, bool al_active, bool ah_active)
    {
        this.timestamp = timestamp;
        this.valueRaw = valueRaw;
        this.valueScaled = valueScaled;
        this.al_active = al_active;
        this.ah_active = ah_active;
    }

    

    public override string ToString()
    {
        string returnString = "timestamp: " + CommonUtilities.GetTimestampAsDatetime(timestamp);
        returnString += "\nvalueRaw: " + valueRaw;
        returnString += "\nvalueScaled: " + valueScaled;
        returnString += "\nal_active: " + al_active;
        returnString += "\nah_active: " + ah_active;
        return returnString;
    }
}
