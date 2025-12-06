using Engine;

namespace TrashValley;

public static class ModelDatabase
{
    public static class Tools
    {
        public static readonly ToolModel AxeModel = new ToolModel("Axe", ToolType.Axe, AssetManager.GetSpriteSheet("axe_tool"), 2);
    }
}