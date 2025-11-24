using Marqdouj.DotNet.AzureMaps.Map.Interop.Layers;
using Marqdouj.DotNet.AzureMaps.UI.Services;

namespace Marqdouj.DotNet.AzureMaps.UI.Models.Maps
{
    public static class LayerExtensions
    {
        public static MapLayerDef GetClone(this MapLayerDef layerDef)
        {
            return layerDef.Type switch
            {
                MapLayerType.Bubble => (MapLayerDef)((BubbleLayerDef)layerDef).Clone(),
                MapLayerType.HeatMap => (MapLayerDef)((HeatMapLayerDef)layerDef).Clone(),
                MapLayerType.Image => (MapLayerDef)((ImageLayerDef)layerDef).Clone(),
                MapLayerType.Line => (MapLayerDef)((LineLayerDef)layerDef).Clone(),
                MapLayerType.Polygon => (MapLayerDef)((PolygonLayerDef)layerDef).Clone(),
                MapLayerType.PolygonExtrusion => (MapLayerDef)((PolygonExtLayerDef)layerDef).Clone(),
                MapLayerType.Symbol => (MapLayerDef)((SymbolLayerDef)layerDef).Clone(),
                MapLayerType.Tile => (MapLayerDef)((TileLayerDef)layerDef).Clone(),
                _ => throw layerDef.Type.LayerNotSupported(),
            };
        }

        public static MapLayerDef GetLayerDef(this MapLayerType layerType)
        {
            return layerType switch
            {
                MapLayerType.Bubble => new BubbleLayerDef(),
                MapLayerType.HeatMap => new HeatMapLayerDef(),
                MapLayerType.Image => new ImageLayerDef(),
                MapLayerType.Line => new LineLayerDef(),
                MapLayerType.Polygon => new PolygonLayerDef(),
                MapLayerType.PolygonExtrusion => new PolygonExtLayerDef(),
                MapLayerType.Symbol => new SymbolLayerDef(),
                MapLayerType.Tile => new TileLayerDef(),
                _ => throw layerType.LayerNotSupported(),
            };
        }

        public static ILayerDefUIModel GetLayerDefUIModel(this MapLayerType layerType, IAzureMapsXmlService xmlService)
        {
            var layerDef = layerType.GetLayerDef();
            return layerDef.GetLayerDefUIModel(xmlService);
        }

        public static ILayerDefUIModel GetLayerDefUIModel(this MapLayerDef layerDef, IAzureMapsXmlService xmlService)
        {
            return layerDef.Type switch
            {
                MapLayerType.Bubble => new BubbleLayerUIModel(xmlService) { Source = (BubbleLayerDef)layerDef },
                MapLayerType.HeatMap => new HeatMapLayerUIModel(xmlService) { Source = (HeatMapLayerDef)layerDef },
                MapLayerType.Image => new ImageLayerUIModel(xmlService) { Source = (ImageLayerDef)layerDef },
                MapLayerType.Line => new LineLayerUIModel(xmlService) { Source = (LineLayerDef)layerDef },
                MapLayerType.Polygon => new PolygonLayerUIModel(xmlService) { Source = (PolygonLayerDef)layerDef },
                MapLayerType.PolygonExtrusion => new PolygonExtLayerUIModel(xmlService) { Source = (PolygonExtLayerDef)layerDef },
                MapLayerType.Symbol => new SymbolLayerUIModel(xmlService) { Source = (SymbolLayerDef)layerDef },
                MapLayerType.Tile => new TileLayerUIModel(xmlService) { Source = (TileLayerDef)layerDef },
                _ => throw layerDef.Type.LayerNotSupported(),
            };
        }

        private static NotSupportedException LayerNotSupported(this MapLayerType layerType) => new($"Layer type '{layerType}' is not supported.");
    }
}
