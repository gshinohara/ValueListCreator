using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace ValueListCreator
{
    public class ValueListCreatorInfo : GH_AssemblyInfo
    {
        public override string Name => "ValueListCreator";
        public override Bitmap Icon => null;
        public override string Description => "";
        public override Guid Id => new Guid("ad6f06aa-a49e-42a8-948f-8eeab2a3e8c7");
        public override string AuthorName => "Gaku Shinohara";
        public override string AuthorContact => "https://github.com/gshinohara/ValueListCreator";
    }
}