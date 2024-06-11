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

        List<Rectangle> possibleMoves = new List<Rectangle>();

        string[,] boardCoords =
        {
            {"BR", "BH", "BB", "BQ", "BK", "BB", "BH", "BR"},
            {"BP", "BP", "BP", "BP", "BP", "BP", "BP", "BP"},
            {"XX", "XX", "XX", "XX", "XX", "XX", "XX", "XX"},
            {"XX", "XX", "XX", "XX", "XX", "XX", "XX", "XX"},
            {"XX", "XX", "XX", "XX", "XX", "XX", "XX", "XX"},
            {"XX", "XX", "XX", "XX", "XX", "XX", "XX", "XX"},
            {"WP", "WP", "WP", "WP", "WP", "WP", "WP", "WP"},
            {"WR", "WH", "WB", "WQ", "WK", "WB", "WH", "WR"},
        };

        Brush lightBrush = Brushes.LightGray;
        Brush darkBrush = Brushes.Gray;
        Brush greenBrush = Brushes.Green;
        Brush blueBrush = Brushes.Blue;

        bool possibleMovesRendered = false;
        int selectedpieceX;
        int selectedpieceY;

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




            /*for (int i = 0; i < peiceRectangles.Count(); i++)
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

            }*/


            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    Rectangle renderedPeice = new Rectangle(j * squareSize,  i * squareSize, squareSize, squareSize);

                    switch (boardCoords[i, j][1])
                    {
                        case 'R':
                            if (boardCoords[i, j][0] == 'W')
                            {
                                e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Rooke_rlt60, renderedPeice);
                            }
                            else
                            {
                                e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Black_Rooke_rdt60, renderedPeice);
                            }
                            break;
                        case 'H':
                            if (boardCoords[i, j][0] == 'W')
                            {
                                e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Knight_nlt60, renderedPeice);
                            }
                            else
                            {
                                e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Black_Knight_ndt60, renderedPeice);
                            }
                            break;
                        case 'B':
                            if (boardCoords[i, j][0] == 'W')
                            {
                                e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Bishop_blt60, renderedPeice);
                            }
                            else
                            {
                                e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Black_Bishop_bdt60, renderedPeice);
                            }
                            break;
                        case 'Q':
                            if (boardCoords[i, j][0] == 'W')
                            {
                                e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Queen_qlt60, renderedPeice);
                            }
                            else
                            {
                                e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Queen_Black_qdt60, renderedPeice);
                            }
                            break;
                        case 'K':
                            if (boardCoords[i, j][0] == 'W')
                            {
                                e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.King_klt60, renderedPeice);
                            }
                            else
                            {
                                e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.King_Black_kdt60, renderedPeice);
                            }
                            break;
                        case 'P':
                            if (boardCoords[i, j][0] == 'W')
                            {
                                e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Pawn_plt60, renderedPeice);
                            }
                            else
                            {
                                e.Graphics.DrawImageUnscaledAndClipped(Properties.Resources.Black_Pawn_pdt60, renderedPeice);
                            }
                            break;
                        default:

                            break;
                    }
                }
            }

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            bool moved = false;

            for (int i = 0; i < possibleMoves.Count(); i++)
            {
                if (e.X > possibleMoves[i].X && e.X < (possibleMoves[i].X + squareSize) && e.Y > possibleMoves[i].Y && e.Y < (possibleMoves[i].Y + squareSize))
                {
                    boardCoords[(possibleMoves[i].X / squareSize), (possibleMoves[i].Y / squareSize)] = boardCoords[selectedpieceX, selectedpieceY];
                    boardCoords[selectedpieceX, selectedpieceY] = "XX";

                    possibleMoves.Clear();
                }
            
            }
            if (moved == false)
            {
                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        if (e.X > (i * squareSize) && e.X < ((1 + i) * squareSize) && e.Y > (j * squareSize) && e.Y < ((1 + j) * squareSize))
                        {
                            switch (boardCoords[i, j][1])
                            {
                                case 'P':
                                    if (boardCoords[i, j][0] == 'W')
                                    {
                                        if (boardCoords[i, j - 1] == "XX")
                                        {
                                            possibleMoves.Add(new Rectangle(i * squareSize, (j - 1) * squareSize, squareSize, squareSize));
                                            selectedpieceX = i;
                                            selectedpieceY = j;
                                        }
                                        else if (boardCoords[i + 1, j - 1][0] == 'B')
                                        {
                                            possibleMoves.Add(new Rectangle((i + 1) * squareSize, (j - 1) * squareSize, squareSize, squareSize));
                                            selectedpieceX = i;
                                            selectedpieceY = j;
                                        }
                                        else if (boardCoords[i - 1, j - 1][0] == 'B')
                                        {
                                            possibleMoves.Add(new Rectangle((i - 1) * squareSize, (j - 1) * squareSize, squareSize, squareSize));
                                            selectedpieceX = i;
                                            selectedpieceY = j;
                                        }
                                    }
                                    else
                                    {
                                        
                                    }
                                    break;
                            }
                        }
                    }
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

                    /*if (p2.IntersectsWith(ballList[i]))
                    {
                        ballList.RemoveAt(i);
                    }*/
            }
        }
    }
}
