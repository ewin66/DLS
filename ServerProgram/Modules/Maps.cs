using System;
using System.Linq;
using System.Xml.Linq;
using DevExpress.XtraMap;
using System.Collections.Generic;
using DevExpress.Demos.OpenWeatherService;

namespace DevExpress.ProductsDemo.Win.Modules {
    public partial class MapsModule : BaseModule {
        OpenWeatherMapService openWeatherMapService;
        CityWeather actualWeatherInfo;
        TemperatureMeasureUnits actualMeasureUnits = TemperatureMeasureUnits.Celsius;

        public MapControl MapControl { get { return mapControl1; } }
        protected OpenWeatherMapService OpenWeatherMapService { get { return openWeatherMapService; } }
        ImageTilesLayer TilesLayer { get { return (ImageTilesLayer)(mapControl1.Layers[0]); } }
        VectorItemsLayer ItemsLayer { get { return (VectorItemsLayer)(mapControl1.Layers[1]); } }
        ListSourceDataAdapter DataAdapter { get { return (ListSourceDataAdapter)ItemsLayer.Data; } }


        public MapsModule() {
            InitializeComponent();

            TilesLayer.DataProvider = MapUtils.CreateBingDataProvider(BingMapKind.Area);
            mapControl1.SetMapItemFactory(new DemoWeatherItemFactory());

            this.openWeatherMapService = new OpenWeatherMapService(LoadCapitalsFromXML());
            OpenWeatherMapService.ReadCompleted += OpenWeatherMapService_ReadCompleted;
            openWeatherMapService.GetWeatherAsync();
        }
        protected override bool AutoMergeRibbon { get { return true;  } }

        List<string> LoadCapitalsFromXML() {
            List<string> capitals = new List<string>();
            XDocument document = MapUtils.LoadXml("Capitals.xml");
            if(document != null) {
                foreach(XElement element in document.Element("Capitals").Elements()) {
                    capitals.Add(element.Value);
                }
            }
            return capitals;
        }
        void OpenWeatherMapService_ReadCompleted(object sender, EventArgs e) {
            DataAdapter.DataSource = OpenWeatherMapService.WeatherInCities;
            ItemsLayer.SelectedItem = OpenWeatherMapService.LosAngelesWeather;
            OpenWeatherMapService.ReadCompleted -= OpenWeatherMapService_ReadCompleted;
        }

        void mapControl1_SelectionChanged(object sender, MapSelectionChangedEventArgs e) {
            IList<object> selection = e.Selection;
            if(selection == null || selection.Count != 1)
                return;
            CityWeather cityWeatherInfo = selection[0] as CityWeather;
            this.actualWeatherInfo = cityWeatherInfo;
            if(cityWeatherInfo != null) {
                if(cityWeatherInfo.Forecast == null) {
                    OpenWeatherMapService.GetForecastForCityAsync(cityWeatherInfo);
                    cityWeatherInfo.ForecastUpdated += cityWeatherInfo_ForecastUpdated;
                } else
                    cityWeatherInfo_ForecastUpdated(cityWeatherInfo, null);
            }
        }
        void cityWeatherInfo_ForecastUpdated(object sender, EventArgs e) {
            CityWeather cityWeatherInfo = sender as CityWeather;
            Action<CityWeather> del = LoadWeatherPicture;
            BeginInvoke(del, cityWeatherInfo);
        }
        void LoadWeatherPicture(CityWeather cityWeatherInfo) {
            this.chartControl1.Series[0].DataSource = cityWeatherInfo.Forecast;
            lbCity.Text = cityWeatherInfo.City;
            lbTemperature.Text = cityWeatherInfo.Weather.GetTemperatureString(actualMeasureUnits);
            peWeatherIcon.LoadAsync(cityWeatherInfo.WeatherIconPath);
        }
        private void mapControl1_SelectionChanging(object sender, MapSelectionChangingEventArgs e) {
            e.Cancel = e.Selection.Count == 0;
        }

        void chkCelsius_CheckedChanged(object sender, XtraBars.ItemClickEventArgs e) {
            UpdateTemperatureUnit((int)e.Item.Tag);

        }
        void UpdateTemperatureUnit(int temperatureType) {
            if(actualWeatherInfo != null) {
                string member = "Weather.CelsiusTemperature";
                actualMeasureUnits = TemperatureMeasureUnits.Celsius;
                
                if(temperatureType == 0) {
                    actualMeasureUnits = TemperatureMeasureUnits.Fahrenheit;
                    member = "Weather.FahrenheitTemperature";
                }
                this.chartControl1.Series[0].ValueDataMembers[0] = member;
                lbTemperature.Text = actualWeatherInfo.Weather.GetTemperatureString(actualMeasureUnits);
                DataAdapter.Mappings.Text = actualWeatherInfo.GetTemperatureDataMember(actualMeasureUnits);
            }
        }
        void chkBingRoad_CheckedChanged(object sender, XtraBars.ItemClickEventArgs e) {
            UpdateBingMapKind((int)e.Item.Tag);
        }
        void UpdateBingMapKind(int bingMapKind) {
            BingMapDataProvider provider = (BingMapDataProvider)TilesLayer.DataProvider;
            provider.Kind = (BingMapKind)bingMapKind;
        }


    }
}
