﻿namespace NetAdvantage.Controls
{
    using Deklarit;
    using Deklarit.Controls;
    using Deklarit.Resources;
    using Infragistics.Win.UltraWinGrid;
    using Microsoft.VisualBasic.CompilerServices;
    using Placa;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Globalization;
    using System.Reflection;
    using System.Security.Principal;
    using System.Threading;
    using System.Windows.Forms;

    public class URADataGrid : DeklaritDataGrid
    {
        private int currentRow;
        private URADataSet dsURADataSet1 = new URADataSet();
        private System.Data.DataView dv1;
        private int m_FillByIDTIPURAIDTIPURA;
        private bool m_FillByPage = false;
        private int m_FillByURADOKIDDOKUMENTURADOKIDDOKUMENT;
        private int m_FillByURAGODIDGODINEURADOKIDDOKUMENTURADOKIDDOKUMENT;
        private short m_FillByURAGODIDGODINEURADOKIDDOKUMENTURAGODIDGODINE;
        private short m_FillByURAGODIDGODINEURAGODIDGODINE;
        private int m_FillByurapartnerIDPARTNERUrapartnerIDPARTNER;
        private string m_FillMethod = "Fill";
        private bool m_GridLoading;
        private int m_PageSize = 60;
        private int m_StartRow;
        private int startRowThread;
        private const int WM_SETREDRAW = 11;

        public URADataGrid()
        {
            this.BindDataGrid();
            DeklaritDataGrid.myFillMethods = new TypeConverter.StandardValuesCollection(new string[] { "Fill", "FillByURADOKIDDOKUMENT", "FillByurapartnerIDPARTNER", "FillByIDTIPURA", "FillByURAGODIDGODINE", "FillByURAGODIDGODINEURADOKIDDOKUMENT" });
            this.AfterRowRegionScroll += new RowScrollRegionEventHandler(this.Grid_AfterRowRegionScroll);
            this.AfterSelectChange += new AfterSelectChangeEventHandler(this.Grid_AfterSelectChange);
        }

        private void BindDataGrid()
        {
            this.dv1 = new System.Data.DataView();
            this.dv1.Table = this.dsURADataSet1.URA;
            BindingSource source = new BindingSource(this.dv1, "");
            this.DataSource = source;
        }

        private void ContinuePainting()
        {
            DeklaritDataGrid.NativeMethods.SendMessage(this.Handle, 11, 1, (int) IntPtr.Zero);
            this.Invalidate(true);
            this.Update();
        }

        public virtual void Fill()
        {
            this.PreFill();
            this.dsURADataSet1 = new URADataSet();
            this.m_StartRow = 0;
            this.Cursor = Cursors.WaitCursor;
            this.m_GridLoading = true;
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.FillDataThread), Thread.CurrentPrincipal);
        }

        private void FillDataThread(object state)
        {
            Thread.CurrentPrincipal = (IPrincipal) state;
            try
            {
                if (this.FillByPage)
                {
                    if (this.FillMethod == "Fill")
                    {
                        this.DataAdapter.FillPage(this.dsURADataSet1, 0, this.PageSize);
                    }
                    else
                    {
                        MethodInfo info = this.DataAdapter.GetType().GetMethod("FillPageBy" + this.FillMethod.Substring(6));
                        this.InvokeFillByMethod(this.FillMethod, (IDataAdapter) this.DataAdapter, this.dsURADataSet1, info, 0, this.PageSize);
                    }
                }
                else if (this.FillMethod == "Fill")
                {
                    this.DataAdapter.Fill(this.dsURADataSet1);
                }
                else
                {
                    MethodInfo info2 = this.DataAdapter.GetType().GetMethod(this.FillMethod);
                    this.InvokeFillByMethod(this.FillMethod, (IDataAdapter) this.DataAdapter, this.dsURADataSet1, info2);
                }
                MethodInvoker method = new MethodInvoker(this.ResumeBindingPostThreadFirst);
                if (this.InvokeRequired)
                {
                    this.Invoke(method);
                }
                else
                {
                    this.ResumeBindingPostThreadFirst();
                }
            }
            catch (System.Exception exception1)
            {
                throw exception1;
                
                
                
            }
        }

        private void Grid_AfterRowRegionScroll(object sender, RowScrollRegionEventArgs e)
        {
            UltraGridRow firstRow = this.ActiveRowScrollRegion.FirstRow;
            if ((!(firstRow is UltraGridGroupByRow) && !this.GridLoading) && ((firstRow.Index + this.ActiveRowScrollRegion.VisibleRows.Count) >= this.Rows.Count))
            {
                this.m_StartRow += this.m_PageSize;
                if ((this.m_StartRow < ((this.m_PageSize + this.Rows.Count) + 1)) && (this.m_StartRow >= this.Rows.Count))
                {
                    this.Refill(this.m_StartRow);
                }
            }
        }

        private void Grid_AfterSelectChange(object sender, AfterSelectChangeEventArgs args)
        {
            if (this.ActiveRow != null)
            {
                this.ActiveRow.Selected = true;
            }
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            if (!this.DesignMode)
            {
                this.MyLoadData();
            }
        }

        public virtual void MyLoadData()
        {
            if (this.FillAtStartup)
            {
                this.Fill();
            }
        }

        private void PreFill()
        {
            this.currentRow = this.CurrentRowIndex;
            this.SuspendLayout();
            this.BindingContext[this.DataView].SuspendBinding();
            this.BeginUpdate();
        }

        public virtual void Refill(int startRow)
        {
            this.PreFill();
            this.Cursor = Cursors.WaitCursor;
            this.m_GridLoading = true;
            this.StopPainting();
            this.startRowThread = startRow;
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.ReFillDataThread), Thread.CurrentPrincipal);
        }

        private void ReFillDataThread(object state)
        {
            Thread.CurrentPrincipal = (IPrincipal) state;
            try
            {
                if (this.FillByPage)
                {
                    if (this.FillMethod == "Fill")
                    {
                        this.DataAdapter.FillPage(this.dsURADataSet1, this.startRowThread, this.PageSize);
                    }
                    else
                    {
                        MethodInfo info = this.DataAdapter.GetType().GetMethod("FillPageBy" + this.FillMethod.Substring(6));
                        this.InvokeFillByMethod(this.FillMethod, (IDataAdapter) this.DataAdapter, this.dsURADataSet1, info, this.startRowThread, this.PageSize);
                    }
                }
                MethodInvoker method = new MethodInvoker(this.ResumeBindingPostThread);
                if (this.InvokeRequired)
                {
                    this.Invoke(method);
                }
                else
                {
                    this.ResumeBindingPostThread();
                }
            }
            catch (System.Exception exception1)
            {
                throw exception1;
                
                
                
            }
        }

        private void ResumeBindingPostThread()
        {
            this.Cursor = Cursors.Default;
            this.EndUpdate();
            this.BindDataGrid();
            this.Text = "URA : " + this.DataView.Count.ToString(CultureInfo.InvariantCulture) + " " + Deklarit.Resources.Resources.Rows;
            this.BindingContext[this.DataView].ResumeBinding();
            this.ResumeLayout();
            this.ActiveRow = this.Rows.GetRowWithListIndex(this.currentRow);
            this.m_GridLoading = false;
            this.ContinuePainting();
        }

        private void ResumeBindingPostThreadFirst()
        {
            this.ResumeBindingPostThread();
            ColumnEnumerator enumerator = this.DisplayLayout.Bands[0].Columns.GetEnumerator();
            while (enumerator.MoveNext())
            {
                enumerator.Current.PerformAutoResize();
            }
        }

        private void StopPainting()
        {
            DeklaritDataGrid.NativeMethods.SendMessage(this.Handle, 11, 0, (int) IntPtr.Zero);
        }

        public int CurrentRowIndex
        {
            get
            {
                if (this.ActiveRow == null)
                {
                    return -1;
                }
                UltraGridRow activeRow = this.ActiveRow;
                while ((activeRow.ParentRow != null) && !(activeRow.ParentRow is UltraGridGroupByRow))
                {
                    activeRow = activeRow.ParentRow;
                }
                return activeRow.ListIndex;
            }
        }

        [Category("Deklarit Work With "), Browsable(false)]
        public virtual IURADataAdapter DataAdapter
        {
            get
            {
                if (DataAdapterFactory.Provider == null)
                {
                    DataAdapterFactory.Provider = new SimpleDataAdapterFactory();
                }
                if (DataAdapterFactory.Provider != null)
                {
                    return DataAdapterFactory.GetURADataAdapter();
                }
                return null;
            }
        }

        [Browsable(false), Category("Deklarit Work With ")]
        public virtual URADataSet DataSet
        {
            get
            {
                return this.dsURADataSet1;
            }
            set
            {
                this.dsURADataSet1 = value;
            }
        }

        [Category("Deklarit Work With "), Browsable(false)]
        public virtual System.Data.DataView DataView
        {
            get
            {
                return this.dv1;
            }
        }

        [Category("Deklarit Work With ")]
        public virtual int FillByIDTIPURAIDTIPURA
        {
            get
            {
                return this.m_FillByIDTIPURAIDTIPURA;
            }
            set
            {
                this.m_FillByIDTIPURAIDTIPURA = value;
            }
        }

        [Category("Deklarit Work With "), DefaultValue(true)]
        public virtual bool FillByPage
        {
            get
            {
                return this.m_FillByPage;
            }
            set
            {
                this.m_FillByPage = value;
            }
        }

        [Category("Deklarit Work With ")]
        public virtual int FillByURADOKIDDOKUMENTURADOKIDDOKUMENT
        {
            get
            {
                return this.m_FillByURADOKIDDOKUMENTURADOKIDDOKUMENT;
            }
            set
            {
                this.m_FillByURADOKIDDOKUMENTURADOKIDDOKUMENT = value;
            }
        }

        [Category("Deklarit Work With ")]
        public virtual int FillByURAGODIDGODINEURADOKIDDOKUMENTURADOKIDDOKUMENT
        {
            get
            {
                return this.m_FillByURAGODIDGODINEURADOKIDDOKUMENTURADOKIDDOKUMENT;
            }
            set
            {
                this.m_FillByURAGODIDGODINEURADOKIDDOKUMENTURADOKIDDOKUMENT = value;
            }
        }

        [Category("Deklarit Work With ")]
        public virtual short FillByURAGODIDGODINEURADOKIDDOKUMENTURAGODIDGODINE
        {
            get
            {
                return this.m_FillByURAGODIDGODINEURADOKIDDOKUMENTURAGODIDGODINE;
            }
            set
            {
                this.m_FillByURAGODIDGODINEURADOKIDDOKUMENTURAGODIDGODINE = value;
            }
        }

        [Category("Deklarit Work With ")]
        public virtual short FillByURAGODIDGODINEURAGODIDGODINE
        {
            get
            {
                return this.m_FillByURAGODIDGODINEURAGODIDGODINE;
            }
            set
            {
                this.m_FillByURAGODIDGODINEURAGODIDGODINE = value;
            }
        }

        [Category("Deklarit Work With ")]
        public virtual int FillByurapartnerIDPARTNERUrapartnerIDPARTNER
        {
            get
            {
                return this.m_FillByurapartnerIDPARTNERUrapartnerIDPARTNER;
            }
            set
            {
                this.m_FillByurapartnerIDPARTNERUrapartnerIDPARTNER = value;
            }
        }

        [TypeConverter(typeof(DeklaritDataGrid.FillMethodsConverter)), DefaultValue("Fill"), Category("Deklarit Work With ")]
        public virtual string FillMethod
        {
            get
            {
                return this.m_FillMethod;
            }
            set
            {
                this.m_FillMethod = value;
            }
        }

        [DefaultValue(false), Browsable(false)]
        public virtual bool GridLoading
        {
            get
            {
                return this.m_GridLoading;
            }
        }

        [Category("Deklarit Work With "), DefaultValue(60)]
        public virtual int PageSize
        {
            get
            {
                return this.m_PageSize;
            }
            set
            {
                this.m_PageSize = value;
            }
        }
    }
}
