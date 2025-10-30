#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.UI;
using FTOptix.HMIProject;
using FTOptix.NativeUI;
using FTOptix.CoreBase;
using FTOptix.System;
using FTOptix.Retentivity;
using FTOptix.NetLogic;
using FTOptix.SerialPort;
using FTOptix.UI;
using FTOptix.Core;
using FTOptix.WebUI;
using FTOptix.S7TiaProfinet;
using FTOptix.S7TCP;
using FTOptix.CommunicationDriver;
using FTOptix.CODESYS;
using FTOptix.Alarm;
using FTOptix.EventLogger;
using FTOptix.Store;
using FTOptix.SQLiteStore;
using FTOptix.DataLogger;
#endregion

public class CreaAllarmi : BaseNetLogic
{
    [ExportMethod]
    public void CreaAllarmiDaArray()
    {
        // Insert code to be executed by the method
        var array = Project.Current.GetVariable("Model/Allarmi");
        for (uint i = 0; i < 10; i++)
        {
            var mioAllarme = InformationModel.Make<DigitalAlarm>("Allarme_" + i);
            mioAllarme.Message = "Testo allarme " + i;
            mioAllarme.InputValueVariable.SetDynamicLink(array, i, DynamicLinkMode.ReadWrite);
            Project.Current.Get("Alarms/AllarmiGenerati").Add(mioAllarme);
        }

    }
}
