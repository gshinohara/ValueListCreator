using Grasshopper.Kernel;
using Grasshopper.Kernel.Special;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ValueListCreator
{
    public class ValueListCreator_Component : GH_Component
    {
        internal GH_ValueList valueList;
        public ValueListCreator_Component()
          : base("ValueList Creator", "VL Creator",
              "",
              "Params", "Input")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Key", "K", "", GH_ParamAccess.list);
            pManager.AddTextParameter("Value", "V", "", GH_ParamAccess.list);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            if (Params.Input.Any(p => p.VolatileData.PathCount != 1))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Only one branch can be accepted.");
                valueList = null;
                return;
            }

            List<string> keys = new List<string>();
            List<string> values = new List<string>();
            DA.GetDataList("Key", keys);
            DA.GetDataList("Value", values);

            var kvs = keys.Zip(values, (key, value) => new { key = key, value = value });

            valueList = new GH_ValueList();
            valueList.ListItems.Clear();

            foreach (var item in kvs)
                valueList.ListItems.Add(new GH_ValueListItem(item.key, $"\"{item.value}\""));
        }
        protected override System.Drawing.Bitmap Icon => null;
        public override Guid ComponentGuid => new Guid("DD163425-940C-4E43-AFAC-6E74976D3C61");
        public override void CreateAttributes()
        {
            m_attributes = new ValueListCreator_Attribute(this);
        }
    }
}