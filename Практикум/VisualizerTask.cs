using System;
using System.Drawing;
using System.Windows.Forms;

namespace Manipulation
{
    public static class VisualizerTask
    {
        public static double X = 220;
        public static double Y = -100;
        public static double Alpha = 0.05;
        public static double Wrist = 2 * Math.PI / 3;
        public static double Elbow = 3 * Math.PI / 4;
        public static double Shoulder = Math.PI / 2;

        public static Brush UnreachableAreaBrush = new SolidBrush(Color.FromArgb(255, 255, 230, 230));
        public static Brush ReachableAreaBrush = new SolidBrush(Color.FromArgb(255, 230, 255, 230));
        public static Pen ManipulatorPen = new Pen(Color.Black, 3);
        public static Brush JointBrush = Brushes.Gray;

        private static double oneStep = Math.PI / 180; 

        public static void KeyDown(Form form, KeyEventArgs key)
        {
            switch (key.KeyData)
            {
                case Keys.Q:
                    Shoulder += oneStep;
                    break;
                case Keys.A:
                    Shoulder -= oneStep;
                    break;
                case Keys.W:
                    Elbow += oneStep;
                    break;
                case Keys.S:
                    Elbow -= oneStep;
                    break;
            }

            Wrist = -Alpha - Shoulder - Elbow;
            form.Invalidate();
        }

        public static void MouseMove(Form form, MouseEventArgs e)
        {
            var point = ConvertWindowToMath(new PointF {X = e.X, Y = e.Y}, GetShoulderPos(form));
            (X, Y) = (point.X, point.Y);
            UpdateManipulator();
            form.Invalidate();
        }

        public static void MouseWheel(Form form, MouseEventArgs e)
        {
            Alpha += (double)e.Delta / 3600;
            UpdateManipulator();
            form.Invalidate();
        }
        
        public static void DrawManipulator(Graphics graphics, PointF shoulderPos)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(Shoulder, Elbow, Wrist);
            
            graphics.DrawString(
                $"X={X:0}, Y={Y:0}, Alpha={Alpha:0.00}",
                new Font(SystemFonts.DefaultFont.FontFamily, 12),
                Brushes.DarkRed,
                10,
                10);
            
            DrawReachableZone(graphics, ReachableAreaBrush, UnreachableAreaBrush, shoulderPos, joints);
            DrawManipulator(graphics, GetAllWindowPoints(joints, shoulderPos), 5);
        }
        
        private static void UpdateManipulator()
        {
            var angles = ManipulatorTask.MoveManipulatorTo(X, Y, Alpha);
            
            if (angles.Any(double.IsNaN))
                return;
                
            Shoulder = angles[0];
            Elbow = angles[1];
            Wrist = angles[2];
        }

        private static PointF GetShoulderPos(Form form)
        {
            return new PointF(form.ClientSize.Width / 2f, form.ClientSize.Height / 2f);
        }

        private static PointF ConvertMathToWindow(PointF mathPoint, PointF shoulderPos)
        {
            return new PointF(mathPoint.X + shoulderPos.X, shoulderPos.Y - mathPoint.Y);
        }

        private static PointF ConvertWindowToMath(PointF windowPoint, PointF shoulderPos)
        {
            return new PointF(windowPoint.X - shoulderPos.X, shoulderPos.Y - windowPoint.Y);
        }

        private static void DrawManipulator(Graphics graphics, PointF[] windowJoints, int radius)
        {
            var diameter = radius * 2;
            for (var i = 0; i < windowJoints.Length - 1; i++)
            {
                graphics.DrawLine(ManipulatorPen, windowJoints[i], windowJoints[i + 1]);
                graphics.FillEllipse(JointBrush, windowJoints[i].X - radius, windowJoints[i].Y - radius, diameter,
                    diameter);
            }
        }

        private static PointF[] GetAllWindowPoints(PointF[] joints, PointF shoulderPos)
        {
            return new[]
            {
                ConvertMathToWindow(new PointF {X = 0, Y = 0}, shoulderPos),
                ConvertMathToWindow(joints[0], shoulderPos),
                ConvertMathToWindow(joints[1], shoulderPos),
                ConvertMathToWindow(joints[2], shoulderPos)
            };
        }

        private static void DrawReachableZone(
            Graphics graphics,
            Brush reachableBrush,
            Brush unreachableBrush,
            PointF shoulderPos,
            PointF[] joints)
        {
            var rmin = Math.Abs(Manipulator.UpperArm - Manipulator.Forearm);
            var rmax = Manipulator.UpperArm + Manipulator.Forearm;
            var mathCenter = new PointF(joints[2].X - joints[1].X, joints[2].Y - joints[1].Y);
            var windowCenter = ConvertMathToWindow(mathCenter, shoulderPos);
            graphics.FillEllipse(reachableBrush, windowCenter.X - rmax, windowCenter.Y - rmax, 2 * rmax, 2 * rmax);
            graphics.FillEllipse(unreachableBrush, windowCenter.X - rmin, windowCenter.Y - rmin, 2 * rmin, 2 * rmin);
        }
    }
}