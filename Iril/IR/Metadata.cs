using System;
using System.Collections.Generic;

namespace Repil.IR
{
    public class Metadata
    {
        public readonly static MetadataValue Null = new MetadataNull ();

        public readonly SymbolTable<MetadataValue> Table = new SymbolTable<MetadataValue> ();

        public MetadataValue this[Symbol key] {
            get {
                if (Table.TryGetValue (key, out var m)) {
                    return m;
                }
                return Null;
            }
        }
    }

    public abstract class MetadataValue
    {
        public virtual bool IsNull => false;

        public MetadataValue this[Symbol key] => GetValue (key);

        protected abstract MetadataValue GetValue (Symbol key);

        public Metadata Metadata;
    }

    public class MetadataReference : MetadataValue
    {
        readonly MetaSymbol symbol;

        public MetadataValue Value {
            get {
                if (Metadata != null && Metadata.Table.TryGetValue (symbol, out var m))
                    return m;
                return Metadata.Null;
            }
        }

        public MetadataReference (MetaSymbol symbol)
        {
            this.symbol = symbol;
        }

        protected override MetadataValue GetValue (Symbol key) => Metadata.Null;
    }

    public class MetadataList : MetadataValue
    {
        public MetadataList (IEnumerable<MetadataValue> list)
        {
        }

        protected override MetadataValue GetValue (Symbol key) => Metadata.Null;
    }

    public class MetadataObject : MetadataValue
    {
        public MetadataObject ()
        {
        }

        public MetadataObject (SymbolTable<MetadataValue> table)
        {
        }

        protected override MetadataValue GetValue (Symbol key) => Metadata.Null;
    }

    public class MetadataNull : MetadataValue
    {
        public override bool IsNull => true;
        protected override MetadataValue GetValue (Symbol key) => this;
    }
}
