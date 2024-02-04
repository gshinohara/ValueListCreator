using Grasshopper;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.GUI.Canvas.Interaction;

namespace ValueListCreator
{
    internal class ValueListCreator_Interaction : GH_AbstractInteraction
    {
        private ValueListCreator_Attribute attribute;
        public ValueListCreator_Interaction(GH_Canvas canvas,GH_CanvasMouseEvent mouseEvent, ValueListCreator_Attribute attribute) : base(canvas, mouseEvent)
        {
            this.attribute = attribute;
        }
        public override GH_ObjectResponse RespondToMouseMove(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            if (attribute.onGenerate)
                Instances.CursorServer.AttachCursor(sender, "GH_AddObject");
            else
                Instances.CursorServer.ResetCursor(sender);
            return GH_ObjectResponse.Handled;
        }
        public override GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            attribute.onGenerate = false;
            if (attribute.Owner.RuntimeMessageLevel != Grasshopper.Kernel.GH_RuntimeMessageLevel.Blank)
                return GH_ObjectResponse.Release;

            Grasshopper.Kernel.Special.GH_ValueList valueList = (attribute.Owner as ValueListCreator_Component).valueList;
            sender.Document.AddObject(valueList, true);
            valueList.Attributes.Pivot = e.CanvasLocation;
            valueList.ExpireSolution(true);

            attribute.Owner.ExpireSolution(true);

            return GH_ObjectResponse.Release;
        }
    }
}
