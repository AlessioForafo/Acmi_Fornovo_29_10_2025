#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.UI;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.NativeUI;
using FTOptix.WebUI;
using FTOptix.CoreBase;
using FTOptix.Alarm;
using FTOptix.EventLogger;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.S7TiaProfinet;
using FTOptix.S7TCP;
using FTOptix.CODESYS;
using FTOptix.System;
using FTOptix.Retentivity;
using FTOptix.CommunicationDriver;
using FTOptix.SerialPort;
using FTOptix.UI;
using FTOptix.Core;
using FTOptix.DataLogger;
#endregion

public class RuntimeNetLogic1 : BaseNetLogic
{
    private IUAVariable modello;
    private PeriodicTask mioPeriodicTask;

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        Log.Info("pagina aperta");
        modello = LogicObject.GetVariable("LinkedVariable");
        modello.VariableChange += Modello_VariableChange;

        mioPeriodicTask = new PeriodicTask(IncrementaTag1, 2000, LogicObject);
        mioPeriodicTask.Start();

        for (int i = 0; i < 10; i++)
        { 
           var oggettiCustom = InformationModel.Make<ACMI_Toggle_Button>("Acmi_" +  i);
            Owner.Get("HorizontalLayout1").Add(oggettiCustom);
        }


    }

    private void Modello_VariableChange(object sender, VariableChangeEventArgs e)
    {
       if (e.NewValue > 100)
        {
            var mioRettangolo = Owner.Get<Rectangle>("Rectangle1");
            mioRettangolo.FillColor = Colors.Blue;
        }
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        Log.Info("pagina chiusa");
        modello.VariableChange -= Modello_VariableChange;
        mioPeriodicTask.Dispose();
    }

    [ExportMethod]
    public void mioMetodo()
    {
        Log.Info("mio metodo");
        
    }

    [ExportMethod]
    public void IncrementaTag(int incremento)
    {
        //var modello = Project.Current.GetVariable("Model/VarTest");
        
        modello.Value += incremento;
    }

    [ExportMethod]
    public void IncrementaTag1()
    {
        //var modello = Project.Current.GetVariable("Model/VarTest");

        modello.Value += 1;
    }
}
