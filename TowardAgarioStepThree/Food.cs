using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace TowardTowardAgarioStepThree;
public class Food
{
    public float X { get; set; }
    public float Y { get; set; }
    public int ARGBColor { get; set; }
    public float Mass { get; set; }
    [JsonConstructor]
    public Food()
    {
        this.X = X;
        this.Y = Y;
        this.ARGBColor = ARGBColor;
        this.Mass = Mass;
    }
}