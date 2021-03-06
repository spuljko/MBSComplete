﻿namespace NetAdvantage.Controllers
{
    using Deklarit.Practices.CompositeUI;
    using Deklarit.Practices.CompositeUI.Controllers;
    using Deklarit.Practices.CompositeUI.WorkItems;
    using NetAdvantage.WorkItems;
    using Placa;
    using System;
    using System.Data;

    public class TITULAController : FormControllerBase
    {
        public override void DoUpdate()
        {
            DataAdapterFactory.GetTITULADataAdapter().Update(this.DataSet);
        }

        public void NewRADNIK(DataRow row)
        {
            RADNIKWorkItem item = this.WorkItem.Items.AddNew<RADNIKWorkItem>();
            item.RootForm.Show(DeklaritMode.Insert, row, false);
            item.Terminate();
            this.WorkItem.Activate();
        }

        public void ViewRADNIK(DataRow row)
        {
            RADNIKSelectionListWorkItem item = this.WorkItem.Items.AddNew<RADNIKSelectionListWorkItem>();
            item.ShowModal(false, "FillByIDTITULA", row);
            item.Terminate();
            this.WorkItem.Activate();
        }

        public TITULADataSet DataSet
        {
            get
            {
                return (TITULADataSet) this.State["InstanceDataSet"];
            }
            set
            {
                this.State["InstanceDataSet"] = value;
            }
        }

        public FormDefinition TITULAFormDefinition
        {
            get
            {
                return this.TITULAWorkItem.TITULA;
            }
        }

        public NetAdvantage.WorkItems.TITULAWorkItem TITULAWorkItem
        {
            get
            {
                return (NetAdvantage.WorkItems.TITULAWorkItem) this.WorkItem;
            }
        }
    }
}

