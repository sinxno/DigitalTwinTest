using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;


public class GetMeasurementData : MonoBehaviour
{
    
    public string phpUrl;
    public int amountOfMeasurementPoints = 20;

    private Instrument instrument;
    private string url;
    private ShowInstrumentUI showInstrumentUI;

    void Start()//Bruker start fordi Awake kjører før start. hvis vi bruker start her er det ikke sikkert instrument objektet er opprettet.
    {
        instrument = GetComponent<GetInstrumentData>().GetInstrument();
        showInstrumentUI = GetComponent<ShowInstrumentUI>();

        url = phpUrl + "?name=" + instrument.instrumentName + "&amount="+amountOfMeasurementPoints;
    }

    private void OnTriggerEnter(Collider other)
    {
        string[] responseLines = GetMeasurementsFromDatabase();
        AddMeasurementsToMeasurementList(responseLines);
        showInstrumentUI.ShowUI(instrument);
    }

    private void OnTriggerExit(Collider other)
    {
        showInstrumentUI.HideUI();
    }

    private string[] GetMeasurementsFromDatabase()
    {
        string response;
        using (WebClient client = new WebClient())
        {
            response = client.DownloadString(url);
        }
        string[] responseLines = response.Split(';');
        return responseLines;
    }

    private void AddMeasurementsToMeasurementList(string[] responseLines)
    {
        instrument.ClearMeasurementList();
        foreach (string line in responseLines)
        {
            if (line.Length > 0)
            {

                string[] parts = line.Split(',');
                Measurement m = CreateMeasurement(parts);
                instrument.AddMeasurementToList(m);
            }
        }
    }

    private static Measurement CreateMeasurement(string[] parts)
    {
        int timestamp = CommonUtilities.TestIntFromString(parts[0]);
        int valueRaw = CommonUtilities.TestIntFromString(parts[1]);
        float valueScaled = CommonUtilities.TestFloatFromString(parts[2]);
        bool al_active = CommonUtilities.ConvertIntToBool(CommonUtilities.TestIntFromString(parts[3]));
        bool ah_active = CommonUtilities.ConvertIntToBool(CommonUtilities.TestIntFromString(parts[4]));
        Measurement m = new Measurement((uint)timestamp, valueRaw, valueScaled, al_active, ah_active);
        return m;
    }

}
