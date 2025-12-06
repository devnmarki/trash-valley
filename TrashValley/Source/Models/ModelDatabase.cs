using Engine;

namespace TrashValley;

public static class ModelDatabase
{
    public static class Tools
    {
        public static readonly ToolModel AxeModel = new ToolModel("Axe", ToolType.Axe, AssetManager.GetSpriteSheet("axe_tool"), 2);
    }

    public static class Resources
    {
        public static readonly ResourceModel RockModel = new ResourceModel("Rock", new Sprite(AssetManager.GetTexture("rock_resource")!), 5f);
        public static readonly ResourceModel TreeModel = new ResourceModel("Tree", new Sprite(AssetManager.GetTexture("tree_resource")!), 8f);
    }
}