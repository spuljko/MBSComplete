﻿namespace NetAdvantage.SmartParts
{
    using Deklarit;
    using Deklarit.Practices.CompositeUI;
    using Deklarit.Practices.CompositeUI.BuilderStrategies;
    using Deklarit.Practices.CompositeUI.NetAdvantage.Services;
    using Deklarit.Practices.CompositeUI.Services;
    using Deklarit.Resources;
    using Infragistics.Win;
    using Infragistics.Win.Printing;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win.UltraWinGrid.ExcelExport;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using NetAdvantage.Controllers;
    using NetAdvantage.WorkItems;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using Placa;
    using System.Drawing;
    using System.Resources;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [SmartPart]
    public class VERZIJAWorkWith : UserControl, ISmartPartInfoProvider, IFilteredView
    {
        [AccessedThroughProperty("userControlDataGridVERZIJA")]
        private VERZIJAUserDataGrid _userControlDataGridVERZIJA;
        private IContainer components = null;
        private SmartPartInfoProvider infoProvider;
        private string m_FillByMethod = "";
        private DataRow m_FillByRow;
        private DataRow m_RowSelected;
        private Deklarit.Practices.CompositeUI.WorkWithMode m_WorkWithMode;
        private SmartPartInfo smartPartInfo1;
        private UltraGridPrintDocument ultraGridPrintDocument1;
        private UltraPrintPreviewDialog ultraPrintPreviewDialog1;

        public VERZIJAWorkWith()
        {
            this.InitializeComponent();
            this.smartPartInfo1 = new SmartPartInfo(string.Format(Deklarit.Resources.Resources.WorkWithTitle, StringResources.VERZIJADescription), string.Format(Deklarit.Resources.Resources.WorkWithTitle, StringResources.VERZIJADescription));
            this.infoProvider = new SmartPartInfoProvider();
            this.infoProvider.Items.Add(this.smartPartInfo1);
            this.userControlDataGridVERZIJA.DataGrid.AfterRowActivate += new EventHandler(this.DataGrid_AfterRowActivate);
        }

        [OnBuiltUp]
        public void AddSelectedRowProviderService()
        {
            UltraGridSelectedRowsProviderService<VERZIJADataSet> serviceInstance = new UltraGridSelectedRowsProviderService<VERZIJADataSet>(this.userControlDataGridVERZIJA.DataGrid);
            this.Controller.WorkItem.Services.Add(typeof(ISelectedRowsProviderService), serviceInstance);
        }

        [LocalCommandHandler("Copy")]
        public void CopyHandler(object sender, EventArgs e)
        {
            if (this.InValidState())
            {
                this.Controller.Copy(this.CurrentDataRow);
            }
        }

        private void DataGrid_AfterRowActivate(object sender, EventArgs e)
        {
            if (this.CurrentDataRow != null)
            {
                this.Controller.CurrentRow = this.CurrentDataRow;
            }
        }

        private void DataGrid_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            if (!this.userControlDataGridVERZIJA.DataGrid.GridLoading && ((this.userControlDataGridVERZIJA.DataGrid.ActiveRow != null) && (this.userControlDataGridVERZIJA.DataGrid.ActiveCell != null)))
            {
                this.DefaultAction();
            }
        }

        private void DataGrid_SetFocus()
        {
            if ((this.userControlDataGridVERZIJA.DataGrid.Rows.Count > 0) && (this.userControlDataGridVERZIJA.DataGrid.Rows[0] != null))
            {
                this.userControlDataGridVERZIJA.DataGrid.Rows[0].Activate();
            }
        }

        private void DefaultAction()
        {
            if (this.InValidState())
            {
                this.Controller.Update(this.CurrentDataRow);
            }
        }

        [LocalCommandHandler("Delete")]
        public void DeleteHandler(object sender, EventArgs e)
        {
            if (this.InValidState())
            {
                this.Controller.Delete(this.CurrentDataRow);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        [LocalCommandHandler("ExportExcel")]
        public void ExportExcelHandler(object sender, EventArgs e)
        {
            if (this.Controller.WorkItem.Status == WorkItemStatus.Active)
            {
                SaveFileDialog dialog = new SaveFileDialog {
                    DefaultExt = "xls",
                    Filter = "Excel file (*.xls)|*.xls"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    new UltraGridExcelExporter().Export(this.userControlDataGridVERZIJA.DataGrid, dialog.FileName);
                    Process.Start(dialog.FileName);
                }
            }
        }

        [LocalCommandHandler("FillAll")]
        public void FillAll(object sender, EventArgs e)
        {
            if (this.Controller.WorkItem.Status == WorkItemStatus.Active)
            {
                bool fillByPage = this.userControlDataGridVERZIJA.DataGrid.FillByPage;
                this.userControlDataGridVERZIJA.DataGrid.FillByPage = false;
                this.FillData();
                this.userControlDataGridVERZIJA.DataGrid.FillByPage = fillByPage;
            }
        }

        public virtual void FillData()
        {
            try
            {
                string fillByMethod = this.m_FillByMethod;
                this.userControlDataGridVERZIJA.FillMethod = "Fill";
                this.userControlDataGridVERZIJA.Fill();
                if (this.m_WorkWithMode == Deklarit.Practices.CompositeUI.WorkWithMode.SelectionList)
                {
                    ((VERZIJASelectionListWorkItem) this.Controller.WorkItem).DataSet = this.userControlDataGridVERZIJA.DataView.Table.DataSet;
                }
                else
                {
                    ((VERZIJAWorkWithWorkItem) this.Controller.WorkItem).DataSet = this.userControlDataGridVERZIJA.DataView.Table.DataSet;
                }
            }
            catch (System.Exception exception1)
            {
                throw exception1;
                
                
                
            }
        }

        public ISmartPartInfo GetSmartPartInfo(Type smartPartInfoType)
        {
            return this.infoProvider.GetSmartPartInfo(smartPartInfoType);
        }

        private object GetValueFromRow(string fieldSuperTypeName, string fieldName)
        {
            if (this.m_FillByRow.Table.Columns.Contains(fieldSuperTypeName))
            {
                return this.m_FillByRow[fieldSuperTypeName];
            }
            return this.m_FillByRow[fieldName];
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new Container();
            ResourceManager manager = new ResourceManager(typeof(VERZIJAWorkWith));
            this.userControlDataGridVERZIJA = new VERZIJAUserDataGrid();
            this.ultraGridPrintDocument1 = new UltraGridPrintDocument(this.components);
            this.ultraPrintPreviewDialog1 = new UltraPrintPreviewDialog(this.components);
            this.SuspendLayout();
            this.userControlDataGridVERZIJA.Name = "userControlDataGridVERZIJA";
            this.userControlDataGridVERZIJA.Dock = DockStyle.Fill;
            this.userControlDataGridVERZIJA.DockPadding.All = 5;
            this.userControlDataGridVERZIJA.FillAtStartup = false;
            this.userControlDataGridVERZIJA.TabIndex = 6;
            Size size = new System.Drawing.Size(0x2d8, 400);
            this.userControlDataGridVERZIJA.Size = size;
            this.ultraGridPrintDocument1.Grid = this.userControlDataGridVERZIJA.DataGrid;
            this.Controls.Add(this.userControlDataGridVERZIJA);
            SizeF ef = new System.Drawing.SizeF(6f, 13f);
            this.AutoScaleDimensions = ef;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DockPadding.All = 5;
            this.Name = "VERZIJAWorkWith";
            this.Text = "Work With VERZIJA";
            this.Load += new EventHandler(this.VERZIJAWorkWith_Load);
            this.ResumeLayout(false);
        }

        [LocalCommandHandler("Insert")]
        public void InsertHandler(object sender, EventArgs e)
        {
            if (this.Controller.WorkItem.Status == WorkItemStatus.Active)
            {
                this.Controller.Insert(this.m_FillByRow);
            }
        }

        private bool InValidState()
        {
            return ((this.Controller.WorkItem.Status == WorkItemStatus.Active) && (this.CurrentDataRow != null));
        }

        [LocalCommandHandler("Print")]
        public void PrintHandler(object sender, EventArgs e)
        {
            if (Information.IsNothing(this.ultraPrintPreviewDialog1.Document))
            {
                this.ultraPrintPreviewDialog1.Document = this.ultraGridPrintDocument1;
                this.ultraPrintPreviewDialog1.Name = "ultraPrintPreviewDialog1";
                this.ultraPrintPreviewDialog1.Document.DocumentName = "";
                this.ultraPrintPreviewDialog1.Text = "";
            }
            if (this.Controller.WorkItem.Status == WorkItemStatus.Active)
            {
                this.ultraPrintPreviewDialog1.ShowDialog(this);
            }
        }

        [EventSubscription("topic://NetAdvantage/BusinessComponentUpdated/VERZIJA", Thread=ThreadOption.UserInterface)]
        public void RefreshGrid(object sender, ComponentEventArgs args)
        {
            this.FillData();
        }

        [LocalCommandHandler("Refresh")]
        public void RefreshHandler(object sender, EventArgs e)
        {
            if (this.Controller.WorkItem.Status == WorkItemStatus.Active)
            {
                this.FillData();
            }
        }

        private void Select_Click(object sender, EventArgs e)
        {
            RowUIElement ancestor;
            UIElement lastElementEntered = this.userControlDataGridVERZIJA.DataGrid.DisplayLayout.UIElement.LastElementEntered;
            if (lastElementEntered is RowUIElement)
            {
                ancestor = (RowUIElement) lastElementEntered;
            }
            else
            {
                ancestor = (RowUIElement) lastElementEntered.GetAncestor(typeof(RowUIElement));
            }
            if (ancestor != null)
            {
                this.SelectHandler(RuntimeHelpers.GetObjectValue(sender), e);
            }
        }

        [LocalCommandHandler("Select")]
        public void SelectHandler(object sender, EventArgs e)
        {
            if (this.Controller.WorkItem.Status == WorkItemStatus.Active)
            {
                this.m_RowSelected = this.CurrentDataRow;
                this.ParentForm.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void SetSmartPartInfoInformation()
        {
            if (this.m_WorkWithMode == Deklarit.Practices.CompositeUI.WorkWithMode.SelectionList)
            {
                this.smartPartInfo1.Title = string.Format("{0} VERZIJA ", Deklarit.Resources.Resources.Select);
            }
            else
            {
                this.smartPartInfo1.Title = string.Format("{0} VERZIJA ", Deklarit.Resources.Resources.Workwith);
            }
            this.smartPartInfo1.Description = this.smartPartInfo1.Title;
        }

        private void ShowMessage(string msg)
        {
            MessageBox.Show(msg, this.smartPartInfo1.Description, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        [LocalCommandHandler("Update")]
        public void UpdateHandler(object sender, EventArgs e)
        {
            if (this.InValidState())
            {
                this.Controller.Update(this.CurrentDataRow);
            }
        }

        private void VERZIJAWorkWith_Load(object sender, EventArgs e)
        {
            if (this.m_WorkWithMode == Deklarit.Practices.CompositeUI.WorkWithMode.SelectionList)
            {
                this.userControlDataGridVERZIJA.DataGrid.DoubleClick += new EventHandler(this.Select_Click);
                Size size = new System.Drawing.Size(600, 500);
                this.Parent.Parent.Size = size;
            }
            else
            {
                this.userControlDataGridVERZIJA.DataGrid.DoubleClickRow += new DoubleClickRowEventHandler(this.DataGrid_DoubleClickRow);
            }
            this.userControlDataGridVERZIJA.DataGrid.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
            this.userControlDataGridVERZIJA.DataGrid.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
            this.userControlDataGridVERZIJA.DataGrid.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
            this.userControlDataGridVERZIJA.DataGrid.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
            this.userControlDataGridVERZIJA.DataGrid.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortSingle;
            this.FillData();
        }

        [LocalCommandHandler("View")]
        public void View(object sender, EventArgs e)
        {
            if (this.InValidState())
            {
                this.Controller.View(this.CurrentDataRow);
            }
        }

        [CreateNew]
        public VERZIJAWorkWithController Controller { get; set; }

        private DataRow CurrentDataRow
        {
            get
            {
                if (this.userControlDataGridVERZIJA.DataGrid.CurrentRowIndex == -1)
                {
                    return null;
                }
                return this.userControlDataGridVERZIJA.DataView[this.userControlDataGridVERZIJA.DataGrid.CurrentRowIndex].Row;
            }
        }

        DataRow Deklarit.Practices.CompositeUI.IFilteredView.FillByRow
        {
            set
            {
                this.m_FillByRow = value;
                this.SetSmartPartInfoInformation();
            }
        }

        string Deklarit.Practices.CompositeUI.IFilteredView.FillMethod
        {
            set
            {
                this.m_FillByMethod = value;
            }
        }

        DataRow Deklarit.Practices.CompositeUI.IFilteredView.SelectedRow
        {
            get
            {
                return this.m_RowSelected;
            }
        }

        Deklarit.Practices.CompositeUI.WorkWithMode Deklarit.Practices.CompositeUI.IFilteredView.WorkWithMode
        {
            set
            {
                this.m_WorkWithMode = value;
                this.SetSmartPartInfoInformation();
            }
        }

        public DataRow FillByRow
        {
            set
            {
                this.m_FillByRow = value;
                this.SetSmartPartInfoInformation();
            }
        }

        public string FillMethod
        {
            set
            {
                this.m_FillByMethod = value;
            }
        }

        public DataRow SelectedRow
        {
            get
            {
                return this.m_RowSelected;
            }
        }

        private VERZIJAUserDataGrid userControlDataGridVERZIJA
        {
            [DebuggerNonUserCode]
            get
            {
                return this._userControlDataGridVERZIJA;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                this._userControlDataGridVERZIJA = value;
            }
        }

        Deklarit.Practices.CompositeUI.WorkWithMode WorkWithMode
        {
            set
            {
                this.m_WorkWithMode = value;
                this.SetSmartPartInfoInformation();
            }
        }
    }
}

