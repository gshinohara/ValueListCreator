using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using System.Drawing;
using System.Windows.Forms;

namespace ValueListCreator
{
    internal class ValueListCreator_Attribute : GH_ComponentAttributes
    {
        public ValueListCreator_Attribute(ValueListCreator_Component owner) : base(owner) { }
        private RectangleF ButtonBounds { get; set; }
        protected override void Layout()
        {
            base.Layout();

            Rectangle baseRect = GH_Convert.ToRectangle(Bounds);
            baseRect.Height += 22;

            Rectangle buttonRect = baseRect;
            buttonRect.Y = buttonRect.Bottom - 22;
            buttonRect.Height = 22;
            buttonRect.Inflate(-2,-2);

            Bounds = baseRect;
            ButtonBounds = buttonRect;
        }
        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            base.Render(canvas, graphics, channel);

            if (channel == GH_CanvasChannel.Objects)
            {
                GH_Capsule button = GH_Capsule.CreateTextCapsule(ButtonBounds, ButtonBounds, GH_Palette.Black, "Locate", 2, 0);
                button.Render(graphics, Selected, Owner.Locked, false);
                button.Dispose();
            }
        }
        public override GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            if (e.Button == MouseButtons.Left && ButtonBounds.Contains(e.CanvasLocation))
            {
                onGenerate = true;
                sender.ActiveInteraction = new ValueListCreator_Interaction(sender, e, this);
                return GH_ObjectResponse.Handled;
            }
            return base.RespondToMouseDown(sender, e);
        }
        internal bool onGenerate = false;
    }
}
