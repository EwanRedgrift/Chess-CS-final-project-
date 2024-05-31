using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Chess
{
    public partial class Form1 : Form
    {
        int squareSize = 75; // Size of each square on the chessboard
        int boardSize = 8;

        String turn = "White";

        List<string> peiceNames;
        List<string> peiceOwndership;
        List<Rectangle> peiceRectangles;
        List<string> peicePoints;

        List<Rectangle> possibleMoves = new List<Rectangle>();

        Brush lightBrush = Brushes.LightGray;
        Brush darkBrush = Brushes.Gray;
        Brush greenBrush = Brushes.Green;
        Brush blueBrush = Brushes.Blue;

        bool possibleMovesRendered = false;
        int selectedpiece;

        public Form1()
        {
            InitializeComponent();
            peiceNames = new List<string>(new string[] { "Rook", "Knight", "Bishop", "Queen", "King", "Bishop", "Knight", "Rook", "Pawn", "Pawn", "Pawn", "Pawn", "Pawn", "Pawn", "Pawn", "Pawn", "Pawn", "Pawn", "Pawn", "Pawn", "Pawn", "Pawn", "Pawn", "Pawn", "Rook", "Knight", "Bishop", "Queen", "King", "Bishop", "Knight", "Rook" });
            peiceOwndership = new List<string>(new string[] { "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "Black", "White", "White", "White", "White", "White", "White", "White", "White", "White", "White", "White", "White", "White", "White", "White", "White" });
            peiceRectangles = new List<Rectangle>(new Rectangle[] { new Rectangle(squareSize * 0, squareSize * 0, squareSize, squareSize), new Rectangle(squareSize * 1, squareSize * 0, squareSize, squareSize), new Rectangle(squareSize * 2, squareSize * 0, squareSize, squareSize), new Rectangle(squareSize * 3, squareSize * 0, squareSize, squareSize), new Rectangle(squareSize * 4, squareSize * 0, squareSize, squareSize), new Rectangle(squareSize * 5, squareSize * 0, squareSize, squareSize), new Rectangle(squareSize * 6, squareSize * 0, squareSize, squareSize), new Rectangle(squareSize * 7, squareSize * 0, squareSize, squareSize), new Rectangle(squareSize * 0, squareSize * 1, squareSize, squareSize), new Rectangle(squareSize * 1, squareSize * 1, squareSize, squareSize), new Rectangle(squareSize * 2, squareSize * 1, squareSize, squareSize), new Rectangle(squareSize * 3, squareSize * 1, squareSize, squareSize), new Rectangle(squareSize * 4, squareSize * 1, squareSize, squareSize), new Rectangle(squareSize * 5, squareSize * 1, squareSize, squareSize), new Rectangle(squareSize * 6, squareSize * 1, squareSize, squareSize), new Rectangle(squareSize * 7, squareSize * 1, squareSize, squareSize), new Rectangle(squareSize * 0, squareSize * 6, squareSize, squareSize), new Rectangle(squareSize * 1, squareSize * 6, squareSize, squareSize), new Rectangle(squareSize * 2, squareSize * 6, squareSize, squareSize), new Rectangle(squareSize * 3, squareSize * 6, squareSize, squareSize), new Rectangle(squareSize * 4, squareSize * 6, squareSize, squareSize), new Rectangle(squareSize * 5, squareSize * 6, squareSize, squareSize), new Rectangle(squareSize * 6, squareSize * 6, squareSize, squareSize), new Rectangle(squareSize * 7, squareSize * 6, squareSize, squareSize), new Rectangle(squareSize * 0, squareSize * 7, squareSize, squareSize), new Rectangle(squareSize * 1, squareSize * 7, squareSize, squareSize), new Rectangle(squareSize * 2, squareSize * 7, squareSize, squareSize), new Rectangle(squareSize * 3, squareSize * 7, squareSize, squareSize), new Rectangle(squareSize * 4, squareSize * 7, squareSize, squareSize), new Rectangle(squareSize * 5, squareSize * 7, squareSize, squareSize), new Rectangle(squareSize * 6, squareSize * 7, squareSize, squareSize), new Rectangle(squareSize * 7, squareSize * 7, squareSize, squareSize) });
            Refresh();
        }
        //square width-peicewidth/2 + x and y
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < boardSize + 1; i++)
            {
                for (int j = 0; j < boardSize + 1; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        e.Graphics.FillRectangle(lightBrush, squareSize * (j - 1), squareSize * (i - 1), squareSize, squareSize);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(darkBrush, squareSize * (j - 1), squareSize * (i - 1), squareSize, squareSize);
                    }
                }
            }

            if (possibleMovesRendered == true)
            {
                for (int i = 0; i < possibleMoves.Count(); i++)
                {
                    e.Graphics.FillEllipse(blueBrush, possibleMoves[i]);
                }
            }

            for (int i = 0; i < peiceRectangles.Count(); i++)
            {
                switch (peiceNames[i])
                {
                    case "Rook":
                        if (peiceOwndership[i] == "White")
                        {
                            e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Rooke_rlt60, peiceRectangles[i]);
                        }
                        else
                        {
                            e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Black_Rooke_rdt60, peiceRectangles[i]);
                        }
                        break;
                    case "Knight":
                        if (peiceOwndership[i] == "White")
                        {
                            e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Knight_nlt60, peiceRectangles[i]);
                        }
                        else
                        {
                            e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Black_Knight_ndt60, peiceRectangles[i]);
                        }
                        break;
                    case "Bishop":
                        if (peiceOwndership[i] == "White")
                        {
                            e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Bishop_blt60, peiceRectangles[i]);
                        }
                        else
                        {
                            e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Black_Bishop_bdt60, peiceRectangles[i]);
                        }
                        break;
                    case "Queen":
                        if (peiceOwndership[i] == "White")
                        {
                            e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Queen_qlt60, peiceRectangles[i]);
                        }
                        else
                        {
                            e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Queen_Black_qdt60, peiceRectangles[i]);
                        }
                        break;
                    case "King":
                        if (peiceOwndership[i] == "White")
                        {
                            e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.King_klt60, peiceRectangles[i]);
                        }
                        else
                        {
                            e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.King_Black_kdt60, peiceRectangles[i]);
                        }
                        break;
                    case "Pawn":
                        if (peiceOwndership[i] == "White")
                        {
                            e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Pawn_plt60, peiceRectangles[i]);
                        }
                        else
                        {
                            e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Black_Pawn_pdt60, peiceRectangles[i]);
                        }
                        break;
                }

            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < peiceRectangles.Count(); i++)
            {
                if (e.X > peiceRectangles[i].X && e.X < (peiceRectangles[i].X + squareSize) && e.Y > peiceRectangles[i].Y && e.Y < (peiceRectangles[i].Y + squareSize))
                {
                    selectedpiece = i;

                    switch (peiceNames[i])
                    {
                        case "Pawn":
                            if (peiceOwndership[i] == "White")
                            {
                                for (int j = 0; j < peiceRectangles.Count(); j++)
                                {
                                    if (peiceRectangles[i].X != peiceRectangles[j].X && peiceRectangles[i].Y - squareSize != peiceRectangles[j].Y)
                                    {
                                        possibleMoves.Add(new Rectangle(peiceRectangles[i].X, peiceRectangles[i].Y - squareSize, squareSize, squareSize));
                                    }
                                    else if (peiceRectangles[i].X - squareSize == peiceRectangles[j].X && peiceRectangles[i].Y - squareSize == peiceRectangles[j].Y)
                                    {
                                        possibleMoves.Add(new Rectangle(peiceRectangles[i].X - squareSize, peiceRectangles[i].Y - squareSize, squareSize, squareSize));
                                    }
                                    else if (peiceRectangles[i].X + squareSize == peiceRectangles[j].X && peiceRectangles[i].Y - squareSize == peiceRectangles[j].Y)
                                    {
                                        possibleMoves.Add(new Rectangle(peiceRectangles[i].X + squareSize, peiceRectangles[i].Y - squareSize, squareSize, squareSize));
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < peiceRectangles.Count(); j++)
                                {
                                    if (peiceRectangles[i].X != peiceRectangles[j].X && peiceRectangles[i].Y + squareSize != peiceRectangles[j].Y)
                                    {
                                        possibleMoves.Add(new Rectangle(peiceRectangles[i].X, peiceRectangles[i].Y + squareSize, squareSize, squareSize));
                                    }
                                    else if (peiceRectangles[i].X - squareSize == peiceRectangles[j].X && peiceRectangles[i].Y + squareSize == peiceRectangles[j].Y)
                                    {
                                        possibleMoves.Add(new Rectangle(peiceRectangles[i].X - squareSize, peiceRectangles[i].Y + squareSize, squareSize, squareSize));
                                    }
                                    else if (peiceRectangles[i].X + squareSize == peiceRectangles[j].X && peiceRectangles[i].Y + squareSize == peiceRectangles[j].Y)
                                    {
                                        possibleMoves.Add(new Rectangle(peiceRectangles[i].X + squareSize, peiceRectangles[i].Y + squareSize, squareSize, squareSize));
                                    }
                                }
                            }
                            break;
                        
                        case "Rook":
                            break;
                    }
                }
                
            }
            for (int i = 0; i < possibleMoves.Count(); i++)
            {
                if (e.X > possibleMoves[i].X && e.X < (possibleMoves[i].X + squareSize) && e.Y > possibleMoves[i].Y && e.Y < (possibleMoves[i].Y + squareSize))
                {
                    for (int j = 0; j < peiceRectangles.Count(); j++)
                    {
                        if (peiceRectangles[j].X == possibleMoves[i].X && peiceRectangles[j].Y == possibleMoves[i].Y)
                        {
                            peiceRectangles[selectedpiece] = new Rectangle(possibleMoves[i].X, possibleMoves[i].Y, squareSize, squareSize);
                            /*peiceNames.RemoveAt(j);
                            peiceOwndership.RemoveAt(j);
                            peiceRectangles.RemoveAt(j); */
                            //peiceRectangles[j] = new Rectangle(0, 0, 0, 0);
                        }                           
                        peiceRectangles[selectedpiece] = new Rectangle(possibleMoves[i].X, possibleMoves[i].Y, squareSize, squareSize);
                    }
                    possibleMoves.Clear();
                }
            }
            
            Refresh();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            possibleMovesRendered = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    break;
                case Keys.Escape:
                    break;
            }
        }
    }
}
