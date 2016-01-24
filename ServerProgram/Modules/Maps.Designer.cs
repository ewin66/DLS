using DevExpress.XtraMap;
namespace DevExpress.ProductsDemo.Win.Modules {
    partial class MapsModule {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            DevExpress.XtraMap.ImageTilesLayer imageTilesLayer1 = new DevExpress.XtraMap.ImageTilesLayer();
            DevExpress.XtraMap.VectorItemsLayer vectorItemsLayer1 = new DevExpress.XtraMap.VectorItemsLayer();
            DevExpress.XtraMap.ListSourceDataAdapter listSourceDataAdapter1 = new ListSourceDataAdapter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapsModule));
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
            this.mapContainerPanel = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.mapControl1 = new DevExpress.XtraMap.MapControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.chkBingRoad = new DevExpress.XtraBars.BarCheckItem();
            this.chkBingArea = new DevExpress.XtraBars.BarCheckItem();
            this.chkBingHybrid = new DevExpress.XtraBars.BarCheckItem();
            this.chkFahrenheit = new DevExpress.XtraBars.BarCheckItem();
            this.chkCelsius = new DevExpress.XtraBars.BarCheckItem();
            this.viewRibbonPage1 = new DevExpress.XtraScheduler.UI.ViewRibbonPage();
            this.activeViewRibbonPageGroup1 = new DevExpress.XtraScheduler.UI.ActiveViewRibbonPageGroup();
            this.timeScaleRibbonPageGroup1 = new DevExpress.XtraScheduler.UI.TimeScaleRibbonPageGroup();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lbTemperature = new DevExpress.XtraEditors.LabelControl();
            this.peWeatherIcon = new DevExpress.XtraEditors.PictureEdit();
            this.lbCity = new DevExpress.XtraEditors.LabelControl();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.mapContainerPanel)).BeginInit();
            this.mapContainerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peWeatherIcon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
            this.SuspendLayout();
            // 
            // mapContainerPanel
            // 
            this.mapContainerPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mapContainerPanel.Controls.Add(this.layoutControl1);
            this.mapContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapContainerPanel.Location = new System.Drawing.Point(0, 142);
            this.mapContainerPanel.Margin = new System.Windows.Forms.Padding(23);
            this.mapContainerPanel.Name = "mapContainerPanel";
            this.mapContainerPanel.Size = new System.Drawing.Size(1053, 597);
            this.mapContainerPanel.TabIndex = 15;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.mapControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(376, 238, 905, 577);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1053, 597);
            this.layoutControl1.TabIndex = 16;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // mapControl1
            // 
            this.mapControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(7)))), ((int)(((byte)(21)))));
            this.mapControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mapControl1.CenterPoint = new DevExpress.XtraMap.GeoPoint(5D, -70D);
            listSourceDataAdapter1.Mappings.Latitude = "Latitude";
            listSourceDataAdapter1.Mappings.Longitude = "Longitude";
            listSourceDataAdapter1.Mappings.Text = "CelsiusDisplayText";
            vectorItemsLayer1.Data = listSourceDataAdapter1;
            this.mapControl1.Layers.Add(imageTilesLayer1);
            this.mapControl1.Layers.Add(vectorItemsLayer1);
            this.mapControl1.Location = new System.Drawing.Point(12, 12);
            this.mapControl1.MinZoomLevel = 1.7D;
            this.mapControl1.Name = "mapControl1";
            this.mapControl1.SelectionMode = DevExpress.XtraMap.ElementSelectionMode.Single;
            this.mapControl1.Size = new System.Drawing.Size(1029, 573);
            this.mapControl1.TabIndex = 17;
            this.mapControl1.ZoomLevel = 3D;
            this.mapControl1.SelectionChanging += new DevExpress.XtraMap.MapSelectionChangingEventHandler(this.mapControl1_SelectionChanging);
            this.mapControl1.SelectionChanged += new DevExpress.XtraMap.MapSelectionChangedEventHandler(this.mapControl1_SelectionChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1053, 597);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.mapControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1033, 577);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.chkBingRoad,
            this.chkBingArea,
            this.chkBingHybrid,
            this.chkFahrenheit,
            this.chkCelsius});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 45;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.viewRibbonPage1});
            this.ribbonControl1.Size = new System.Drawing.Size(1053, 142);
            // 
            // chkBingRoad
            // 
            this.chkBingRoad.Caption = "Road";
            this.chkBingRoad.Glyph = ((System.Drawing.Image)(resources.GetObject("chkBingRoad.Glyph")));
            this.chkBingRoad.GroupIndex = 1;
            this.chkBingRoad.Id = 40;
            this.chkBingRoad.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("chkBingRoad.LargeGlyph")));
            this.chkBingRoad.Name = "chkBingRoad";
            this.chkBingRoad.Tag = 0;
            this.chkBingRoad.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.chkBingRoad_CheckedChanged);
            // 
            // chkBingArea
            // 
            this.chkBingArea.Caption = "Area";
            this.chkBingArea.Checked = true;
            this.chkBingArea.Glyph = ((System.Drawing.Image)(resources.GetObject("chkBingArea.Glyph")));
            this.chkBingArea.GroupIndex = 1;
            this.chkBingArea.Id = 41;
            this.chkBingArea.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("chkBingArea.LargeGlyph")));
            this.chkBingArea.Name = "chkBingArea";
            this.chkBingArea.Tag = 1;
            this.chkBingArea.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.chkBingRoad_CheckedChanged);
            // 
            // chkBingHybrid
            // 
            this.chkBingHybrid.Caption = "Hybrid";
            this.chkBingHybrid.Glyph = ((System.Drawing.Image)(resources.GetObject("chkBingHybrid.Glyph")));
            this.chkBingHybrid.GroupIndex = 1;
            this.chkBingHybrid.Id = 42;
            this.chkBingHybrid.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("chkBingHybrid.LargeGlyph")));
            this.chkBingHybrid.Name = "chkBingHybrid";
            this.chkBingHybrid.Tag = 2;
            this.chkBingHybrid.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.chkBingRoad_CheckedChanged);
            // 
            // chkFahrenheit
            // 
            this.chkFahrenheit.Caption = "Fahrenheit";
            this.chkFahrenheit.Glyph = ((System.Drawing.Image)(resources.GetObject("chkFahrenheit.Glyph")));
            this.chkFahrenheit.GroupIndex = 1;
            this.chkFahrenheit.Id = 43;
            this.chkFahrenheit.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("chkFahrenheit.LargeGlyph")));
            this.chkFahrenheit.Name = "chkFahrenheit";
            this.chkFahrenheit.Tag = 0;
            this.chkFahrenheit.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.chkCelsius_CheckedChanged);
            // 
            // chkCelsius
            // 
            this.chkCelsius.Caption = "Celsius";
            this.chkCelsius.Checked = true;
            this.chkCelsius.Glyph = ((System.Drawing.Image)(resources.GetObject("chkCelsius.Glyph")));
            this.chkCelsius.GroupIndex = 1;
            this.chkCelsius.Id = 44;
            this.chkCelsius.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("chkCelsius.LargeGlyph")));
            this.chkCelsius.Name = "chkCelsius";
            this.chkCelsius.Tag = 1;
            this.chkCelsius.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.chkCelsius_CheckedChanged);
            // 
            // viewRibbonPage1
            // 
            this.viewRibbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.activeViewRibbonPageGroup1,
            this.timeScaleRibbonPageGroup1});
            this.viewRibbonPage1.Name = "viewRibbonPage1";
            // 
            // activeViewRibbonPageGroup1
            // 
            this.activeViewRibbonPageGroup1.AllowTextClipping = false;
            this.activeViewRibbonPageGroup1.ItemLinks.Add(this.chkBingRoad);
            this.activeViewRibbonPageGroup1.ItemLinks.Add(this.chkBingArea);
            this.activeViewRibbonPageGroup1.ItemLinks.Add(this.chkBingHybrid);
            this.activeViewRibbonPageGroup1.Name = "activeViewRibbonPageGroup1";
            this.activeViewRibbonPageGroup1.Text = "BingMap Kind";
            // 
            // timeScaleRibbonPageGroup1
            // 
            this.timeScaleRibbonPageGroup1.AllowTextClipping = false;
            this.timeScaleRibbonPageGroup1.ItemLinks.Add(this.chkFahrenheit);
            this.timeScaleRibbonPageGroup1.ItemLinks.Add(this.chkCelsius);
            this.timeScaleRibbonPageGroup1.Name = "timeScaleRibbonPageGroup1";
            this.timeScaleRibbonPageGroup1.Text = "TemperatureUnit";
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.Black;
            this.panelControl1.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(71)))), ((int)(((byte)(80)))));
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.Appearance.Options.UseBorderColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Controls.Add(this.chartControl1);
            this.panelControl1.Location = new System.Drawing.Point(21, 21);
            this.panelControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.panelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(13, 13, 23, 14);
            this.panelControl1.Size = new System.Drawing.Size(320, 267);
            this.panelControl1.TabIndex = 17;
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.Black;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.lbTemperature);
            this.panelControl2.Controls.Add(this.peWeatherIcon);
            this.panelControl2.Controls.Add(this.lbCity);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(15, 15);
            this.panelControl2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(280, 57);
            this.panelControl2.TabIndex = 15;
            // 
            // lbTemperature
            // 
            this.lbTemperature.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbTemperature.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbTemperature.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbTemperature.Location = new System.Drawing.Point(53, 26);
            this.lbTemperature.Name = "lbTemperature";
            this.lbTemperature.Size = new System.Drawing.Size(182, 34);
            this.lbTemperature.TabIndex = 2;
            // 
            // peWeatherIcon
            // 
            this.peWeatherIcon.Location = new System.Drawing.Point(0, 19);
            this.peWeatherIcon.Name = "peWeatherIcon";
            this.peWeatherIcon.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peWeatherIcon.Properties.Appearance.Options.UseBackColor = true;
            this.peWeatherIcon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.peWeatherIcon.Properties.NullText = " ";
            this.peWeatherIcon.Properties.PictureAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.peWeatherIcon.Properties.ShowMenu = false;
            this.peWeatherIcon.Size = new System.Drawing.Size(49, 41);
            this.peWeatherIcon.TabIndex = 1;
            // 
            // lbCity
            // 
            this.lbCity.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCity.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbCity.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbCity.Location = new System.Drawing.Point(14, 0);
            this.lbCity.Name = "lbCity";
            this.lbCity.Size = new System.Drawing.Size(249, 21);
            this.lbCity.TabIndex = 0;
            // 
            // chartControl1
            // 
            this.chartControl1.BackColor = System.Drawing.Color.Black;
            this.chartControl1.BorderOptions.Visibility = Utils.DefaultBoolean.False;
            xyDiagram1.AxisX.Color = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(173)))), ((int)(((byte)(173)))));
            xyDiagram1.AxisX.DateTimeScaleOptions.AutoGrid = false;
            xyDiagram1.AxisX.DateTimeScaleOptions.GridAlignment = DevExpress.XtraCharts.DateTimeGridAlignment.Day;
            xyDiagram1.AxisX.DateTimeScaleOptions.MeasureUnit = DevExpress.XtraCharts.DateTimeMeasureUnit.Hour;
            xyDiagram1.AxisX.GridLines.Color = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            xyDiagram1.AxisX.GridLines.MinorColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            xyDiagram1.AxisX.GridLines.MinorVisible = true;
            xyDiagram1.AxisX.GridLines.Visible = true;
            xyDiagram1.AxisX.Interlaced = true;
            xyDiagram1.AxisX.InterlacedColor = System.Drawing.Color.Transparent;
            xyDiagram1.AxisX.Label.TextPattern = "{A:dd.MM}";
            xyDiagram1.AxisX.Label.ResolveOverlappingOptions.AllowHide = false;
            xyDiagram1.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
            xyDiagram1.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
            xyDiagram1.AxisX.Label.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(173)))), ((int)(((byte)(173)))));
            xyDiagram1.AxisX.Tickmarks.MinorVisible = false;
            xyDiagram1.AxisX.Tickmarks.Visible = false;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.Color = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            xyDiagram1.AxisY.GridLines.Color = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            xyDiagram1.AxisY.GridLines.MinorColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            xyDiagram1.AxisY.InterlacedColor = System.Drawing.Color.Transparent;
            xyDiagram1.AxisY.Label.ResolveOverlappingOptions.AllowRotate = false;
            xyDiagram1.AxisY.Label.ResolveOverlappingOptions.AllowStagger = false;
            xyDiagram1.AxisY.Label.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(173)))), ((int)(((byte)(173)))));
            xyDiagram1.AxisY.Tickmarks.MinorVisible = false;
            xyDiagram1.AxisY.Tickmarks.Visible = false;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            xyDiagram1.DefaultPane.BackColor = System.Drawing.Color.Transparent;
            xyDiagram1.Margins.Bottom = 2;
            xyDiagram1.Margins.Left = 0;
            xyDiagram1.Margins.Right = 0;
            xyDiagram1.Margins.Top = 2;
            this.chartControl1.Diagram = xyDiagram1;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chartControl1.Legend.Visibility = Utils.DefaultBoolean.False;
            this.chartControl1.Location = new System.Drawing.Point(15, 72);
            this.chartControl1.Margin = new System.Windows.Forms.Padding(0);
            this.chartControl1.Name = "chartControl1";
            series1.ArgumentDataMember = "CurrentDateTime";
            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series1.CrosshairLabelPattern = "{A:g}: {V}";
            series1.Name = "Series 1";
            series1.ValueDataMembersSerializable = "Weather.CelsiusTemperature";
            series1.View = lineSeriesView1;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl1.SeriesTemplate.View = lineSeriesView2;
            this.chartControl1.Size = new System.Drawing.Size(280, 179);
            this.chartControl1.TabIndex = 14;
            // 
            // MapsModule
            // 
            this.Appearance.Options.UseFont = true;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.mapContainerPanel);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "MapsModule";
            this.Size = new System.Drawing.Size(1053, 739);
            ((System.ComponentModel.ISupportInitialize)(this.mapContainerPanel)).EndInit();
            this.mapContainerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.peWeatherIcon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private XtraEditors.PanelControl mapContainerPanel;
        private XtraBars.Ribbon.RibbonControl ribbonControl1;
        private XtraScheduler.UI.ViewRibbonPage viewRibbonPage1;
        private XtraScheduler.UI.ActiveViewRibbonPageGroup activeViewRibbonPageGroup1;
        private XtraScheduler.UI.TimeScaleRibbonPageGroup timeScaleRibbonPageGroup1;
        private XtraBars.BarCheckItem chkBingRoad;
        private XtraBars.BarCheckItem chkBingArea;
        private XtraBars.BarCheckItem chkBingHybrid;
        private XtraBars.BarCheckItem chkFahrenheit;
        private XtraBars.BarCheckItem chkCelsius;
        private XtraLayout.LayoutControl layoutControl1;
        private MapControl mapControl1;
        private XtraLayout.LayoutControlGroup layoutControlGroup1;
        private XtraLayout.LayoutControlItem layoutControlItem1;
        private XtraEditors.PanelControl panelControl1;
        private XtraEditors.PanelControl panelControl2;
        private XtraEditors.LabelControl lbTemperature;
        private XtraEditors.PictureEdit peWeatherIcon;
        private XtraEditors.LabelControl lbCity;
        private XtraCharts.ChartControl chartControl1;

    }
}
