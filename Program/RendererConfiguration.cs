namespace Program
{
    public class RendererConfiguration
    {
        public bool Antialiasing { get; set; }
        public int AASamples { get; set; } = 1;
        public int Width { get; set; }
        public int Height { get; set; }
    }
}