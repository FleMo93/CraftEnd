namespace CraftEnd.CoreGame.Content
{
  public static class Content
  {
    public static string GameDirectory { get; } = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
    public static string FilePathTiled0x72DungenTileset { get; } = System.IO.Path.Combine(GameDirectory, "Content", "Tiled", "0x72_DungeonTilesetII_v1.3.tsx");
    public static string FilePath0x72DungeonTilesetSpriteSheetList { get; } = System.IO.Path.Combine(GameDirectory, "Content", "0x72_DungeonTilesetII_v1.3.1", "tiles_list_v1.3");
    public static string FilePathTiledLevelDev { get; } = System.IO.Path.Combine(GameDirectory, "Content", "Level", "Dev.tmx");
    public static string Texture2D0x72DungeonTilesetSpriteSheet { get; } = "0x72_DungeonTilesetII_v1.3.1/0x72_DungeonTilesetII_v1.3";
  }
}