using Newtonsoft.Json;

namespace Engine;

public struct AnimationData
{
    [JsonProperty("frames")]
    public List<int> Frames { get; set; }
    
    [JsonProperty("frame_duration")]
    public float FrameDuration { get; set; }
    
    [JsonProperty("loop")]
    public bool Loop { get; set; }
    
    [JsonProperty("flip")]
    public bool Flip { get; set; }
}

public struct AnimationConfig
{
    [JsonProperty("sprite_sheet_id")]
    public string SpriteSheetId { get; set; }
    
    [JsonProperty("animations")]
    public Dictionary<string, AnimationData> Animations { get; set; }
}