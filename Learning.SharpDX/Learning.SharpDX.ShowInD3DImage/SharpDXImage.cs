using SharpDX.Direct3D;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using D2D = SharpDX.Direct2D1;
using D3D11 = SharpDX.Direct3D11;
using D3D9 = SharpDX.Direct3D9;
using DXGI = SharpDX.DXGI;

// https://www.cnblogs.com/lindexi/p/12087076.html
// D3DImage，显示用户创建的 Direct3D 图面

namespace Learning.SharpDX.ShowInD3DImage
{
    /// <summary>
    /// WPF使用SharpDX在D3DImage显示
    /// </summary>    
    public abstract class SharpDXImage : D3DImage
    {
        #region Fields

        private D3D9.Texture _renderTarget;
        private D2D.RenderTarget _d2DRenderTarget;

        #endregion

        public void CreateAndBindTargets(int actualWidth, int actualHeight)
        {
            var width = Math.Max(actualWidth, 100);
            var height = Math.Max(actualHeight, 100);

            var renderDesc = new D3D11.Texture2DDescription
            {
                BindFlags = D3D11.BindFlags.RenderTarget | D3D11.BindFlags.ShaderResource,
                Format = DXGI.Format.B8G8R8A8_UNorm,
                Width = width,
                Height = height,
                MipLevels = 1,
                SampleDescription = new DXGI.SampleDescription(1, 0),
                Usage = D3D11.ResourceUsage.Default,
                OptionFlags = D3D11.ResourceOptionFlags.Shared,
                CpuAccessFlags = D3D11.CpuAccessFlags.None,
                ArraySize = 1
            };

            // 创建设备。
            // 如果需要使用 Direct2D 渲染，需要先创建 D3D11 的设备，因为实际的渲染是通过 3D 渲染。
            var device = new D3D11.Device(DriverType.Hardware, D3D11.DeviceCreationFlags.BgraSupport);
            // 创建指针。
            // 传入D3DImage中
            var renderTarget = new D3D11.Texture2D(device, renderDesc);
            // 创建缓冲区
            var surface = renderTarget.QueryInterface<DXGI.Surface>();
            var d2DFactory = new D2D.Factory();
            var format = new D2D.PixelFormat(DXGI.Format.Unknown, D2D.AlphaMode.Premultiplied);
            var renderTargetProperties = new D2D.RenderTargetProperties(format);

            _d2DRenderTarget = new D2D.RenderTarget(d2DFactory, surface, renderTargetProperties);

            SetRenderTarget(renderTarget);

            device.ImmediateContext.Rasterizer.SetViewport(0, 0, width, height);

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        protected abstract void OnRender(D2D.RenderTarget renderTarget);

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            Rendering();
        }

        /// <summary>
        /// 绘制出显示图形
        /// </summary>
        private void Rendering()
        {
            _d2DRenderTarget.BeginDraw();

            // 实际执行绘制的方法
            OnRender(_d2DRenderTarget);

            _d2DRenderTarget.EndDraw();

            Lock();

            // 每次重新渲染
            AddDirtyRect(new Int32Rect(0, 0, PixelWidth, PixelHeight));

            Unlock();
        }

        private void SetRenderTarget(D3D11.Texture2D target)
        {
            // 首先转换Format，因为D3D11.Texture2D使用的是SharpDX.DXGI.Format需要转换为 D3D9.Format
            var format = TranslateFormat(target);
            // 获取D3D11.Texture2D的指针
            var handle = GetSharedHandle(target);
            // 获取窗口指针
            var presentParams = GetPresentParameters();
            var createFlags = D3D9.CreateFlags.HardwareVertexProcessing |
                              D3D9.CreateFlags.Multithreaded |
                              D3D9.CreateFlags.FpuPreserve;
            // 实例D3D9.Device，需要D3D9.Direct3DEx 
            var d3DContext = new D3D9.Direct3DEx();
            var d3DDevice = new D3D9.DeviceEx(d3DContext, 0, D3D9.DeviceType.Hardware,
                IntPtr.Zero, createFlags,
                presentParams);

            // 创建D3D9.Texture，通过这个来给D3DImage传递指针
            _renderTarget = new D3D9.Texture(d3DDevice, target.Description.Width,
                target.Description.Height, 1,
                D3D9.Usage.RenderTarget, format, D3D9.Pool.Default, ref handle);

            using (D3D9.Surface surface = _renderTarget.GetSurfaceLevel(0))
            {
                Lock();
                SetBackBuffer(D3DResourceType.IDirect3DSurface9, surface.NativePointer);
                Unlock();
            }
        }

        private static D3D9.PresentParameters GetPresentParameters()
        {
            var presentParams = new D3D9.PresentParameters
            {
                Windowed = true,
                SwapEffect = D3D9.SwapEffect.Discard,
                DeviceWindowHandle = NativeMethods.GetDesktopWindow(),
                PresentationInterval = D3D9.PresentInterval.Default
            };

            return presentParams;
        }

        /// <summary>
        /// 获取D3D11.Texture2D的指针
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        private IntPtr GetSharedHandle(D3D11.Texture2D texture)
        {
            using (var resource = texture.QueryInterface<DXGI.Resource>())
            {
                return resource.SharedHandle;
            }
        }

        /// <summary>
        /// 转换显示格式
        /// </summary>
        /// <param name="texture"></param>
        /// <remarks>
        /// 创建好了D3D11.Texture2D，让 D3DImage显示需要使用 SetBackBuffer 设置。
        /// 因为传入D3D11.Texture2D ，但是D3DImage是dx9渲染的，所以需要转换一下。
        /// 首先转换Format，因为D3D11.Texture2D使用的是SharpDX.DXGI.Format需要转换为 D3D9.Format ，请看下面代码
        /// </remarks>
        /// <returns></returns>
        private static D3D9.Format TranslateFormat(D3D11.Texture2D texture)
        {
            switch (texture.Description.Format)
            {
                case DXGI.Format.R10G10B10A2_UNorm:
                    return D3D9.Format.A2B10G10R10;
                case DXGI.Format.R16G16B16A16_Float:
                    return D3D9.Format.A16B16G16R16F;
                case DXGI.Format.B8G8R8A8_UNorm:
                    return D3D9.Format.A8R8G8B8;
                default:
                    return D3D9.Format.Unknown;
            }
        }

        private static class NativeMethods
        {
            [DllImport("user32.dll", SetLastError = false)]
            public static extern IntPtr GetDesktopWindow();
        }
    }
}
