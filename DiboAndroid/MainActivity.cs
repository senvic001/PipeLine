using Android.App;
using Android.Widget;
using Android.OS;
using Com.Amap.Api.Maps2d;
using Com.Amap.Api.Maps2d.Model;
using Com.Amap.Api.Location;
using Output = System.Diagnostics.Debug;

namespace DiboAndroid
{
    [Activity(Label = "DiboAndroid", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        MapView mapView = null;
        AMap aMap = null;
        Button button = null;
        AMapLocationClient locationClient = null;
        AMapLocationClientOption locationClientOption = null;
        MyLocationStyle locationStyle = null;
        CameraUpdate lastCamera = null;
        string lastPosition = "LastPosition";
        double[] lastPositionArray = new double[] { 30.32381 , 120.068663 };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            mapView = FindViewById<MapView>(Resource.Id.mapView);
            button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            mapView.OnCreate(savedInstanceState);

            locationClient = new AMapLocationClient(this.ApplicationContext);
            locationClientOption = new AMapLocationClientOption();
            locationClientOption.SetWifiActiveScan(false);
            locationClientOption.SetMockEnable(true);
            locationClientOption.SetLocationMode(AMapLocationClientOption.AMapLocationMode.DeviceSensors);

            locationClient.SetLocationOption(locationClientOption);
          
            locationClient.Location += AMapLocationClient_Location;

            if (aMap == null)
            {
                aMap = mapView.Map;
                if (lastCamera == null)
                {
                    lastCamera = CameraUpdateFactory.NewLatLngZoom(new LatLng(lastPositionArray[0],lastPositionArray[1]), 15);
                } 
               aMap.MoveCamera(lastCamera);

                if(locationStyle == null)
                {
                    locationStyle=new MyLocationStyle();
                    locationStyle.ShowMyLocation(true);
                    locationStyle.InvokeMyLocationType(MyLocationStyle.LocationTypeFollow);
                }
                
                aMap.SetMyLocationStyle(locationStyle);
                aMap.MyLocationEnabled = true;
                
                aMap.UiSettings.MyLocationButtonEnabled = true; //设置默认定位按钮是否显示，非必需设置。
                locationClient.StartLocation();
                //aMap.
            }
        }

        private void AMapLocationClient_Location(object sender, AMapLocationEventArgs e)
        {
            if (e.P0 == null) return;
            if(e.P0.ErrorCode!=0)
            {
                Output.WriteLine($"ErrorCode:{e.P0.ErrorCode},ErrorInfo:{e.P0.ErrorInfo}.");
                return;
            }

            Output.WriteLine(e.P0.ToStr());
            button.Text = $"{e.P0.Latitude},{e.P0.Longitude}";
            button.Invalidate();
            lastCamera = CameraUpdateFactory.NewLatLngZoom(new LatLng(e.P0.Latitude, e.P0.Longitude), 15);
            lastPositionArray[0] = e.P0.Latitude;
            lastPositionArray[1] = e.P0.Longitude;
            //aMap.MoveCamera(cameraUpdate);
            //throw new System.NotImplementedException();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            mapView.OnDestroy();
        }

        protected override void OnResume()
        {
            base.OnResume();
            mapView.OnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();
            mapView.OnPause();
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutDoubleArray(lastPosition, lastPositionArray);
            mapView.OnSaveInstanceState(outState);
        }
        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            lastPositionArray = savedInstanceState.GetDoubleArray(lastPosition);
            lastCamera = CameraUpdateFactory.NewLatLngZoom(new LatLng(lastPositionArray[0], lastPositionArray[1]), 15);
        }
    }
}

