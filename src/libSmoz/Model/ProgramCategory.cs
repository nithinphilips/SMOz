using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace LibSmoz
{
    public class ProgramCategory : Collection<ProgramItem>, IEquatable<ProgramCategory>, IComparable<ProgramCategory>
    {

        #region List methods

        protected override void InsertItem(int index, ProgramItem item)
        {
            item.Category = this;
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, ProgramItem item)
        {
            this[index].Category = null;
            base.SetItem(index, item);
            this[index].Category = this;
        }

        protected override void RemoveItem(int index)
        {
            this[index].Category = null;
            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            foreach (var item in this)
            {
                item.Category = null;
            }
            base.ClearItems();
        }

        #endregion

        public string Name { get; set; }
        public ProgramsMenu Root { get; internal set; }
        public ReadOnlyCollection<string> Paths { get; protected set; }

        internal void UpdatePaths()
        {
            List<string> list = new List<string>(this.Root.Paths.Count);
            foreach (var item in this.Root.Paths)
            {
                list.Add(System.IO.Path.Combine(item, this.Name));
            }
            this.Paths = list.AsReadOnly();

            foreach (var item in this)
                item.UpdatePaths();
            
        }

        public bool Equals(ProgramCategory other)
        {
            return this.Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public int CompareTo(ProgramCategory other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            string s = this.Name;
            foreach (var item in Paths)
            {
                s += Environment.NewLine + item;
            }
            return s;
        }
    }
}
