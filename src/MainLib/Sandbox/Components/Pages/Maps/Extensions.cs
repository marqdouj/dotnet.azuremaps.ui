using Marqdouj.DotNet.AzureMaps.Map.Common;
using Marqdouj.DotNet.AzureMaps.Map.GeoJson;
using Marqdouj.DotNet.AzureMaps.Map.Interop;
using Marqdouj.DotNet.AzureMaps.Map.Layers;
using Marqdouj.DotNet.Web.Components.Css;
using Sandbox.Services;

namespace Sandbox.Components.Pages.Maps
{
    internal enum HRefSource
    {
        AzureDocs,
        //AzureMaps,
        //DemoApp,
        Examples,
        Sandbox,
    }

    internal static class HRefExtensions
    {
        public static string? ToHRefAddLayerExample(this MapLayerDef? layerDef) => layerDef?.LayerType.ToHRefAddLayerExample();

        public static string ToHRefAddLayerExample(this MapLayerType layerType)
        {
            var hRef = HRefSource.Examples.CodeUrl($"add{layerType}Layer.md");
            return hRef;
        }

        public static string? ToHRefAzureDocs(this MapLayerDef? layerDef) => layerDef?.LayerType.ToHRefAzureDocs();

        public static string ToHRefAzureDocs(this MapLayerType layerType)
        {
            string codePath = layerType switch
            {
                MapLayerType.Bubble => "map-add-bubble-layer",
                MapLayerType.HeatMap => "map-add-heat-map-layer",
                MapLayerType.Image => "map-add-image-layer",
                MapLayerType.Line => "map-add-line-layer",
                MapLayerType.Polygon => "map-add-shape",
                MapLayerType.PolygonExtrusion => "map-extruded-polygon",
                MapLayerType.Symbol => "map-add-pin",
                MapLayerType.Tile => "map-add-tile-layer",
                _ => throw new NotImplementedException(),
            };

            var hRef = HRefSource.AzureDocs.CodeUrl(codePath);
            return hRef;
        }

        public static string ToPageSource(this string name) => HRefSource.Sandbox.CodeUrl($"{name}.razor");

        public static string CodeUrl(this HRefSource source, string path)
        {
            string? url = source switch
            {
                HRefSource.AzureDocs => "https://learn.microsoft.com/en-us/azure/azure-maps",
                //HRefSource.AzureMaps => "https://github.com/marqdouj/dotnet.azuremaps/blob/master/src/Marqdouj.DotNet.AzureMaps/Marqdouj.DotNet.AzureMaps",
                HRefSource.Examples => "https://github.com/marqdouj/dotnet.azuremaps/blob/master/docs/examples/",
                HRefSource.Sandbox => "https://github.com/marqdouj/dotnet.azuremaps.ui/blob/master/src/MainLib/Sandbox/Components/Pages/Maps",
                _ => throw new NotImplementedException(),
            };
            return Path.Combine(url, path);
        }
    }

    internal static class LayerExtensions
    {
        public static async Task<MapLayerDef> GetDefaultLayerDef(this MapLayerType layerType, IDataService dataService)
        {
            return layerType switch
            {
                MapLayerType.Bubble => new BubbleLayerDef(),
                MapLayerType.HeatMap => await GetDefaultHeatMapLayerDef(dataService),
                MapLayerType.Image => await GetDefaultImageLayerDef(dataService),
                MapLayerType.Line => new LineLayerDef()
                {
                    Before = "labels",
                    Options = new()
                    {
                        StrokeColor = HtmlColorName.Blue.ToString(),
                        StrokeWidth = 4,
                    }
                },
                MapLayerType.Polygon => new PolygonLayerDef()
                {
                    Options = new()
                    {
                        FillColor = HtmlColorName.Red.ToString(),
                        FillOpacity = 0.7,
                    }
                },
                MapLayerType.PolygonExtrusion => new PolygonExtLayerDef()
                {
                    Options = new()
                    {
                        FillColor = HtmlColorName.Red.ToString(),
                        FillOpacity = 0.7,
                        Height = 500,
                    }
                },
                MapLayerType.Symbol => new SymbolLayerDef() { Options = new() { IconOptions = new() { Image = IconImage.Pin_Red } } },
                MapLayerType.Tile => new TileLayerDef()
                {
                    Options = new()
                    {
                        Opacity = 0.8,
                        TileSize = 256,
                        MinSourceZoom = 7,
                        MaxSourceZoom = 17,
                        TileUrl = await dataService.GetTileLayerUrl()
                    },
                },
                _ => throw new ArgumentOutOfRangeException(nameof(layerType)),
            };
        }

        private static async Task<HeatMapLayerDef> GetDefaultHeatMapLayerDef(IDataService dataService)
        {
            var layerDef = new HeatMapLayerDef();
            layerDef.DataSource.Url = await dataService.GetHeatMapLayerUrl();
            return layerDef;
        }

        private static async Task<ImageLayerDef> GetDefaultImageLayerDef(IDataService dataService)
        {
            var layerDef = new ImageLayerDef();

            var data = await dataService.GetImageLayerData();
            layerDef.Options = new ImageLayerOptions
            {
                Url = data.Url,
                Coordinates = data.Coordinates,
            };

            return layerDef;
        }

        public static async Task<MapLayerDef> AddBasicMapLayer(this IAzureMapContainer mapInterop, IDataService dataService, MapLayerDef layerDef, bool zoomTo = true)
        {
            return layerDef.LayerType switch
            {
                MapLayerType.Bubble => await AddBubbleLayer(mapInterop, dataService, (BubbleLayerDef)layerDef, zoomTo),
                MapLayerType.HeatMap => await AddHeatMapLayer(mapInterop, (HeatMapLayerDef)layerDef, zoomTo),
                MapLayerType.Image => await AddImageLayer(mapInterop, (ImageLayerDef)layerDef, zoomTo),
                MapLayerType.Line => await AddLineLayer(mapInterop, dataService, (LineLayerDef)layerDef, zoomTo),
                MapLayerType.Polygon => await AddPolygonLayer(mapInterop, dataService, (PolygonLayerDef)layerDef, zoomTo),
                MapLayerType.PolygonExtrusion => await AddPolygonExtLayer(mapInterop, dataService, (PolygonExtLayerDef)layerDef, zoomTo),
                MapLayerType.Symbol => await AddSymbolLayer(mapInterop, dataService, (SymbolLayerDef)layerDef, zoomTo),
                MapLayerType.Tile => await AddTileLayer(mapInterop, dataService, (TileLayerDef)layerDef, zoomTo),
                _ => throw new ArgumentOutOfRangeException(nameof(layerDef.LayerType)),
            };
        }

        private static async Task<MapLayerDef> AddBubbleLayer(IAzureMapContainer mapInterop, IDataService dataService, BubbleLayerDef layerDef, bool zoomTo = true)
        {
            await mapInterop.Layers.CreateLayer(layerDef);

            var data = await dataService.GetBubbleLayerData();
            MapFeatureDef featureDef = new(new MultiPoint(data))
            {
                Properties = new Properties
                    {
                        { "title", "my bubble layer" },
                        { "demo", true },
                    }
            };
            await mapInterop.Layers.AddMapFeature(featureDef, layerDef.DataSource.Id!);

            if (zoomTo)
                await mapInterop.Configuration.ZoomTo(data[0], 11);

            return layerDef;
        }

        private static async Task<MapLayerDef> AddHeatMapLayer(IAzureMapContainer mapInterop, HeatMapLayerDef layerDef, bool zoomTo = true)
        {
            await mapInterop.Layers.CreateLayer(layerDef);

            if (zoomTo)
                await mapInterop.Configuration.ZoomTo(new Position(-122.33, 47.6), 1);

            return layerDef;
        }

        private static async Task<MapLayerDef> AddImageLayer(IAzureMapContainer mapInterop, ImageLayerDef layerDef, bool zoomTo = true)
        {
            await mapInterop.Layers.CreateLayer(layerDef);

            if (zoomTo)
                await mapInterop.Configuration.ZoomTo(new Position(-74.172363, 40.735657), 11);

            return layerDef;
        }

        private static async Task<MapLayerDef> AddLineLayer(IAzureMapContainer mapInterop, IDataService dataService, LineLayerDef layerDef, bool zoomTo)
        {
            await mapInterop.Layers.CreateLayer(layerDef);

            var data = await dataService.GetLineLayerData();
            var feature = new MapFeatureDef(new LineString(data))
            {
                Properties = new Properties
                {
                    { "title", "my line" },
                    { "demo", true },
                }
            };

            await mapInterop.Layers.AddMapFeature(feature, layerDef.DataSource.Id!);

            if (zoomTo)
                await mapInterop.Configuration.ZoomTo(data[0], 10);

            return layerDef;
        }

        private static async Task<MapLayerDef> AddPolygonLayer(IAzureMapContainer mapInterop, IDataService dataService, PolygonLayerDef layerDef, bool zoomTo)
        {
            await mapInterop.Layers.CreateLayer(layerDef);

            var data = await dataService.GetPolygonLayerData();
            var feature = new MapFeatureDef(new Polygon(data))
            {
                Properties = new Properties
                {
                    { "title", "my Polygon layer" },
                    { "demo", true },
                },
                AsShape = true
            };

            await mapInterop.Layers.AddMapFeature(feature, layerDef.DataSource.Id!);

            if (zoomTo)
                await mapInterop.Configuration.ZoomTo(data[0][0], 11);

            return layerDef;
        }

        private static async Task<MapLayerDef> AddPolygonExtLayer(IAzureMapContainer mapInterop, IDataService dataService, PolygonExtLayerDef layerDef, bool zoomTo)
        {
            await mapInterop.Layers.CreateLayer(layerDef);

            var data = await dataService.GetPolygonExtLayerData();
            var feature = new MapFeatureDef(new Polygon(data))
            {
                Properties = new Properties
                {
                    { "title", "my PolygonExt layer" },
                    { "demo", true },
                },
                AsShape = true
            };

            await mapInterop.Layers.AddMapFeature(feature, layerDef.DataSource.Id!);

            if (zoomTo)
                await mapInterop.Configuration.ZoomTo(data[0][0], 11);

            var camera = await mapInterop.Configuration.GetCamera();
            camera.Pitch = 60;
            await mapInterop.Configuration.SetCamera(camera.ToCameraOptions());

            return layerDef;
        }

        private static async Task<MapLayerDef> AddSymbolLayer(IAzureMapContainer mapInterop, IDataService dataService, SymbolLayerDef layerDef, bool zoomTo)
        {
            await mapInterop.Layers.CreateLayer(layerDef);

            var data = await dataService.GetSymbolLayerData();

            foreach (var position in data)
            {
                var feature = new MapFeatureDef(new Point(position))
                {
                    Properties = new Properties
                    {
                        { "title", "my symbol" },
                        { "description", "my symbol description" },
                        { "demo", true },
                    }
                };
                await mapInterop.Layers.AddMapFeature(feature, layerDef.DataSource.Id!);
            }

            if (zoomTo)
                await mapInterop.Configuration.ZoomTo(data[0], 11);

            return layerDef;
        }

        private static async Task<MapLayerDef> AddTileLayer(IAzureMapContainer mapInterop, IDataService dataService, TileLayerDef layerDef, bool zoomTo)
        {
            await mapInterop.Layers.CreateLayer(layerDef);

            if (zoomTo)
                await mapInterop.Configuration.ZoomTo(new Position(-122.426181, 47.608070), 10.75);

            return layerDef;
        }
    }
}
