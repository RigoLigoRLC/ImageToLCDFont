using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageFonter
{
  class Program
	{
		static void Main(string[] args)
		{
			Bitmap textImage;
			int startH, startV, width, height, sepH, sepV, chrH, chrV;

			Console.WriteLine("Drag in image file and press ENTER: ");
			string imagepath = Console.ReadLine();

			try
			{
				if (imagepath[0] == '"' && imagepath[imagepath.Length - 1] == '"')
					imagepath = imagepath.Substring(1, imagepath.Length - 2);
				textImage = new Bitmap(imagepath);    //Open Image
			}
			catch(Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
				Console.ReadKey();
				return;
			}

			Console.WriteLine("The image width is " + textImage.Width.ToString() + " pixels, height is " + textImage.Height.ToString() + ".");
			Console.Write("Enter the starting horizontal pixel: ");
			while (!int.TryParse(Console.ReadLine(), out startH))
				Console.Write("Invalid integer. Please re-enter: ");
			Console.Write("Enter the starting vertical pixel: ");
			while (!int.TryParse(Console.ReadLine(), out startV))
				Console.Write("Invalid integer. Please re-enter: ");
			//Get starting pixel

			Console.Write("Enter the character width: ");
			while (!int.TryParse(Console.ReadLine(), out width))
				Console.Write("Invalid integer. Please re-enter: ");
			Console.Write("Enter the character height: ");
			while (!int.TryParse(Console.ReadLine(), out height))
				Console.Write("Invalid integer. Please re-enter: ");


			Console.Write("Enter the horizontal separation pixel numbers: ");
			while (!int.TryParse(Console.ReadLine(), out sepH))
				Console.Write("Invalid integer. Please re-enter: ");
			Console.Write("Enter the vertical separation pixel numbers: ");
			while (!int.TryParse(Console.ReadLine(), out sepV))
				Console.Write("Invalid integer. Please re-enter: ");


			Console.Write("Enter the horizontal character numbers: ");
			while (!int.TryParse(Console.ReadLine(), out chrH))
				Console.Write("Invalid integer. Please re-enter: ");
			Console.Write("Enter the vertical character numbers: ");
			while (!int.TryParse(Console.ReadLine(), out chrV))
				Console.Write("Invalid integer. Please re-enter: ");


			//try reading first charater into the console
			Console.WriteLine("Is this the first character you wanted?\nIf it's not, please double-check your parameters, and report a bug if needed.");
			for (int i = 0; i < width + 2; i++)
				Console.Write('-');
			Console.Write('\n');
			for (int j = startV; j < startV + height; j++)
			{
				Console.Write('|');
				for (int i = startH; i < startH + width; i++)
						if (textImage.GetPixel(i, j).GetBrightness() < 0.05)
						Console.Write('X');
					else
						Console.Write(' ');
				Console.Write("|\n");
			}
			for (int i = 0; i < width + 2; i++)
				Console.Write('-');
			Console.Write('\n');


			//De-comment this section if you'd like to see a preview of all characters the program found
			//in the image file.

			/*
			for (int y = 1; y <= chrV; y++)
			{
				for (int x = 1; x <= chrH; x++)
					for (int j = startV + (y - 1) * (height + sepV); j < startV + y * (height + sepV) - sepV; j++)
					{
						for (int i = startH + (x - 1) * (width + sepH); i < startH + x * (width + sepH) - sepH; i++)
							if (textImage.GetPixel(i, j).GetBrightness() < 0.05)
								Console.Write('X');
							else
								Console.Write(' ');
						Console.Write('\n');
					}
				Console.Write("\n\n");
			}*/


			int mode = -1, rtn = 0;

			Console.WriteLine("Enter the output mode: ");
			while (!(mode > 0 && mode < 5))
			{
				if (mode != -1)
					Console.WriteLine("Invalid Mode. Please re-enter.");
				while (!int.TryParse(Console.ReadLine(), out mode))
					Console.Write("Invalid integer. Please re-enter: ");
			}

			try //start conversion
			{

				switch (mode)
				{
					case -1:
						Console.WriteLine("Program received quit command.");
						break;
					case 1:
						Console.WriteLine("Not implemented yet.");
						break;
					case 2:
						Console.WriteLine("Not implemented yet.");
						break;
					case 3:

						for (int y = 1; y <= chrV; y++)
						{
							for (int x = 1; x <= chrH; x++)
							{
								int cycles = (int)Math.Ceiling((double)height / 8.0d);
								int leng = width * cycles;
								int[] charData = new int[leng]; //Initialize a character data array

								for (int h = 0; h < cycles; h++) //vertical segments loop
								{
									int baseH = startH + (x - 1) * (width + sepH);
									for (int i = baseH; i < startH + x * (width + sepH) - sepH; i++) //i provides Xpx
									{
										int currentByte = h * width + i - baseH;
										int baseBit = startV + h * 8 + ((y - 1) * (height + sepV));

										charData[currentByte] = 0; //Initialize the current writing byte

										for (int j = baseBit; j < ((h + 1) * 8 > height ? baseBit + cycles * 8 - height : baseBit + 8 ); j++)
										{
											if (textImage.GetPixel(i, j).GetBrightness() < 0.05)
												charData[currentByte] |= (128 >> (j - baseBit));
										}
									}
								}
								Console.Write("{");
								leng--;
								for (int i = 0; i < leng; i++)
									Console.Write("0x" + (charData[i] <= 15 ? "0" : "") + Convert.ToString(charData[i], 16).ToUpper() + ", ");
								Console.Write("0x" + (charData[leng] <= 15 ? "0" : "") + Convert.ToString(charData[leng], 16).ToUpper() + "}\n");
							}
						}
						rtn = 0;
						break;

					case 4:
						for (int y = 1; y <= chrV; y++)
						{
							for (int x = 1; x <= chrH; x++)
							{
								int cycles = (int)Math.Ceiling((double)height / 8.0d);
								int leng = width * cycles;
								int[] charData = new int[leng]; //Initialize a character data array

								for (int h = 0; h < cycles; h++) //vertical segments loop
								{
									int baseH = startH + (x - 1) * (width + sepH);
									for (int i = baseH; i < startH + x * (width + sepH) - sepH; i++) //i provides Xpx
									{
										int currentByte = h * width + i - baseH;
										int baseBit = startV + h * 8 + ((y - 1) * (height + sepV));

										charData[currentByte] = 0; //Initialize the current writing byte

										for (int j = baseBit; j < ((h + 1) * 8 > height ? baseBit + cycles * 8 - height : baseBit + 8 ); j++)
										{
											if (textImage.GetPixel(i, j).GetBrightness() < 0.05)
												charData[currentByte] |= (1 << (j - baseBit));
										}
									}
								}
								Console.Write("{");
								leng--;
								for (int i = 0; i < leng; i++)
									Console.Write("0x" + (charData[i] <= 15 ? "0": "") + Convert.ToString(charData[i], 16).ToUpper() + ", ");
								Console.Write("0x" + (charData[leng] <= 15 ? "0" : "") + Convert.ToString(charData[leng], 16).ToUpper() + "}\n");
							}
						}
						rtn = 0;
						break;
				}
			}
			catch(Exception e)
			{
				Console.WriteLine("Unexpected Exception: " + e.Message);
				Console.WriteLine("Stack Trace:\n" + e.StackTrace);
				Console.WriteLine("Please contact author for help or open an issue on GitHub page.\nBefore doing that, please check all the parameters you have entered in order to prevent false bug reports.");
			}

			Console.Write("Press any key to exit...");
			Console.ReadKey();
		}
	}
}

