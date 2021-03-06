﻿namespace Placa
{
    using Deklarit.Data;
    using Deklarit.Utils;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Reflection;
    using System.Resources;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class S_DD_REKAP_DOPRINOSDataAdapter : IDataAdapter, IS_DD_REKAP_DOPRINOSDataAdapter
    {
        private string AV8IDOBRAC;
        private ReadWriteCommand cmS_DD_REKAP_DOPRINOSSelect1;
        private ReadWriteCommand cmS_DD_REKAP_DOPRINOSSelect2;
        private ReadWriteConnection connDefault;
        private IDbTransaction daCurrentTransaction;
        private DataStore dsDefault = new DataStore(new SqlServer2005Handler(), "System.Data.SqlClient", Configuration.ConnectionString, DeklaritTransaction.TransactionSlotName);
        private DbParameter[] fillDataParameters;
        private int m_RecordCount;
        private System.Resources.ResourceManager resourceManager;
        private System.Resources.ResourceManager resourceManagerTables;
        private S_DD_REKAP_DOPRINOSDataSet.S_DD_REKAP_DOPRINOSRow rowS_DD_REKAP_DOPRINOS;
        private IDataReader S_DD_REKAP_DOPRINOSSelect1;
        private IDataReader S_DD_REKAP_DOPRINOSSelect2;
        private S_DD_REKAP_DOPRINOSDataSet S_DD_REKAP_DOPRINOSSet;

        private void AddRowS_dd_rekap_doprinos()
        {
            this.S_DD_REKAP_DOPRINOSSet.S_DD_REKAP_DOPRINOS.AddS_DD_REKAP_DOPRINOSRow(this.rowS_DD_REKAP_DOPRINOS);
            this.rowS_DD_REKAP_DOPRINOS.AcceptChanges();
        }

        protected void Cleanup()
        {
            this.dsDefault.Dispose();
        }

        public void executePrivate(int startRow, int maxRows)
        {
            this.cmS_DD_REKAP_DOPRINOSSelect2 = this.connDefault.GetCommand("S_DD_REKAP_DOPRINOS", true);
            this.cmS_DD_REKAP_DOPRINOSSelect2.IDbCommand.CommandType = CommandType.StoredProcedure;
            this.cmS_DD_REKAP_DOPRINOSSelect2.IDbCommand.Parameters.Clear();
            this.cmS_DD_REKAP_DOPRINOSSelect2.IDbCommand.Parameters.Add(this.dsDefault.GetDbParameter("@IDOBRACUN", this.AV8IDOBRAC));
            this.cmS_DD_REKAP_DOPRINOSSelect2.ErrorMask |= ErrorMask.Lock;
            this.S_DD_REKAP_DOPRINOSSelect2 = this.cmS_DD_REKAP_DOPRINOSSelect2.FetchData();
            while (this.cmS_DD_REKAP_DOPRINOSSelect2.HasMoreRows && (startRow > 0))
            {
                startRow--;
                this.cmS_DD_REKAP_DOPRINOSSelect2.HasMoreRows = this.S_DD_REKAP_DOPRINOSSelect2.Read();
            }
            int num = 0;
            while (this.cmS_DD_REKAP_DOPRINOSSelect2.HasMoreRows && (num != maxRows))
            {
                this.rowS_DD_REKAP_DOPRINOS["IZNOS"] = RuntimeHelpers.GetObjectValue(this.S_DD_REKAP_DOPRINOSSelect2["IZNOS"]);
                this.rowS_DD_REKAP_DOPRINOS["SIFRA"] = RuntimeHelpers.GetObjectValue(this.S_DD_REKAP_DOPRINOSSelect2["SIFRA"]);
                this.rowS_DD_REKAP_DOPRINOS["vrsta"] = RuntimeHelpers.GetObjectValue(this.S_DD_REKAP_DOPRINOSSelect2["VRSTA"]);
                this.rowS_DD_REKAP_DOPRINOS["NAZIVDOPRINOS"] = RuntimeHelpers.GetObjectValue(this.S_DD_REKAP_DOPRINOSSelect2["NAZIVDOPRINOS"]);
                this.rowS_DD_REKAP_DOPRINOS["vrstasifra"] = RuntimeHelpers.GetObjectValue(this.S_DD_REKAP_DOPRINOSSelect2["VRSTASIFRA"]);
                this.AddRowS_dd_rekap_doprinos();
                num++;
                this.rowS_DD_REKAP_DOPRINOS = this.S_DD_REKAP_DOPRINOSSet.S_DD_REKAP_DOPRINOS.NewS_DD_REKAP_DOPRINOSRow();
                this.cmS_DD_REKAP_DOPRINOSSelect2.HasMoreRows = this.S_DD_REKAP_DOPRINOSSelect2.Read();
            }
            this.S_DD_REKAP_DOPRINOSSelect2.Close();
            this.Cleanup();
        }

        public virtual int Fill(S_DD_REKAP_DOPRINOSDataSet dataSet)
        {
            if (this.fillDataParameters == null)
            {
                throw new ArgumentException();
            }
            this.Fill(dataSet, this.fillDataParameters[0].Value.ToString());
            return 0;
        }

        public virtual int Fill(DataSet dataSet)
        {
            this.S_DD_REKAP_DOPRINOSSet = (S_DD_REKAP_DOPRINOSDataSet) dataSet;
            if (this.S_DD_REKAP_DOPRINOSSet != null)
            {
                return this.Fill(this.S_DD_REKAP_DOPRINOSSet);
            }
            this.S_DD_REKAP_DOPRINOSSet = new S_DD_REKAP_DOPRINOSDataSet();
            this.Fill(this.S_DD_REKAP_DOPRINOSSet);
            dataSet.Merge(this.S_DD_REKAP_DOPRINOSSet);
            return 0;
        }

        public virtual int Fill(S_DD_REKAP_DOPRINOSDataSet dataSet, string iDOBRACUN)
        {
            this.Initialize();
            this.connDefault = this.dsDefault.GetReadWriteConnection(this.daCurrentTransaction);
            this.S_DD_REKAP_DOPRINOSSet = dataSet;
            this.rowS_DD_REKAP_DOPRINOS = this.S_DD_REKAP_DOPRINOSSet.S_DD_REKAP_DOPRINOS.NewS_DD_REKAP_DOPRINOSRow();
            this.SetFillParameters(iDOBRACUN);
            this.AV8IDOBRAC = iDOBRACUN;
            try
            {
                this.executePrivate(0, -1);
            }
            finally
            {
                this.Cleanup();
            }
            return 0;
        }

        public virtual int FillPage(S_DD_REKAP_DOPRINOSDataSet dataSet, int startRow, int maxRows)
        {
            if (this.fillDataParameters == null)
            {
                throw new ArgumentException();
            }
            this.FillPage(dataSet, this.fillDataParameters[0].Value.ToString(), startRow, maxRows);
            return 0;
        }

        public virtual int FillPage(DataSet dataSet, int startRow, int maxRows)
        {
            this.S_DD_REKAP_DOPRINOSSet = (S_DD_REKAP_DOPRINOSDataSet) dataSet;
            if (this.S_DD_REKAP_DOPRINOSSet != null)
            {
                return this.FillPage(this.S_DD_REKAP_DOPRINOSSet, startRow, maxRows);
            }
            this.S_DD_REKAP_DOPRINOSSet = new S_DD_REKAP_DOPRINOSDataSet();
            this.FillPage(this.S_DD_REKAP_DOPRINOSSet, startRow, maxRows);
            dataSet.Merge(this.S_DD_REKAP_DOPRINOSSet);
            return 0;
        }

        public virtual int FillPage(S_DD_REKAP_DOPRINOSDataSet dataSet, string iDOBRACUN, int startRow, int maxRows)
        {
            this.Initialize();
            this.connDefault = this.dsDefault.GetReadWriteConnection(this.daCurrentTransaction);
            this.S_DD_REKAP_DOPRINOSSet = dataSet;
            this.rowS_DD_REKAP_DOPRINOS = this.S_DD_REKAP_DOPRINOSSet.S_DD_REKAP_DOPRINOS.NewS_DD_REKAP_DOPRINOSRow();
            this.SetFillParameters(iDOBRACUN);
            this.AV8IDOBRAC = iDOBRACUN;
            try
            {
                this.executePrivate(startRow, maxRows);
            }
            finally
            {
                this.Cleanup();
            }
            return 0;
        }

        public virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
        {
            DataTable[] array = new DataTable[dataSet.Tables.Count + 1];
            dataSet.Tables.CopyTo(array, dataSet.Tables.Count);
            return array;
        }

        public virtual IDataParameter[] GetFillParameters()
        {
            if (this.fillDataParameters == null)
            {
                DbParameter parameter = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateParameter();
                parameter.ParameterName = "IDOBRACUN";
                parameter.DbType = DbType.String;
                this.fillDataParameters = new DbParameter[] { parameter };
            }
            return this.fillDataParameters;
        }

        private int GetInternalRecordCount(string iDOBRACUN)
        {
            this.connDefault = this.dsDefault.GetReadWriteConnection(this.daCurrentTransaction);
            this.cmS_DD_REKAP_DOPRINOSSelect1 = this.connDefault.GetCommand("S_DD_REKAP_DOPRINOS", true);
            this.cmS_DD_REKAP_DOPRINOSSelect1.IDbCommand.CommandType = CommandType.StoredProcedure;
            this.cmS_DD_REKAP_DOPRINOSSelect1.IDbCommand.Parameters.Clear();
            this.cmS_DD_REKAP_DOPRINOSSelect1.IDbCommand.Parameters.Add(this.dsDefault.GetDbParameter("@IDOBRACUN", iDOBRACUN));
            this.cmS_DD_REKAP_DOPRINOSSelect1.ErrorMask |= ErrorMask.Lock;
            this.S_DD_REKAP_DOPRINOSSelect1 = this.cmS_DD_REKAP_DOPRINOSSelect1.FetchData();
            if (this.S_DD_REKAP_DOPRINOSSelect1.IsDBNull(0))
            {
                this.m_RecordCount = 0;
            }
            else
            {
                this.m_RecordCount = this.S_DD_REKAP_DOPRINOSSelect1.GetInt32(0);
            }
            this.S_DD_REKAP_DOPRINOSSelect1.Close();
            return this.m_RecordCount;
        }

        public virtual int GetRecordCount(string iDOBRACUN)
        {
            int internalRecordCount;
            try
            {
                this.SetFillParameters(iDOBRACUN);
                internalRecordCount = this.GetInternalRecordCount(iDOBRACUN);
            }
            finally
            {
                this.Cleanup();
            }
            return internalRecordCount;
        }

        private void Initialize()
        {
            this.resourceManager = Deklarit.Utils.ResourceManager.Instance;
            this.resourceManagerTables = new System.Resources.ResourceManager("Tables", Assembly.GetExecutingAssembly());
            this.AV8IDOBRAC = "";
            this.m_RecordCount = 0;
            if (this.Transaction == null)
            {
                this.Transaction = (IDbTransaction) Thread.GetData(Thread.GetNamedDataSlot(DeklaritTransaction.TransactionSlotName));
            }
        }

        private void SetFillParameters(string iDOBRACUN)
        {
            if (this.fillDataParameters == null)
            {
                this.GetFillParameters();
                this.fillDataParameters[0].Value = iDOBRACUN;
            }
        }

        public virtual int Update(DataSet dataSet)
        {
            throw new InvalidOperationException();
        }

        public System.Data.MissingMappingAction MissingMappingAction
        {
            get
            {
                return System.Data.MissingMappingAction.Passthrough;
            }
            set
            {
            }
        }

        System.Data.MissingSchemaAction MissingSchemaAction
        {
            get
            {
                return System.Data.MissingSchemaAction.Ignore;
            }
            set
            {
            }
        }

        System.Data.MissingMappingAction System.Data.IDataAdapter.MissingMappingAction
        {
            get
            {
                return System.Data.MissingMappingAction.Passthrough;
            }
            set
            {
            }
        }

        System.Data.MissingSchemaAction System.Data.IDataAdapter.MissingSchemaAction
        {
            get
            {
                return System.Data.MissingSchemaAction.Ignore;
            }
            set
            {
            }
        }

        ITableMappingCollection System.Data.IDataAdapter.TableMappings
        {
            get
            {
                return new DataTableMappingCollection();
            }
        }

        ITableMappingCollection TableMappings
        {
            get
            {
                return new DataTableMappingCollection();
            }
        }

        public IDbTransaction Transaction
        {
            get
            {
                return this.daCurrentTransaction;
            }
            set
            {
                this.daCurrentTransaction = value;
            }
        }
    }
}

