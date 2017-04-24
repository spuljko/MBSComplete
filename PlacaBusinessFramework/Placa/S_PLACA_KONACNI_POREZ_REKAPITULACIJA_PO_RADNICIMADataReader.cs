﻿namespace Placa
{
    using Deklarit.Data;
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Data;
    using System.Threading;

    public class S_PLACA_KONACNI_POREZ_REKAPITULACIJA_PO_RADNICIMADataReader : IDisposable
    {
        private ReadWriteCommand cmS_PLACA_KONACNI_POREZ_REKAPITULACIJA_PO_RADNICIMASelect3;
        private ReadWriteConnection connDefault;
        private IDbTransaction daCurrentTransaction;
        private DataStore dsDefault;
        private bool m_Disposed;
        private IDataReader S_PLACA_KONACNI_POREZ_REKAPITULACIJA_PO_RADNICIMASelect3;

        protected void Cleanup()
        {
            this.dsDefault.Dispose();
        }

        public void Close()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (!this.m_Disposed)
            {
                this.m_Disposed = true;
                try
                {
                    this.S_PLACA_KONACNI_POREZ_REKAPITULACIJA_PO_RADNICIMASelect3.Close();
                }
                finally
                {
                    try
                    {
                        this.connDefault.Close();
                    }
                    finally
                    {
                        this.Cleanup();
                    }
                }
            }
        }

        private void init_reader()
        {
            this.Initialize();
            this.dsDefault = new DataStore(new SqlServer2005Handler(), "System.Data.SqlClient", Configuration.ConnectionString, DeklaritTransaction.TransactionSlotName);
            this.m_Disposed = false;
        }

        private void Initialize()
        {
            this.m_Disposed = false;
            if (this.Transaction == null)
            {
                this.Transaction = (IDbTransaction) Thread.GetData(Thread.GetNamedDataSlot(DeklaritTransaction.TransactionSlotName));
            }
        }

        public IDataReader Open(string iDOBRACUN)
        {
            this.init_reader();
            this.connDefault = this.dsDefault.GetReadWriteConnection(this.daCurrentTransaction);
            this.cmS_PLACA_KONACNI_POREZ_REKAPITULACIJA_PO_RADNICIMASelect3 = this.connDefault.GetCommand("S_PLACA_KONACNI_POREZ_REKAPITULACIJA_PO_RADNICIMA", true);
            this.cmS_PLACA_KONACNI_POREZ_REKAPITULACIJA_PO_RADNICIMASelect3.IDbCommand.CommandType = CommandType.StoredProcedure;
            this.cmS_PLACA_KONACNI_POREZ_REKAPITULACIJA_PO_RADNICIMASelect3.IDbCommand.Parameters.Clear();
            this.cmS_PLACA_KONACNI_POREZ_REKAPITULACIJA_PO_RADNICIMASelect3.IDbCommand.Parameters.Add(this.dsDefault.GetDbParameter("@IDOBRACUN", iDOBRACUN));
            this.cmS_PLACA_KONACNI_POREZ_REKAPITULACIJA_PO_RADNICIMASelect3.ErrorMask |= ErrorMask.Lock;
            this.S_PLACA_KONACNI_POREZ_REKAPITULACIJA_PO_RADNICIMASelect3 = this.cmS_PLACA_KONACNI_POREZ_REKAPITULACIJA_PO_RADNICIMASelect3.ExecuteReader((CommandBehavior) Conversions.ToInteger(Interaction.IIf(this.daCurrentTransaction == null, Configuration.ReaderCommandBehavior, CommandBehavior.Default)));
            return this.S_PLACA_KONACNI_POREZ_REKAPITULACIJA_PO_RADNICIMASelect3;
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
