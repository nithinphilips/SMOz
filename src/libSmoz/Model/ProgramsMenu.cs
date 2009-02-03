using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace LibSmoz
{
    public class ProgramsMenu : Collection<ProgramCategory>
    {
        #region List methods

        protected override void InsertItem(int index, ProgramCategory item)
        {
            item.Root = this;
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, ProgramCategory item)
        {
            this[index].Root = null;
            base.SetItem(index, item);
            this[index].Root = this;
        }

        protected override void RemoveItem(int index)
        {
            this[index].Root = null;
            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            foreach (var item in this)
            {
                item.Root = null;
            }
            base.ClearItems();
        }

        #endregion

        ReadOnlyCollection<string> paths;
        /// <summary>
        /// Gets or sets the paths on the file system refered to by this ProgramsMenu.
        /// </summary>
        public ReadOnlyCollection<string> Paths 
        {
            get
            {
                return paths;
            }
            set
            {
                this.paths = value;
                this.UpdatePaths();
            }
        }

        /// <summary>
        /// Forces update of the Paths field of all Categories in this ProgramsMenu.
        /// </summary>
        public void UpdatePaths()
        {
            foreach (var item in this)
                item.UpdatePaths();
        }

        public override string ToString()
        {
            string s = "";
            foreach (var item in Paths)
            {
                s += item + Environment.NewLine;
            }
            return s;
        }
    }
}
