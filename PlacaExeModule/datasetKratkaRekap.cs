﻿using Microsoft.VisualBasic.CompilerServices;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

[Serializable, XmlSchemaProvider("GetTypedDataSetSchema"), GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), ToolboxItem(true), DesignerCategory("code"), HelpKeyword("vs.data.DataSet"), XmlRoot("datasetKratkaRekap")]
public class datasetKratkaRekap : DataSet
{
    private System.Data.SchemaSerializationMode _schemaSerializationMode;
    private dsRekapitulacijaDataTable tabledsRekapitulacija;
    private totalsatiDataTable tabletotalsati;

    [DebuggerNonUserCode]
    public datasetKratkaRekap()
    {
        this._schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        this.BeginInit();
        this.InitClass();
        CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
        base.Tables.CollectionChanged += handler;
        base.Relations.CollectionChanged += handler;
        this.EndInit();
    }

    [DebuggerNonUserCode]
    protected datasetKratkaRekap(SerializationInfo info, StreamingContext context) : base(info, context, false)
    {
        this._schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        if (this.IsBinarySerialized(info, context))
        {
            this.InitVars(false);
            CollectionChangeEventHandler handler2 = new CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += handler2;
            this.Relations.CollectionChanged += handler2;
        }
        else
        {
            string s = Conversions.ToString(info.GetValue("XmlSchema", typeof(string)));
            if (this.DetermineSchemaSerializationMode(info, context) == System.Data.SchemaSerializationMode.IncludeSchema)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["totalsati"] != null)
                {
                    base.Tables.Add(new totalsatiDataTable(dataSet.Tables["totalsati"]));
                }
                if (dataSet.Tables["dsRekapitulacija"] != null)
                {
                    base.Tables.Add(new dsRekapitulacijaDataTable(dataSet.Tables["dsRekapitulacija"]));
                }
                this.DataSetName = dataSet.DataSetName;
                this.Prefix = dataSet.Prefix;
                this.Namespace = dataSet.Namespace;
                this.Locale = dataSet.Locale;
                this.CaseSensitive = dataSet.CaseSensitive;
                this.EnforceConstraints = dataSet.EnforceConstraints;
                this.Merge(dataSet, false, MissingSchemaAction.Add);
                this.InitVars();
            }
            else
            {
                this.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
            }
            this.GetSerializationData(info, context);
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            this.Relations.CollectionChanged += handler;
        }
    }

    [DebuggerNonUserCode]
    public override DataSet Clone()
    {
        datasetKratkaRekap rekap = (datasetKratkaRekap) base.Clone();
        rekap.InitVars();
        rekap.SchemaSerializationMode = this.SchemaSerializationMode;
        return rekap;
    }

    [DebuggerNonUserCode]
    protected override XmlSchema GetSchemaSerializable()
    {
        MemoryStream w = new MemoryStream();
        this.WriteXmlSchema(new XmlTextWriter(w, null));
        w.Position = 0L;
        return XmlSchema.Read(new XmlTextReader(w), null);
    }

    [DebuggerNonUserCode]
    public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
    {
        datasetKratkaRekap rekap = new datasetKratkaRekap();
        XmlSchemaComplexType type2 = new XmlSchemaComplexType();
        XmlSchemaSequence sequence = new XmlSchemaSequence();
        XmlSchemaAny item = new XmlSchemaAny {
            Namespace = rekap.Namespace
        };
        sequence.Items.Add(item);
        type2.Particle = sequence;
        XmlSchema schemaSerializable = rekap.GetSchemaSerializable();
        if (xs.Contains(schemaSerializable.TargetNamespace))
        {
            MemoryStream stream = new MemoryStream();
            MemoryStream stream2 = new MemoryStream();
            try
            {
                XmlSchema current = null;
                schemaSerializable.Write(stream);
                IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    current = (XmlSchema) enumerator.Current;
                    stream2.SetLength(0L);
                    current.Write(stream2);
                    if (stream.Length == stream2.Length)
                    {
                        stream.Position = 0L;
                        stream2.Position = 0L;
                        while ((stream.Position != stream.Length) && (stream.ReadByte() == stream2.ReadByte()))
                        {
                        }
                        if (stream.Position == stream.Length)
                        {
                            return type2;
                        }
                    }
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                if (stream2 != null)
                {
                    stream2.Close();
                }
            }
        }
        xs.Add(schemaSerializable);
        return type2;
    }

    [DebuggerNonUserCode]
    private void InitClass()
    {
        this.DataSetName = "datasetKratkaRekap";
        this.Prefix = "";
        this.Namespace = "http://tempuri.org/datasetKratkaRekap.xsd";
        this.EnforceConstraints = true;
        this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        this.tabletotalsati = new totalsatiDataTable();
        base.Tables.Add(this.tabletotalsati);
        this.tabledsRekapitulacija = new dsRekapitulacijaDataTable();
        base.Tables.Add(this.tabledsRekapitulacija);
    }

    [DebuggerNonUserCode]
    protected override void InitializeDerivedDataSet()
    {
        this.BeginInit();
        this.InitClass();
        this.EndInit();
    }

    [DebuggerNonUserCode]
    internal void InitVars()
    {
        this.InitVars(true);
    }

    [DebuggerNonUserCode]
    internal void InitVars(bool initTable)
    {
        this.tabletotalsati = (totalsatiDataTable) base.Tables["totalsati"];
        if (initTable && (this.tabletotalsati != null))
        {
            this.tabletotalsati.InitVars();
        }
        this.tabledsRekapitulacija = (dsRekapitulacijaDataTable) base.Tables["dsRekapitulacija"];
        if (initTable && (this.tabledsRekapitulacija != null))
        {
            this.tabledsRekapitulacija.InitVars();
        }
    }

    [DebuggerNonUserCode]
    protected override void ReadXmlSerializable(XmlReader reader)
    {
        if (this.DetermineSchemaSerializationMode(reader) == System.Data.SchemaSerializationMode.IncludeSchema)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["totalsati"] != null)
            {
                base.Tables.Add(new totalsatiDataTable(dataSet.Tables["totalsati"]));
            }
            if (dataSet.Tables["dsRekapitulacija"] != null)
            {
                base.Tables.Add(new dsRekapitulacijaDataTable(dataSet.Tables["dsRekapitulacija"]));
            }
            this.DataSetName = dataSet.DataSetName;
            this.Prefix = dataSet.Prefix;
            this.Namespace = dataSet.Namespace;
            this.Locale = dataSet.Locale;
            this.CaseSensitive = dataSet.CaseSensitive;
            this.EnforceConstraints = dataSet.EnforceConstraints;
            this.Merge(dataSet, false, MissingSchemaAction.Add);
            this.InitVars();
        }
        else
        {
            this.ReadXml(reader);
            this.InitVars();
        }
    }

    [DebuggerNonUserCode]
    private void SchemaChanged(object sender, CollectionChangeEventArgs e)
    {
        if (e.Action == CollectionChangeAction.Remove)
        {
            this.InitVars();
        }
    }

    [DebuggerNonUserCode]
    private bool ShouldSerializedsRekapitulacija()
    {
        return false;
    }

    [DebuggerNonUserCode]
    protected override bool ShouldSerializeRelations()
    {
        return false;
    }

    [DebuggerNonUserCode]
    protected override bool ShouldSerializeTables()
    {
        return false;
    }

    [DebuggerNonUserCode]
    private bool ShouldSerializetotalsati()
    {
        return false;
    }

    [Browsable(false), DebuggerNonUserCode, DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public dsRekapitulacijaDataTable dsRekapitulacija
    {
        get
        {
            return this.tabledsRekapitulacija;
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DebuggerNonUserCode]
    public new DataRelationCollection Relations
    {
        get
        {
            return base.Relations;
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), DebuggerNonUserCode, Browsable(true)]
    public override System.Data.SchemaSerializationMode SchemaSerializationMode
    {
        get
        {
            return this._schemaSerializationMode;
        }
        set
        {
            this._schemaSerializationMode = value;
        }
    }

    [DebuggerNonUserCode, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataTableCollection Tables
    {
        get
        {
            return base.Tables;
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false), DebuggerNonUserCode]
    public totalsatiDataTable totalsati
    {
        get
        {
            return this.tabletotalsati;
        }
    }

    [Serializable, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), XmlSchemaProvider("GetTypedTableSchema")]
    public class dsRekapitulacijaDataTable : TypedTableBase<datasetKratkaRekap.dsRekapitulacijaRow>
    {
        private DataColumn columniznos;
        private DataColumn columnopis;
        private DataColumn columnsati;

        public event datasetKratkaRekap.dsRekapitulacijaRowChangeEventHandler dsRekapitulacijaRowChanged;

        public event datasetKratkaRekap.dsRekapitulacijaRowChangeEventHandler dsRekapitulacijaRowChanging;

        public event datasetKratkaRekap.dsRekapitulacijaRowChangeEventHandler dsRekapitulacijaRowDeleted;

        public event datasetKratkaRekap.dsRekapitulacijaRowChangeEventHandler dsRekapitulacijaRowDeleting;

        [DebuggerNonUserCode]
        public dsRekapitulacijaDataTable()
        {
            this.TableName = "dsRekapitulacija";
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }

        [DebuggerNonUserCode]
        internal dsRekapitulacijaDataTable(DataTable table)
        {
            this.TableName = table.TableName;
            if (table.CaseSensitive != table.DataSet.CaseSensitive)
            {
                this.CaseSensitive = table.CaseSensitive;
            }
            if (table.Locale.ToString() != table.DataSet.Locale.ToString())
            {
                this.Locale = table.Locale;
            }
            if (table.Namespace != table.DataSet.Namespace)
            {
                this.Namespace = table.Namespace;
            }
            this.Prefix = table.Prefix;
            this.MinimumCapacity = table.MinimumCapacity;
        }

        [DebuggerNonUserCode]
        protected dsRekapitulacijaDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.InitVars();
        }

        [DebuggerNonUserCode]
        public void AdddsRekapitulacijaRow(datasetKratkaRekap.dsRekapitulacijaRow row)
        {
            this.Rows.Add(row);
        }

        [DebuggerNonUserCode]
        public datasetKratkaRekap.dsRekapitulacijaRow AdddsRekapitulacijaRow(string opis, decimal iznos, decimal sati)
        {
            datasetKratkaRekap.dsRekapitulacijaRow row = (datasetKratkaRekap.dsRekapitulacijaRow) this.NewRow();
            row.ItemArray = new object[] { opis, iznos, sati };
            this.Rows.Add(row);
            return row;
        }

        [DebuggerNonUserCode]
        public override DataTable Clone()
        {
            datasetKratkaRekap.dsRekapitulacijaDataTable table = (datasetKratkaRekap.dsRekapitulacijaDataTable) base.Clone();
            table.InitVars();
            return table;
        }

        [DebuggerNonUserCode]
        protected override DataTable CreateInstance()
        {
            return new datasetKratkaRekap.dsRekapitulacijaDataTable();
        }

        [DebuggerNonUserCode]
        protected override Type GetRowType()
        {
            return typeof(datasetKratkaRekap.dsRekapitulacijaRow);
        }

        [DebuggerNonUserCode]
        public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
        {
            XmlSchemaComplexType type2 = new XmlSchemaComplexType();
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            datasetKratkaRekap rekap = new datasetKratkaRekap();
            XmlSchemaAny item = new XmlSchemaAny {
                Namespace = "http://www.w3.org/2001/XMLSchema"
            };
            decimal num = new decimal(0);
            item.MinOccurs = num;
            item.MaxOccurs = 79228162514264337593543950335M;
            item.ProcessContents = XmlSchemaContentProcessing.Lax;
            sequence.Items.Add(item);
            XmlSchemaAny any2 = new XmlSchemaAny {
                Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1"
            };
            num = new decimal(1);
            any2.MinOccurs = num;
            any2.ProcessContents = XmlSchemaContentProcessing.Lax;
            sequence.Items.Add(any2);
            XmlSchemaAttribute attribute = new XmlSchemaAttribute {
                Name = "namespace",
                FixedValue = rekap.Namespace
            };
            type2.Attributes.Add(attribute);
            XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                Name = "tableTypeName",
                FixedValue = "dsRekapitulacijaDataTable"
            };
            type2.Attributes.Add(attribute2);
            type2.Particle = sequence;
            XmlSchema schemaSerializable = rekap.GetSchemaSerializable();
            if (xs.Contains(schemaSerializable.TargetNamespace))
            {
                MemoryStream stream = new MemoryStream();
                MemoryStream stream2 = new MemoryStream();
                try
                {
                    XmlSchema current = null;
                    schemaSerializable.Write(stream);
                    IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        current = (XmlSchema) enumerator.Current;
                        stream2.SetLength(0L);
                        current.Write(stream2);
                        if (stream.Length == stream2.Length)
                        {
                            stream.Position = 0L;
                            stream2.Position = 0L;
                            while ((stream.Position != stream.Length) && (stream.ReadByte() == stream2.ReadByte()))
                            {
                            }
                            if (stream.Position == stream.Length)
                            {
                                return type2;
                            }
                        }
                    }
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                    if (stream2 != null)
                    {
                        stream2.Close();
                    }
                }
            }
            xs.Add(schemaSerializable);
            return type2;
        }

        [DebuggerNonUserCode]
        private void InitClass()
        {
            this.columnopis = new DataColumn("opis", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnopis);
            this.columniznos = new DataColumn("iznos", typeof(decimal), null, MappingType.Element);
            base.Columns.Add(this.columniznos);
            this.columnsati = new DataColumn("sati", typeof(decimal), null, MappingType.Element);
            base.Columns.Add(this.columnsati);
            this.columnopis.AllowDBNull = false;
        }

        [DebuggerNonUserCode]
        internal void InitVars()
        {
            this.columnopis = base.Columns["opis"];
            this.columniznos = base.Columns["iznos"];
            this.columnsati = base.Columns["sati"];
        }

        [DebuggerNonUserCode]
        public datasetKratkaRekap.dsRekapitulacijaRow NewdsRekapitulacijaRow()
        {
            return (datasetKratkaRekap.dsRekapitulacijaRow) this.NewRow();
        }

        [DebuggerNonUserCode]
        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new datasetKratkaRekap.dsRekapitulacijaRow(builder);
        }

        [DebuggerNonUserCode]
        protected override void OnRowChanged(DataRowChangeEventArgs e)
        {
            base.OnRowChanged(e);
            if (this.dsRekapitulacijaRowChanged != null)
            {
                datasetKratkaRekap.dsRekapitulacijaRowChangeEventHandler dsRekapitulacijaRowChangedEvent = this.dsRekapitulacijaRowChanged;
                if (dsRekapitulacijaRowChangedEvent != null)
                {
                    dsRekapitulacijaRowChangedEvent(this, new datasetKratkaRekap.dsRekapitulacijaRowChangeEvent((datasetKratkaRekap.dsRekapitulacijaRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode]
        protected override void OnRowChanging(DataRowChangeEventArgs e)
        {
            base.OnRowChanging(e);
            if (this.dsRekapitulacijaRowChanging != null)
            {
                datasetKratkaRekap.dsRekapitulacijaRowChangeEventHandler dsRekapitulacijaRowChangingEvent = this.dsRekapitulacijaRowChanging;
                if (dsRekapitulacijaRowChangingEvent != null)
                {
                    dsRekapitulacijaRowChangingEvent(this, new datasetKratkaRekap.dsRekapitulacijaRowChangeEvent((datasetKratkaRekap.dsRekapitulacijaRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode]
        protected override void OnRowDeleted(DataRowChangeEventArgs e)
        {
            base.OnRowDeleted(e);
            if (this.dsRekapitulacijaRowDeleted != null)
            {
                datasetKratkaRekap.dsRekapitulacijaRowChangeEventHandler dsRekapitulacijaRowDeletedEvent = this.dsRekapitulacijaRowDeleted;
                if (dsRekapitulacijaRowDeletedEvent != null)
                {
                    dsRekapitulacijaRowDeletedEvent(this, new datasetKratkaRekap.dsRekapitulacijaRowChangeEvent((datasetKratkaRekap.dsRekapitulacijaRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode]
        protected override void OnRowDeleting(DataRowChangeEventArgs e)
        {
            base.OnRowDeleting(e);
            if (this.dsRekapitulacijaRowDeleting != null)
            {
                datasetKratkaRekap.dsRekapitulacijaRowChangeEventHandler dsRekapitulacijaRowDeletingEvent = this.dsRekapitulacijaRowDeleting;
                if (dsRekapitulacijaRowDeletingEvent != null)
                {
                    dsRekapitulacijaRowDeletingEvent(this, new datasetKratkaRekap.dsRekapitulacijaRowChangeEvent((datasetKratkaRekap.dsRekapitulacijaRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode]
        public void RemovedsRekapitulacijaRow(datasetKratkaRekap.dsRekapitulacijaRow row)
        {
            this.Rows.Remove(row);
        }

        [Browsable(false), DebuggerNonUserCode]
        public int Count
        {
            get
            {
                return this.Rows.Count;
            }
        }

        [DebuggerNonUserCode]
        public datasetKratkaRekap.dsRekapitulacijaRow this[int index]
        {
            get
            {
                return (datasetKratkaRekap.dsRekapitulacijaRow) this.Rows[index];
            }
        }

        [DebuggerNonUserCode]
        public DataColumn iznosColumn
        {
            get
            {
                return this.columniznos;
            }
        }

        [DebuggerNonUserCode]
        public DataColumn opisColumn
        {
            get
            {
                return this.columnopis;
            }
        }

        [DebuggerNonUserCode]
        public DataColumn satiColumn
        {
            get
            {
                return this.columnsati;
            }
        }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class dsRekapitulacijaRow : DataRow
    {
        private datasetKratkaRekap.dsRekapitulacijaDataTable tabledsRekapitulacija;

        [DebuggerNonUserCode]
        internal dsRekapitulacijaRow(DataRowBuilder rb) : base(rb)
        {
            this.tabledsRekapitulacija = (datasetKratkaRekap.dsRekapitulacijaDataTable) this.Table;
        }

        [DebuggerNonUserCode]
        public bool IsiznosNull()
        {
            return this.IsNull(this.tabledsRekapitulacija.iznosColumn);
        }

        [DebuggerNonUserCode]
        public bool IssatiNull()
        {
            return this.IsNull(this.tabledsRekapitulacija.satiColumn);
        }

        [DebuggerNonUserCode]
        public void SetiznosNull()
        {
            this[this.tabledsRekapitulacija.iznosColumn] = RuntimeHelpers.GetObjectValue(Convert.DBNull);
        }

        [DebuggerNonUserCode]
        public void SetsatiNull()
        {
            this[this.tabledsRekapitulacija.satiColumn] = RuntimeHelpers.GetObjectValue(Convert.DBNull);
        }

        [DebuggerNonUserCode]
        public decimal iznos
        {
            get
            {
                decimal num = 0;
                try
                {
                    num = Conversions.ToDecimal(this[this.tabledsRekapitulacija.iznosColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    throw exception1;
                    //InvalidCastException innerException = exception1;
                    //throw new StrongTypingException("The value for column 'iznos' in table 'dsRekapitulacija' is DBNull.", innerException);
                }
                return num;
            }
            set
            {
                this[this.tabledsRekapitulacija.iznosColumn] = value;
            }
        }

        [DebuggerNonUserCode]
        public string opis
        {
            get
            {
                return Conversions.ToString(this[this.tabledsRekapitulacija.opisColumn]);
            }
            set
            {
                this[this.tabledsRekapitulacija.opisColumn] = value;
            }
        }

        [DebuggerNonUserCode]
        public decimal sati
        {
            get
            {
                decimal num = 0;
                try
                {
                    num = Conversions.ToDecimal(this[this.tabledsRekapitulacija.satiColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    throw exception1;
                    //InvalidCastException innerException = exception1;
                    //throw new StrongTypingException("The value for column 'sati' in table 'dsRekapitulacija' is DBNull.", innerException);
                }
                return num;
            }
            set
            {
                this[this.tabledsRekapitulacija.satiColumn] = value;
            }
        }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class dsRekapitulacijaRowChangeEvent : EventArgs
    {
        private DataRowAction eventAction;
        private datasetKratkaRekap.dsRekapitulacijaRow eventRow;

        [DebuggerNonUserCode]
        public dsRekapitulacijaRowChangeEvent(datasetKratkaRekap.dsRekapitulacijaRow row, DataRowAction action)
        {
            this.eventRow = row;
            this.eventAction = action;
        }

        [DebuggerNonUserCode]
        public DataRowAction Action
        {
            get
            {
                return this.eventAction;
            }
        }

        [DebuggerNonUserCode]
        public datasetKratkaRekap.dsRekapitulacijaRow Row
        {
            get
            {
                return this.eventRow;
            }
        }
    }

    public delegate void dsRekapitulacijaRowChangeEventHandler(object sender, datasetKratkaRekap.dsRekapitulacijaRowChangeEvent e);

    [Serializable, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), XmlSchemaProvider("GetTypedTableSchema")]
    public class totalsatiDataTable : TypedTableBase<datasetKratkaRekap.totalsatiRow>
    {
        private DataColumn columnGODINAISPLATE;
        private DataColumn columnGODINAOBRACUNA;
        private DataColumn columnidelement;
        private DataColumn columnidobracun;
        private DataColumn columnidvrstaelementa;
        private DataColumn columniznos;
        private DataColumn columnMJESECISPLATE;
        private DataColumn columnMJESECOBRACUNA;
        private DataColumn columnnazivelement;
        private DataColumn columnnazivvrstaelement;
        private DataColumn columnsati;

        public event datasetKratkaRekap.totalsatiRowChangeEventHandler totalsatiRowChanged;

        public event datasetKratkaRekap.totalsatiRowChangeEventHandler totalsatiRowChanging;

        public event datasetKratkaRekap.totalsatiRowChangeEventHandler totalsatiRowDeleted;

        public event datasetKratkaRekap.totalsatiRowChangeEventHandler totalsatiRowDeleting;

        [DebuggerNonUserCode]
        public totalsatiDataTable()
        {
            this.TableName = "totalsati";
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }

        [DebuggerNonUserCode]
        internal totalsatiDataTable(DataTable table)
        {
            this.TableName = table.TableName;
            if (table.CaseSensitive != table.DataSet.CaseSensitive)
            {
                this.CaseSensitive = table.CaseSensitive;
            }
            if (table.Locale.ToString() != table.DataSet.Locale.ToString())
            {
                this.Locale = table.Locale;
            }
            if (table.Namespace != table.DataSet.Namespace)
            {
                this.Namespace = table.Namespace;
            }
            this.Prefix = table.Prefix;
            this.MinimumCapacity = table.MinimumCapacity;
        }

        [DebuggerNonUserCode]
        protected totalsatiDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.InitVars();
        }

        [DebuggerNonUserCode]
        public void AddtotalsatiRow(datasetKratkaRekap.totalsatiRow row)
        {
            this.Rows.Add(row);
        }

        [DebuggerNonUserCode]
        public datasetKratkaRekap.totalsatiRow AddtotalsatiRow(decimal sati, decimal iznos, string nazivelement, string nazivvrstaelement, string idvrstaelementa, string idelement, string idobracun, string MJESECISPLATE, string GODINAISPLATE, string MJESECOBRACUNA, string GODINAOBRACUNA)
        {
            datasetKratkaRekap.totalsatiRow row = (datasetKratkaRekap.totalsatiRow) this.NewRow();
            row.ItemArray = new object[] { sati, iznos, nazivelement, nazivvrstaelement, idvrstaelementa, idelement, idobracun, MJESECISPLATE, GODINAISPLATE, MJESECOBRACUNA, GODINAOBRACUNA };
            this.Rows.Add(row);
            return row;
        }

        [DebuggerNonUserCode]
        public override DataTable Clone()
        {
            datasetKratkaRekap.totalsatiDataTable table = (datasetKratkaRekap.totalsatiDataTable) base.Clone();
            table.InitVars();
            return table;
        }

        [DebuggerNonUserCode]
        protected override DataTable CreateInstance()
        {
            return new datasetKratkaRekap.totalsatiDataTable();
        }

        [DebuggerNonUserCode]
        protected override Type GetRowType()
        {
            return typeof(datasetKratkaRekap.totalsatiRow);
        }

        [DebuggerNonUserCode]
        public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
        {
            XmlSchemaComplexType type2 = new XmlSchemaComplexType();
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            datasetKratkaRekap rekap = new datasetKratkaRekap();
            XmlSchemaAny item = new XmlSchemaAny {
                Namespace = "http://www.w3.org/2001/XMLSchema"
            };
            decimal num = new decimal(0);
            item.MinOccurs = num;
            item.MaxOccurs = 79228162514264337593543950335M;
            item.ProcessContents = XmlSchemaContentProcessing.Lax;
            sequence.Items.Add(item);
            XmlSchemaAny any2 = new XmlSchemaAny {
                Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1"
            };
            num = new decimal(1);
            any2.MinOccurs = num;
            any2.ProcessContents = XmlSchemaContentProcessing.Lax;
            sequence.Items.Add(any2);
            XmlSchemaAttribute attribute = new XmlSchemaAttribute {
                Name = "namespace",
                FixedValue = rekap.Namespace
            };
            type2.Attributes.Add(attribute);
            XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                Name = "tableTypeName",
                FixedValue = "totalsatiDataTable"
            };
            type2.Attributes.Add(attribute2);
            type2.Particle = sequence;
            XmlSchema schemaSerializable = rekap.GetSchemaSerializable();
            if (xs.Contains(schemaSerializable.TargetNamespace))
            {
                MemoryStream stream = new MemoryStream();
                MemoryStream stream2 = new MemoryStream();
                try
                {
                    XmlSchema current = null;
                    schemaSerializable.Write(stream);
                    IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        current = (XmlSchema) enumerator.Current;
                        stream2.SetLength(0L);
                        current.Write(stream2);
                        if (stream.Length == stream2.Length)
                        {
                            stream.Position = 0L;
                            stream2.Position = 0L;
                            while ((stream.Position != stream.Length) && (stream.ReadByte() == stream2.ReadByte()))
                            {
                            }
                            if (stream.Position == stream.Length)
                            {
                                return type2;
                            }
                        }
                    }
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                    if (stream2 != null)
                    {
                        stream2.Close();
                    }
                }
            }
            xs.Add(schemaSerializable);
            return type2;
        }

        [DebuggerNonUserCode]
        private void InitClass()
        {
            this.columnsati = new DataColumn("sati", typeof(decimal), null, MappingType.Element);
            base.Columns.Add(this.columnsati);
            this.columniznos = new DataColumn("iznos", typeof(decimal), null, MappingType.Element);
            base.Columns.Add(this.columniznos);
            this.columnnazivelement = new DataColumn("nazivelement", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnnazivelement);
            this.columnnazivvrstaelement = new DataColumn("nazivvrstaelement", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnnazivvrstaelement);
            this.columnidvrstaelementa = new DataColumn("idvrstaelementa", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnidvrstaelementa);
            this.columnidelement = new DataColumn("idelement", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnidelement);
            this.columnidobracun = new DataColumn("idobracun", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnidobracun);
            this.columnMJESECISPLATE = new DataColumn("MJESECISPLATE", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnMJESECISPLATE);
            this.columnGODINAISPLATE = new DataColumn("GODINAISPLATE", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnGODINAISPLATE);
            this.columnMJESECOBRACUNA = new DataColumn("MJESECOBRACUNA", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnMJESECOBRACUNA);
            this.columnGODINAOBRACUNA = new DataColumn("GODINAOBRACUNA", typeof(string), null, MappingType.Element);
            base.Columns.Add(this.columnGODINAOBRACUNA);
        }

        [DebuggerNonUserCode]
        internal void InitVars()
        {
            this.columnsati = base.Columns["sati"];
            this.columniznos = base.Columns["iznos"];
            this.columnnazivelement = base.Columns["nazivelement"];
            this.columnnazivvrstaelement = base.Columns["nazivvrstaelement"];
            this.columnidvrstaelementa = base.Columns["idvrstaelementa"];
            this.columnidelement = base.Columns["idelement"];
            this.columnidobracun = base.Columns["idobracun"];
            this.columnMJESECISPLATE = base.Columns["MJESECISPLATE"];
            this.columnGODINAISPLATE = base.Columns["GODINAISPLATE"];
            this.columnMJESECOBRACUNA = base.Columns["MJESECOBRACUNA"];
            this.columnGODINAOBRACUNA = base.Columns["GODINAOBRACUNA"];
        }

        [DebuggerNonUserCode]
        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new datasetKratkaRekap.totalsatiRow(builder);
        }

        [DebuggerNonUserCode]
        public datasetKratkaRekap.totalsatiRow NewtotalsatiRow()
        {
            return (datasetKratkaRekap.totalsatiRow) this.NewRow();
        }

        [DebuggerNonUserCode]
        protected override void OnRowChanged(DataRowChangeEventArgs e)
        {
            base.OnRowChanged(e);
            if (this.totalsatiRowChanged != null)
            {
                datasetKratkaRekap.totalsatiRowChangeEventHandler totalsatiRowChangedEvent = this.totalsatiRowChanged;
                if (totalsatiRowChangedEvent != null)
                {
                    totalsatiRowChangedEvent(this, new datasetKratkaRekap.totalsatiRowChangeEvent((datasetKratkaRekap.totalsatiRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode]
        protected override void OnRowChanging(DataRowChangeEventArgs e)
        {
            base.OnRowChanging(e);
            if (this.totalsatiRowChanging != null)
            {
                datasetKratkaRekap.totalsatiRowChangeEventHandler totalsatiRowChangingEvent = this.totalsatiRowChanging;
                if (totalsatiRowChangingEvent != null)
                {
                    totalsatiRowChangingEvent(this, new datasetKratkaRekap.totalsatiRowChangeEvent((datasetKratkaRekap.totalsatiRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode]
        protected override void OnRowDeleted(DataRowChangeEventArgs e)
        {
            base.OnRowDeleted(e);
            if (this.totalsatiRowDeleted != null)
            {
                datasetKratkaRekap.totalsatiRowChangeEventHandler totalsatiRowDeletedEvent = this.totalsatiRowDeleted;
                if (totalsatiRowDeletedEvent != null)
                {
                    totalsatiRowDeletedEvent(this, new datasetKratkaRekap.totalsatiRowChangeEvent((datasetKratkaRekap.totalsatiRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode]
        protected override void OnRowDeleting(DataRowChangeEventArgs e)
        {
            base.OnRowDeleting(e);
            if (this.totalsatiRowDeleting != null)
            {
                datasetKratkaRekap.totalsatiRowChangeEventHandler totalsatiRowDeletingEvent = this.totalsatiRowDeleting;
                if (totalsatiRowDeletingEvent != null)
                {
                    totalsatiRowDeletingEvent(this, new datasetKratkaRekap.totalsatiRowChangeEvent((datasetKratkaRekap.totalsatiRow) e.Row, e.Action));
                }
            }
        }

        [DebuggerNonUserCode]
        public void RemovetotalsatiRow(datasetKratkaRekap.totalsatiRow row)
        {
            this.Rows.Remove(row);
        }

        [DebuggerNonUserCode, Browsable(false)]
        public int Count
        {
            get
            {
                return this.Rows.Count;
            }
        }

        [DebuggerNonUserCode]
        public DataColumn GODINAISPLATEColumn
        {
            get
            {
                return this.columnGODINAISPLATE;
            }
        }

        [DebuggerNonUserCode]
        public DataColumn GODINAOBRACUNAColumn
        {
            get
            {
                return this.columnGODINAOBRACUNA;
            }
        }

        [DebuggerNonUserCode]
        public DataColumn idelementColumn
        {
            get
            {
                return this.columnidelement;
            }
        }

        [DebuggerNonUserCode]
        public DataColumn idobracunColumn
        {
            get
            {
                return this.columnidobracun;
            }
        }

        [DebuggerNonUserCode]
        public DataColumn idvrstaelementaColumn
        {
            get
            {
                return this.columnidvrstaelementa;
            }
        }

        [DebuggerNonUserCode]
        public datasetKratkaRekap.totalsatiRow this[int index]
        {
            get
            {
                return (datasetKratkaRekap.totalsatiRow) this.Rows[index];
            }
        }

        [DebuggerNonUserCode]
        public DataColumn iznosColumn
        {
            get
            {
                return this.columniznos;
            }
        }

        [DebuggerNonUserCode]
        public DataColumn MJESECISPLATEColumn
        {
            get
            {
                return this.columnMJESECISPLATE;
            }
        }

        [DebuggerNonUserCode]
        public DataColumn MJESECOBRACUNAColumn
        {
            get
            {
                return this.columnMJESECOBRACUNA;
            }
        }

        [DebuggerNonUserCode]
        public DataColumn nazivelementColumn
        {
            get
            {
                return this.columnnazivelement;
            }
        }

        [DebuggerNonUserCode]
        public DataColumn nazivvrstaelementColumn
        {
            get
            {
                return this.columnnazivvrstaelement;
            }
        }

        [DebuggerNonUserCode]
        public DataColumn satiColumn
        {
            get
            {
                return this.columnsati;
            }
        }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class totalsatiRow : DataRow
    {
        private datasetKratkaRekap.totalsatiDataTable tabletotalsati;

        [DebuggerNonUserCode]
        internal totalsatiRow(DataRowBuilder rb) : base(rb)
        {
            this.tabletotalsati = (datasetKratkaRekap.totalsatiDataTable) this.Table;
        }

        [DebuggerNonUserCode]
        public bool IsGODINAISPLATENull()
        {
            return this.IsNull(this.tabletotalsati.GODINAISPLATEColumn);
        }

        [DebuggerNonUserCode]
        public bool IsGODINAOBRACUNANull()
        {
            return this.IsNull(this.tabletotalsati.GODINAOBRACUNAColumn);
        }

        [DebuggerNonUserCode]
        public bool IsidelementNull()
        {
            return this.IsNull(this.tabletotalsati.idelementColumn);
        }

        [DebuggerNonUserCode]
        public bool IsidobracunNull()
        {
            return this.IsNull(this.tabletotalsati.idobracunColumn);
        }

        [DebuggerNonUserCode]
        public bool IsidvrstaelementaNull()
        {
            return this.IsNull(this.tabletotalsati.idvrstaelementaColumn);
        }

        [DebuggerNonUserCode]
        public bool IsiznosNull()
        {
            return this.IsNull(this.tabletotalsati.iznosColumn);
        }

        [DebuggerNonUserCode]
        public bool IsMJESECISPLATENull()
        {
            return this.IsNull(this.tabletotalsati.MJESECISPLATEColumn);
        }

        [DebuggerNonUserCode]
        public bool IsMJESECOBRACUNANull()
        {
            return this.IsNull(this.tabletotalsati.MJESECOBRACUNAColumn);
        }

        [DebuggerNonUserCode]
        public bool IsnazivelementNull()
        {
            return this.IsNull(this.tabletotalsati.nazivelementColumn);
        }

        [DebuggerNonUserCode]
        public bool IsnazivvrstaelementNull()
        {
            return this.IsNull(this.tabletotalsati.nazivvrstaelementColumn);
        }

        [DebuggerNonUserCode]
        public bool IssatiNull()
        {
            return this.IsNull(this.tabletotalsati.satiColumn);
        }

        [DebuggerNonUserCode]
        public void SetGODINAISPLATENull()
        {
            this[this.tabletotalsati.GODINAISPLATEColumn] = RuntimeHelpers.GetObjectValue(Convert.DBNull);
        }

        [DebuggerNonUserCode]
        public void SetGODINAOBRACUNANull()
        {
            this[this.tabletotalsati.GODINAOBRACUNAColumn] = RuntimeHelpers.GetObjectValue(Convert.DBNull);
        }

        [DebuggerNonUserCode]
        public void SetidelementNull()
        {
            this[this.tabletotalsati.idelementColumn] = RuntimeHelpers.GetObjectValue(Convert.DBNull);
        }

        [DebuggerNonUserCode]
        public void SetidobracunNull()
        {
            this[this.tabletotalsati.idobracunColumn] = RuntimeHelpers.GetObjectValue(Convert.DBNull);
        }

        [DebuggerNonUserCode]
        public void SetidvrstaelementaNull()
        {
            this[this.tabletotalsati.idvrstaelementaColumn] = RuntimeHelpers.GetObjectValue(Convert.DBNull);
        }

        [DebuggerNonUserCode]
        public void SetiznosNull()
        {
            this[this.tabletotalsati.iznosColumn] = RuntimeHelpers.GetObjectValue(Convert.DBNull);
        }

        [DebuggerNonUserCode]
        public void SetMJESECISPLATENull()
        {
            this[this.tabletotalsati.MJESECISPLATEColumn] = RuntimeHelpers.GetObjectValue(Convert.DBNull);
        }

        [DebuggerNonUserCode]
        public void SetMJESECOBRACUNANull()
        {
            this[this.tabletotalsati.MJESECOBRACUNAColumn] = RuntimeHelpers.GetObjectValue(Convert.DBNull);
        }

        [DebuggerNonUserCode]
        public void SetnazivelementNull()
        {
            this[this.tabletotalsati.nazivelementColumn] = RuntimeHelpers.GetObjectValue(Convert.DBNull);
        }

        [DebuggerNonUserCode]
        public void SetnazivvrstaelementNull()
        {
            this[this.tabletotalsati.nazivvrstaelementColumn] = RuntimeHelpers.GetObjectValue(Convert.DBNull);
        }

        [DebuggerNonUserCode]
        public void SetsatiNull()
        {
            this[this.tabletotalsati.satiColumn] = RuntimeHelpers.GetObjectValue(Convert.DBNull);
        }

        [DebuggerNonUserCode]
        public string GODINAISPLATE
        {
            get
            {
                string str = string.Empty;
                try
                {
                    str = Conversions.ToString(this[this.tabletotalsati.GODINAISPLATEColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    throw exception1;
                    //InvalidCastException innerException = exception1;
                    //throw new StrongTypingException("The value for column 'GODINAISPLATE' in table 'totalsati' is DBNull.", innerException);
                }
                return str;
            }
            set
            {
                this[this.tabletotalsati.GODINAISPLATEColumn] = value;
            }
        }

        [DebuggerNonUserCode]
        public string GODINAOBRACUNA
        {
            get
            {
                string str = string.Empty;
                try
                {
                    str = Conversions.ToString(this[this.tabletotalsati.GODINAOBRACUNAColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    throw exception1;
                    //InvalidCastException innerException = exception1;
                    //throw new StrongTypingException("The value for column 'GODINAOBRACUNA' in table 'totalsati' is DBNull.", innerException);
                }
                return str;
            }
            set
            {
                this[this.tabletotalsati.GODINAOBRACUNAColumn] = value;
            }
        }

        [DebuggerNonUserCode]
        public string idelement
        {
            get
            {
                string str = string.Empty;
                try
                {
                    str = Conversions.ToString(this[this.tabletotalsati.idelementColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    throw exception1;
                    //InvalidCastException innerException = exception1;
                    //throw new StrongTypingException("The value for column 'idelement' in table 'totalsati' is DBNull.", innerException);
                }
                return str;
            }
            set
            {
                this[this.tabletotalsati.idelementColumn] = value;
            }
        }

        [DebuggerNonUserCode]
        public string idobracun
        {
            get
            {
                string str = string.Empty;
                try
                {
                    str = Conversions.ToString(this[this.tabletotalsati.idobracunColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    throw exception1;
                    //InvalidCastException innerException = exception1;
                    //throw new StrongTypingException("The value for column 'idobracun' in table 'totalsati' is DBNull.", innerException);
                }
                return str;
            }
            set
            {
                this[this.tabletotalsati.idobracunColumn] = value;
            }
        }

        [DebuggerNonUserCode]
        public string idvrstaelementa
        {
            get
            {
                string str = string.Empty;
                try
                {
                    str = Conversions.ToString(this[this.tabletotalsati.idvrstaelementaColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    throw exception1;
                    //InvalidCastException innerException = exception1;
                    //throw new StrongTypingException("The value for column 'idvrstaelementa' in table 'totalsati' is DBNull.", innerException);
                }
                return str;
            }
            set
            {
                this[this.tabletotalsati.idvrstaelementaColumn] = value;
            }
        }

        [DebuggerNonUserCode]
        public decimal iznos
        {
            get
            {
                decimal num = 0;
                try
                {
                    num = Conversions.ToDecimal(this[this.tabletotalsati.iznosColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    throw exception1;
                    //InvalidCastException innerException = exception1;
                    //throw new StrongTypingException("The value for column 'iznos' in table 'totalsati' is DBNull.", innerException);
                }
                return num;
            }
            set
            {
                this[this.tabletotalsati.iznosColumn] = value;
            }
        }

        [DebuggerNonUserCode]
        public string MJESECISPLATE
        {
            get
            {
                string str = string.Empty;
                try
                {
                    str = Conversions.ToString(this[this.tabletotalsati.MJESECISPLATEColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    throw exception1;
                    //InvalidCastException innerException = exception1;
                    //throw new StrongTypingException("The value for column 'MJESECISPLATE' in table 'totalsati' is DBNull.", innerException);
                }
                return str;
            }
            set
            {
                this[this.tabletotalsati.MJESECISPLATEColumn] = value;
            }
        }

        [DebuggerNonUserCode]
        public string MJESECOBRACUNA
        {
            get
            {
                string str = string.Empty;
                try
                {
                    str = Conversions.ToString(this[this.tabletotalsati.MJESECOBRACUNAColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    throw exception1;
                    //InvalidCastException innerException = exception1;
                    //throw new StrongTypingException("The value for column 'MJESECOBRACUNA' in table 'totalsati' is DBNull.", innerException);
                }
                return str;
            }
            set
            {
                this[this.tabletotalsati.MJESECOBRACUNAColumn] = value;
            }
        }

        [DebuggerNonUserCode]
        public string nazivelement
        {
            get
            {
                string str = string.Empty;
                try
                {
                    str = Conversions.ToString(this[this.tabletotalsati.nazivelementColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    throw exception1;
                    //InvalidCastException innerException = exception1;
                    //throw new StrongTypingException("The value for column 'nazivelement' in table 'totalsati' is DBNull.", innerException);
                }
                return str;
            }
            set
            {
                this[this.tabletotalsati.nazivelementColumn] = value;
            }
        }

        [DebuggerNonUserCode]
        public string nazivvrstaelement
        {
            get
            {
                string str = string.Empty;
                try
                {
                    str = Conversions.ToString(this[this.tabletotalsati.nazivvrstaelementColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    throw exception1;
                    //InvalidCastException innerException = exception1;
                    //throw new StrongTypingException("The value for column 'nazivvrstaelement' in table 'totalsati' is DBNull.", innerException);
                }
                return str;
            }
            set
            {
                this[this.tabletotalsati.nazivvrstaelementColumn] = value;
            }
        }

        [DebuggerNonUserCode]
        public decimal sati
        {
            get
            {
                decimal num = 0;
                try
                {
                    num = Conversions.ToDecimal(this[this.tabletotalsati.satiColumn]);
                }
                catch (InvalidCastException exception1)
                {
                    throw exception1;
                    //InvalidCastException innerException = exception1;
                    //throw new StrongTypingException("The value for column 'sati' in table 'totalsati' is DBNull.", innerException);
                }
                return num;
            }
            set
            {
                this[this.tabletotalsati.satiColumn] = value;
            }
        }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class totalsatiRowChangeEvent : EventArgs
    {
        private DataRowAction eventAction;
        private datasetKratkaRekap.totalsatiRow eventRow;

        [DebuggerNonUserCode]
        public totalsatiRowChangeEvent(datasetKratkaRekap.totalsatiRow row, DataRowAction action)
        {
            this.eventRow = row;
            this.eventAction = action;
        }

        [DebuggerNonUserCode]
        public DataRowAction Action
        {
            get
            {
                return this.eventAction;
            }
        }

        [DebuggerNonUserCode]
        public datasetKratkaRekap.totalsatiRow Row
        {
            get
            {
                return this.eventRow;
            }
        }
    }

    public delegate void totalsatiRowChangeEventHandler(object sender, datasetKratkaRekap.totalsatiRowChangeEvent e);
}

