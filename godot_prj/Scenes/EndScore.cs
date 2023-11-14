using Godot;
using System;
using System.Collections.Generic;

public partial class EndScore : Node2D
{
    const int MAXLOG10 = 6;
    TextureRect[] digits;

    Dictionary<int, Vector4> digitRegions = new Dictionary<int, Vector4>();



    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        digits = new TextureRect[MAXLOG10 + 1]
        {
            GetNode<TextureRect>("Digit0"),
            GetNode<TextureRect>("Digit1"),
            GetNode<TextureRect>("Digit2"),
            GetNode<TextureRect>("Digit3"),
            GetNode<TextureRect>("Digit4"),
            GetNode<TextureRect>("Digit5"),
            GetNode<TextureRect>("Digit6"),
        };

        digitRegions.Add(1, new Vector4(6, 11, 30, 54));
        digitRegions.Add(2, new Vector4(44, 12, 34, 52));
        digitRegions.Add(3, new Vector4(92, 13, 27, 48));
        digitRegions.Add(4, new Vector4(125, 12, 34, 47));
        digitRegions.Add(5, new Vector4(160, 14, 32, 46));
        digitRegions.Add(6, new Vector4(204, 11, 27, 47));
        digitRegions.Add(7, new Vector4(240, 13, 26, 48));
        digitRegions.Add(8, new Vector4(269, 10, 38, 51));
        digitRegions.Add(9, new Vector4(315, 12, 27, 47));
        digitRegions.Add(0, new Vector4(345, 12, 41, 52));
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

    public void updateCounter(int score)
    {
        
        // Determine Number of Digits That Should Be Displayed (Remove Leading Zeros) (Leave Last Zero)
        int maxDigit = score == 0 ? 0 : (int)Math.Floor(Math.Log10((double)score));

        maxDigit = maxDigit < MAXLOG10 ? maxDigit : MAXLOG10;

        for (int i = 0; i < digits.Length; ++i)
        {
            TextureRect currentDigit = digits[i];

            // Set Visible Digits
            bool setVisible = i <= maxDigit;

            currentDigit.Visible = setVisible;

            // Set Digit Values
            if (setVisible)
            {
                int digitName = (score / (int)Math.Pow(10, maxDigit - i)) % 10;

                Vector4 region = digitRegions[digitName];

                AtlasTexture atlas = (AtlasTexture)currentDigit.Texture;

                atlas.Region = new Rect2(region[0], region[1], region[2], region[3]);

            }
        }
    }
}
