using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace DrawingIn3D
{
	/// <summary>
	/// Interaction logic for Materials.xaml
	/// </summary>

	public partial class HitTesting : System.Windows.Window
	{

		public HitTesting()
		{
			InitializeComponent();
		}

		private void ringVisual_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Point location = e.GetPosition(viewport);

			RayMeshGeometry3DHitTestResult meshHitResult = (RayMeshGeometry3DHitTestResult)VisualTreeHelper.HitTest(viewport, location);

			axisRotation.Axis = new Vector3D(
					 -meshHitResult.PointHit.Y, meshHitResult.PointHit.X, 0);

			DoubleAnimation animation = new DoubleAnimation();
			animation.To = 40;
			animation.DecelerationRatio = 1;
			animation.Duration = TimeSpan.FromSeconds(0.15);
			animation.AutoReverse = true;
			axisRotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, animation);
		}

		// Alternative implementation using the Viewport.MouseDown event.
		private void viewport_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Point location = e.GetPosition(viewport);
			HitTestResult hitResult = VisualTreeHelper.HitTest(viewport, location);

			if (hitResult != null && hitResult.VisualHit == ringVisual)
			{
				// Hit the ring.
			}

			RayMeshGeometry3DHitTestResult meshHitResult = hitResult as RayMeshGeometry3DHitTestResult;
			if (meshHitResult != null && meshHitResult.ModelHit == ringModel)
			{
				// Hit the ring.
			}
			if (meshHitResult != null && meshHitResult.MeshHit == ringMesh)
			{
				axisRotation.Axis = new Vector3D(
					-meshHitResult.PointHit.Y, meshHitResult.PointHit.X, 0);

				DoubleAnimation animation = new DoubleAnimation();
				animation.To = 40;
				animation.DecelerationRatio = 1;
				animation.Duration = TimeSpan.FromSeconds(0.15);
				animation.AutoReverse = true;
				axisRotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, animation);
			}
		}
	}
}
