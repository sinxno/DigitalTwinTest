using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInstrumentUI : MonoBehaviour
{
    public WindowGraph graphUI;
    public void ShowUI(Instrument instrument, List<Measurement> measurements)
    {
        
        graphUI.CreateGraph(measurements, instrument);

    }

    public void HideUI()
    {
        graphUI.HideGraph();
    }
}
