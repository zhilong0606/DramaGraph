using System;
using System.Collections.Generic;

namespace Tool.Export.Structure
{
    public abstract class CollectionStrucureInfo : BaseStructureInfo
    {
        public sealed override bool isCollection { get { return true; } }

        protected CollectionStrucureInfo(string name) : base(name)
        {
        }
    }
}
