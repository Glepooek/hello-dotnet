using DirectShowLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Learning.PrismDemo.Cameras
{
	/// <summary>
	/// 摄像头工具类
	/// </summary>
	public class CameraUtils
	{
		#region Public Methods

		/// <summary>
		/// 获取摄像头列表
		/// </summary>
		/// <returns></returns>
		public static List<Camera> GetCameraDevicelist()
		{
			List<DsDevice> devices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice).ToList<DsDevice>();
			List<Camera> list = new List<Camera>();

			foreach (var item in devices)
			{
				list.Add(new Camera()
				{
					ID = devices.IndexOf(item),
					Name = item.Name,
					MonikerString = item.DevicePath
				});
			}

			return list;
		}

		///// <summary>
		///// 判断设置中的默认摄像头是否可用,不可用则默认启用摄像头列表索引为0的摄像头
		///// </summary>
		///// <returns>true/false</returns>
		//public static bool IsDefaultCameraExisted(out string message)
		//{
		//    message = string.Empty;
		//    List<Camera> cameraList = GetCameraDevicelist();
		//    int cameraListCount = cameraList.Count;

		//    if (cameraList != null && cameraListCount > 0)
		//    {
		//        if (cameraList.Any(c => c.ID == SettingManager.Instance.SettingInfo.DefaultCameraID)
		//            && !string.IsNullOrEmpty(SettingManager.Instance.SettingInfo.DefaultCameraResolution))
		//        {
		//            var CameraResolutionList = GetCameraResolutionByCameraID(SettingManager.Instance.SettingInfo.DefaultCameraID);//获取当前默认摄像头的分辨率集合
		//            if (CameraResolutionList == null)
		//            {
		//                message = "摄像头无可用分辨率，这通常是用户不允许其他应用访问系统相机，请检查设置";
		//                return false;
		//            }
		//            else if (!CameraResolutionList.Any(c => c == SettingManager.Instance.SettingInfo.DefaultCameraResolution))
		//            {
		//                SettingManager.Instance.SettingInfo.DefaultCameraResolution = CameraResolutionList.Last();
		//            }
		//            return true;
		//        }
		//        else
		//        {
		//            if (cameraListCount > SettingManager.Instance.SettingInfo.DefaultCameraID &&
		//                SettingManager.Instance.SettingInfo.DefaultCameraID >= 0)//设置的摄像头存在 但是分辨率未设置
		//            {
		//                var result = SetDefaultCamera(SettingManager.Instance.SettingInfo.DefaultCameraID);
		//                if (result)
		//                    message = "未设置默认摄像头分辨率,已为你分配默认分辨率";
		//                else
		//                    message = "摄像头无可用分辨率，这通常是用户不允许其他应用访问系统相机，请检查设置";
		//                return result;
		//            }
		//            else//设置的摄像头不存在且分辨率未设置
		//            {
		//                //配置的摄像头不存在，
		//                var result = SetDefaultCamera(SettingManager.Instance.SettingInfo.DefaultCameraID = 0);
		//                if (result)
		//                    message = "设置的默认摄像头不可用，已为你分配默认摄像头和默认分辨率";
		//                else
		//                    message = "摄像头无可用分辨率，这通常是用户不允许其他应用访问系统相机，请检查设置";
		//                return result;
		//            }
		//        }
		//    }
		//    else
		//    {
		//        message = "未检测到摄像头。请确认摄像头是否已经正确连接，或摄像头驱动是否正确安装！";
		//    }

		//    return false;
		//}

		///// <summary>
		///// 设置默认摄像头的分辨率
		///// </summary>
		///// <param name="cameraId"></param>
		//public static bool SetDefaultCamera(int cameraId)
		//{
		//    var CameraResolutionList = GetCameraResolutionByCameraID(cameraId);

		//    if (CameraResolutionList != null && CameraResolutionList.Count > 0)
		//    {
		//        if (CameraResolutionList.Any(c => c == "1280*720"))
		//        {
		//            SettingManager.Instance.SettingInfo.DefaultCameraResolution = "1280*720";

		//        }
		//        else
		//        {
		//            SettingManager.Instance.SettingInfo.DefaultCameraResolution = CameraResolutionList.Last();
		//            //LogHelper.Instance.Debug(string.Format("默认摄像头的分辨率为{0}", SettingManager.Instance.SettingInfo.DefaultCameraResolution));
		//        }
		//        return true;
		//    }

		//    return false;

		//}

		///// <summary>
		///// 根据摄像头ID获取其分辨率集合
		///// <para>
		///// 已经过滤掉低于352*288的分辨率;
		///// 根据分辨率宽度降序排序
		///// </para>
		///// </summary>
		///// <param name="cameraId">摄像头id</param>
		///// <returns>分辨率集合</returns>
		//public static List<string> GetCameraResolutionByCameraID(int cameraId)
		//{
		//    try
		//    {
		//        //分辨率 352 * 288
		//        int minWidthResolution = 352;
		//        int minHeightResolution = 288;
		//        //分辨率 640 * 480
		//        int maxWidthResolution = 720;
		//        int maxHeighetResolution = 576;

		//        switch (SettingManager.Instance.SettingInfo.MaxVideoSize)
		//        {
		//            case "D1":
		//                maxWidthResolution = 1280;
		//                maxHeighetResolution = 720;
		//                break;
		//            case "720P":
		//                maxWidthResolution = 1920;
		//                maxHeighetResolution = 1080;
		//                break;
		//            case "1080P":
		//                maxWidthResolution = 3840;
		//                maxHeighetResolution = 2160;
		//                break;
		//            default:
		//                break;
		//        }

		//        List<string> cameraResList = new List<string>();
		//        // 获取列表
		//        DsDevice[] devices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
		//        DsDevice ds = devices.FirstOrDefault<DsDevice>(d => Array.IndexOf(devices, d) == cameraId);
		//        if (ds == null)
		//        {
		//            return null;
		//        }
		//        // 根据摄像头ID获取设备，并通过设备路径（IMoniker）获取其分辨率
		//        ResolutionList list = GetResolutionList(ds.Mon);

		//        foreach (var item in list)
		//        {
		//            cameraResList.Add(item.ToString());
		//        }

		//        if (cameraResList.Count > 0)
		//        {
		//            cameraResList = cameraResList.FindAll(d =>
		//                d.Split<int>("*")[0] >= minWidthResolution &&
		//                d.Split<int>("*")[0] < maxWidthResolution &&
		//                d.Split<int>("*")[1] >= minHeightResolution &&
		//                d.Split<int>("*")[1] < maxHeighetResolution).Distinct().OrderByDescending(s => s.Split<int>("*")[0] * s.Split<int>("*")[1]).ToList();

		//            //    cameraResList = cameraResList.FindAll(d =>
		//            //        d.Split<int>("*")[1] >= 288).Distinct().OrderByDescending(s => s.Split<int>("*")[0]).ToList();
		//        }

		//        return cameraResList;
		//    }
		//    catch (Exception ex)
		//    {
		//        LogHelper.Instance.Error("CameraUtils.GetCameraResolutionByCameraID()方法出现异常：", ex);
		//        return null;
		//    }
		//}

		#endregion

		#region Private Methods

		/// <summary>
		/// Returns available resolutions with RGB color system for device moniker
		/// </summary>
		/// <param name="moniker">Moniker (device identification) of camera.</param>
		/// <returns>List of resolutions with RGB color system of device</returns>
		private static ResolutionList GetResolutionList(IMoniker moniker)
		{
			int hr;

			ResolutionList ResolutionsAvailable = null; //new ResolutionList();

			// Get the graphbuilder object
			IFilterGraph2 filterGraph = new FilterGraph() as IFilterGraph2;
			IBaseFilter capFilter = null;

			try
			{
				// add the video input device
				hr = filterGraph.AddSourceFilterForMoniker(moniker, null, "Source Filter", out capFilter);
				DsError.ThrowExceptionForHR(hr);

				ResolutionsAvailable = GetResolutionsAvailable(capFilter);
			}
			finally
			{
				SafeReleaseComObject(filterGraph);
				filterGraph = null;

				SafeReleaseComObject(capFilter);
				capFilter = null;
			}

			return ResolutionsAvailable;
		}

		/// <summary>
		/// Gets available resolutions (which are appropriate for us) for capture filter.
		/// </summary>
		/// <param name="captureFilter">Capture filter for asking for resolution list.</param>
		private static ResolutionList GetResolutionsAvailable(IBaseFilter captureFilter)
		{
			ResolutionList resolution_list = null;

			IPin pRaw = null;
			try
			{
				pRaw = DsFindPin.ByDirection(captureFilter, PinDirection.Output, 0);
				//pRaw = DsFindPin.ByCategory(captureFilter, PinCategory.Capture, 0);
				//pRaw = DsFindPin.ByCategory(filter, PinCategory.Preview, 0);

				resolution_list = GetResolutionsAvailable(pRaw);
			}
			catch
			{
				throw;
			}
			finally
			{
				SafeReleaseComObject(pRaw);
				pRaw = null;
			}

			return resolution_list;
		}

		/// <summary>
		/// Gets available resolutions (which are appropriate for us) for capture pin (PinCategory.Capture).
		/// </summary>
		/// <param name="pinOutput">Capture pin (PinCategory.Capture) for asking for resolution list.</param>
		private static ResolutionList GetResolutionsAvailable(IPin pinOutput)
		{
			int hr = 0;

			ResolutionList ResolutionsAvailable = new ResolutionList();

			//ResolutionsAvailable.Clear();

			// Media type (shoudl be cleaned)
			AMMediaType media_type = null;

			//NOTE: pSCC is not used. All we need is media_type
			IntPtr pSCC = IntPtr.Zero;

			try
			{
				IAMStreamConfig videoStreamConfig = pinOutput as IAMStreamConfig;

				// -------------------------------------------------------------------------
				// We want the interface to expose all media types it supports and not only the last one set
				hr = videoStreamConfig.SetFormat(null);
				DsError.ThrowExceptionForHR(hr);

				int piCount = 0;
				int piSize = 0;

				hr = videoStreamConfig.GetNumberOfCapabilities(out piCount, out piSize);
				DsError.ThrowExceptionForHR(hr);

				for (int i = 0; i < piCount; i++)
				{
					// ---------------------------------------------------
					pSCC = Marshal.AllocCoTaskMem(piSize);
					videoStreamConfig.GetStreamCaps(i, out media_type, pSCC);

					// NOTE: we could use VideoStreamConfigCaps.InputSize or something like that to get resolution, but it's deprecated
					//VideoStreamConfigCaps videoStreamConfigCaps = (VideoStreamConfigCaps)Marshal.PtrToStructure(pSCC, typeof(VideoStreamConfigCaps));
					// ---------------------------------------------------

					if (IsBitCountAppropriate(GetBitCountForMediaType(media_type)))
					{
						ResolutionsAvailable.AddIfNew(GetResolutionForMediaType(media_type));
					}

					FreeSCCMemory(ref pSCC);
					FreeMediaType(ref media_type);
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				// clean up
				FreeSCCMemory(ref pSCC);
				FreeMediaType(ref media_type);
			}

			return ResolutionsAvailable;
		}

		/// <summary>
		/// Releases COM object
		/// </summary>
		/// <param name="obj">COM object to release.</param>
		private static void SafeReleaseComObject(object obj)
		{
			if (obj != null)
			{
				Marshal.ReleaseComObject(obj);
			}
		}

		/// <summary>
		/// Free SCC (it's not used but required for GetStreamCaps()).
		/// </summary>
		/// <param name="pSCC">SCC to free.</param>
		private static void FreeSCCMemory(ref IntPtr pSCC)
		{
			if (pSCC == IntPtr.Zero)
				return;

			Marshal.FreeCoTaskMem(pSCC);
			pSCC = IntPtr.Zero;
		}

		/// <summary>
		/// Free media type if needed.
		/// </summary>
		/// <param name="media_type">Media type to free.</param>
		private static void FreeMediaType(ref AMMediaType media_type)
		{
			if (media_type == null)
				return;

			DsUtils.FreeAMMediaType(media_type);
			media_type = null;
		}

		/// <summary>
		/// Check if bit count is appropriate for us
		/// </summary>
		/// <param name="bit_count"></param>
		private static bool IsBitCountAppropriate(short bit_count)
		{
			if (bit_count == 16 ||
				bit_count == 24 ||
				bit_count == 32)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Get bit count for mediatype
		/// </summary>
		/// <param name="media_type">Media type to analyze.</param>
		private static short GetBitCountForMediaType(AMMediaType media_type)
		{
			VideoInfoHeader videoInfoHeader = new VideoInfoHeader();
			Marshal.PtrToStructure(media_type.formatPtr, videoInfoHeader);

			return videoInfoHeader.BmiHeader.BitCount;
		}

		/// <summary>
		/// Get resoltuin from if AMMediaType's resolution is appropriate for resolution_desired
		/// </summary>
		/// <param name="media_type">Media type to analyze.</param>
		private static Resolution GetResolutionForMediaType(AMMediaType media_type)
		{
			VideoInfoHeader videoInfoHeader = new VideoInfoHeader();
			Marshal.PtrToStructure(media_type.formatPtr, videoInfoHeader);

			return new Resolution(videoInfoHeader.BmiHeader.Width, videoInfoHeader.BmiHeader.Height);
		}

		#endregion
	}
}
