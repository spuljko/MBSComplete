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
    using System.Drawing;
    using Placa;
    using System.Resources;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [SmartPart]
    public class RAD1SPOLWorkWith : UserControl, ISmartPartInfoProvider, IFilteredView
    {
        [AccessedThroughProperty("userControlDataGridRAD1SPOL")]
        private RAD1SPOLUserDataGrid _userControlDataGridRAD1SPOL;
        private IContainer components = null;
        private SmartPartInfoProvider infoProvider;
        private string m_FillByMethod = "";
        private DataRow m_FillByRow;
        private DataRow m_RowSelected;
        private Deklarit.Practices.CompositeUI.WorkWithMode m_WorkWithMode;
        private SmartPartInfo smartPartInfo1;
        private UltraGridPrintDocument ultraGridPrintDocument1;
        private UltraPrintPreviewDialog ultraPrintPreviewDialog1;

        public RAD1SPOLWorkWith()
        {
            this.InitializeComponent();
            this.smartPartInfo1 = new SmartPartInfo(string.Format(Deklarit.Resources.Resources.WorkWithTitle, StringResources.RAD1SPOLDescription), string.Format(Deklarit.Resources.Resources.WorkWithTitle, StringResources.RAD1SPOLDescription));
            this.infoProvider = new SmartPartInfoProvider();
            this.infoProvider.Items.Add(this.smartPartInfo1);
            this.userControlDataGridRAD1SPOL.DataGrid.AfterRowActivate += new EventHandler(this.DataGrid_AfterRowActivate);
        }

        [OnBuiltUp]
        public void AddSelectedRowProviderService()
        {
            UltraGridSelectedRowsProviderService<RAD1SPOLDataSet> serviceInstance = new UltraGridSelectedRowsProviderService<RAD1SPOLDataSet>(this.userControlDataGridRAD1SPOL.DataGrid);
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
            if (!this.userControlDataGridRAD1SPOL.DataGrid.GridLoading && ((this.userControlDataGridRAD1SPOL.DataGrid.ActiveRow != null) && (this.userControlDataGridRAD1SPOL.DataGrid.ActiveCell != null)))
            {
                this.DefaultAction();
            }
        }

        private void DataGrid_SetFocus()
        {
            if ((this.userControlDataGridRAD1SPOL.DataGrid.Rows.Count > 0) && (this.userControlDataGridRAD1SPOL.DataGrid.Rows[0] != null))
            {
                this.userControlDataGridRAD1SPOL.DataGrid.Rows[0].Activate();
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
                    new UltraGridExcelExporter().Export(this.userControlDataGridRAD1SPOL.DataGrid, dialog.FileName);
                    Process.Start(dialog.FileName);
                }
            }
        }

        [LocalCommandHandler("FillAll")]
        public void FillAll(object sender, EventArgs e)
        {
            if (this.Controller.WorkItem.Status == WorkItemStatus.Active)
            {
                bool fillByPage = this.userControlDataGridRAD1SPOL.DataGrid.FillByPage;
                this.userControlDataGridRAD1SPOL.DataGrid.FillByPage = false;
                this.FillData();
                this.userControlDataGridRAD1SPOL.DataGrid.FillByPage = fillByPage;
            }
        }

        public virtual void FillData()
        {
            try
            {
                string fillByMethod = this.m_FillByMethod;
                this.userControlDataGridRAD1SPOL.FillMethod = "Fill";
                this.userControlDataGridRAD1SPOL.Fill();
                if (this.m_WorkWithMode == Deklarit.Practices.CompositeUI.WorkWithMode.SelectionList)
                {
                    ((RAD1SPOLSelectionListWorkItem) this.Controller.WorkItem).DataSet = this.userControlDataGridRAD1SPOL.DataView.Table.DataSet;
                }
                else
                {
                    ((RAD1SPOLWorkWithWorkItem) this.Controller.WorkItem).DataSet = this.userControlDataGridRAD1SPOL.DataView.Table.DataSet;
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
            ResourceManager manager = new ResourceManager(typeof(RAD1SPOLWorkWith));
            this.userControlDataGridRAD1SPOL = new RAD1SPOLUserDataGrid();
            this.ultraGridPrintDocument1 = new UltraGridPrintDocument(this.components);
            this.ultraPrintPreviewDialog1 = new UltraPrintPreviewDialog(this.components);
            this.SuspendLayout();
            this.userControlDataGridRAD1SPOL.Name = "userControlDataGridRAD1SPOL";
            this.userControlDataGridRAD1SPOL.Dock = DockStyle.Fill;
            this.userControlDataGridRAD1SPOL.DockPadding.All = 5;
            this.userControlDataGridRAD1SPOL.FillAtStartup = false;
            this.userControlDataGridRAD1SPOL.TabIndex = 6;
            Size size = new System.Drawing.Size(0x2d8, 400);
            this.userControlDataGridRAD1SPOL.Size = size;
            this.ultraGridPrintDocument1.Grid = this.userControlDataGridRAD1SPOL.DataGrid;
            this.Controls.Add(this.userControlDataGridRAD1SPOL);
            SizeF ef = new System.Drawing.SizeF(6f, 13f);
            this.AutoScaleDimensions = ef;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DockPadding.All = 5;
            this.Name = "RAD1SPOLWorkWith";
            this.Text = "Work With RAD1SPOL";
            this.Load += new EventHandler(this.RAD1SPOLWorkWith_Load);
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

        private void RAD1SPOLWorkWith_Load(object sender, EventArgs e)
        {
            if (this.m_WorkWithMode == Deklarit.Practices.CompositeUI.WorkWithMode.SelectionList)
            {
                this.userControlDataGridRAD1SPOL.DataGrid.DoubleClick += new EventHandler(this.Select_Click);
                Size size = new System.Drawing.Size(600, 500);
                this.Parent.Parent.Size = size;
            }
            else
            {
                this.userControlDataGridRAD1SPOL.DataGrid.DoubleClickRow += new DoubleClickRowEventHandler(this.DataGrid_DoubleClickRow);
            }
            this.userControlDataGridRAD1SPOL.DataGrid.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
            this.userControlDataGridRAD1SPOL.DataGrid.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
            this.userControlDataGridRAD1SPOL.DataGrid.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
            this.userControlDataGridRAD1SPOL.DataGrid.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
            this.userControlDataGridRAD1SPOL.DataGrid.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortSingle;
            this.userControlDataGridRAD1SPOL.FillByPage = false;
            this.FillData();
        }

        [EventSubscription("topic://NetAdvantage/BusinessComponentUpdated/RAD1SPOL", Thread=ThreadOption.UserInterface)]
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
            UIElement lastElementEntered = this.userControlDataGridRAD1SPOL.DataGrid.DisplayLayout.UIElement.LastElementEntered;
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
                this.smartPartInfo1.Title = string.Format("{0} RAD1SPOL ", Deklarit.Resources.Resources.Select);
            }
            else
            {
                this.smartPartInfo1.Title = string.Format("{0} RAD1SPOL ", Deklarit.Resources.Resources.Workwith);
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

        [LocalCommandHandler("View")]
        public void View(object sender, EventArgs e)
        {
            if (this.InValidState())
            {
                this.Controller.View(this.CurrentDataRow);
            }
        }

        [CreateNew]
        public RAD1SPOLWorkWithController Controller { get; set; }

        private DataRow CurrentDataRow
        {
            get
            {
                if (this.userControlDataGridRAD1SPOL.DataGrid.CurrentRowIndex == -1)
                {
                    return null;
                }
                return this.userControlDataGridRAD1SPOL.DataView[this.userControlDataGridRAD1SPOL.DataGrid.CurrentRowIndex].Row;
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

        private RAD1SPOLUserDataGrid userControlDataGridRAD1SPOL
        {
            [DebuggerNonUserCode]
            get
            {
                return this._userControlDataGridRAD1SPOL;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                this._userControlDataGridRAD1SPOL = value;
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

