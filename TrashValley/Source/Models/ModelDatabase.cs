using Engine;

namespace TrashValley;

public static class ModelDatabase
{
    public static class Tools
    {
        public static readonly ToolModel AxeModel = new ToolModel()
        {
            Name = "Axe",
            SpriteSheet = AssetManager.GetSpriteSheet("axe_tool"),
            Damage = 2
        };
    }
}