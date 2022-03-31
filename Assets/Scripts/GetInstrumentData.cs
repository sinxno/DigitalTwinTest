using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class GetInstrumentData : MonoBehaviour
{
    public string instrumentName;
    public string phpUrl;

    private string url;

    private Instrument instrument;

    private void Awake()
    {
        url = phpUrl + "?name=" + instrumentName;
        instrument = new Instrument(instrumentName);
    }

    private void OnTriggerEnter(Collider other)
    {
        string response = GetInstrumentDataFromDatabase();
        AddDataToInstrument(response);

        Debug.Log(instrument.ToString());

    }
    
    private string GetInstrumentDataFromDatabase()
    {
        string response;
        using (WebClient client = new WebClient())
        {
            response = client.DownloadString(url);
        }

        return response;
    }
    
    private void AddDataToInstrument(string response)
    {
        string[] splittedResponse = response.Split(',');
        instrument.LRV = CommonUtilities.TestFloatFromString(splittedResponse[0]);
        instrument.URV = CommonUtilities.TestFloatFromString(splittedResponse[1]);
        instrument.AL = CommonUtilities.TestFloatFromString(splittedResponse[2]);
        instrument.AH = CommonUtilities.TestFloatFromString(splittedResponse[3]);
    }

    public Instrument GetInstrument()
    {
        return instrument;
    }

    
}
