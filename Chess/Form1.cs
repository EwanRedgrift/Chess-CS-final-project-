/*
 .----------------.  .----------------.  .----------------.  .----------------.  .----------------. 
| .--------------. || .--------------. || .--------------. || .--------------. || .--------------. |
| |     ______   | || |  ____  ____  | || |  _________   | || |    _______   | || |    _______   | |
| |   .' ___  |  | || | |_   ||   _| | || | |_   ___  |  | || |   /  ___  |  | || |   /  ___  |  | |
| |  / .'   \_|  | || |   | |__| |   | || |   | |_  \_|  | || |  |  (__ \_|  | || |  |  (__ \_|  | |
| |  | |         | || |   |  __  |   | || |   |  _|  _   | || |   '.___`-.   | || |   '.___`-.   | |
| |  \ `.___.'\  | || |  _| |  | |_  | || |  _| |___/ |  | || |  |`\____) |  | || |  |`\____) |  | |
| |   `._____.'  | || | |____||____| | || | |_________|  | || |  |_______.'  | || |  |_______.'  | |
| |              | || |              | || |              | || |              | || |              | |
| '--------------' || '--------------' || '--------------' || '--------------' || '--------------' |
 '----------------'  '----------------'  '----------------'  '----------------'  '----------------' 

Chess
By Ewan Redgrift & Garrett Ackersviller
20/6/2024

*/

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
    public partial class Chess : Form
    {
        int squareSize = 75; // Size of each square on the chessboard
        int boardSize = 8;

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

        Rectangle selectedPeiceIndicator = new Rectangle(0, 0, 75, 75);

        Brush lightBrush = Brushes.LightGray;
        Brush darkBrush = Brushes.Gray;
        Brush greenBrush = Brushes.Green;
        Brush blueBrush = Brushes.Blue;
        Brush redBrush = Brushes.DarkRed;

        Pen redPen = new Pen(Color.DarkRed, 5);

        bool possibleMovesRendered = false;
        int selectedpieceX;
        int selectedpieceY;

        bool gameStart = false;
        bool gameEnd = false;
        bool whiteMove = true;

        SoundPlayer peicePickupSound = new SoundPlayer(Properties.Resources.PeicePickup);
        SoundPlayer peicePutdownSound = new SoundPlayer(Properties.Resources.PeicePutdown);

        public Chess()
        {
            InitializeComponent();

            Refresh();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < boardSize + 1; i++) //Draws board
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

            if (gameStart == true)
            {
                if (whiteMove == true)
                {
                    turnLabel.Text = "Turn = white";
                }
                else
                {
                    turnLabel.Text = "Turn = black";
                }


                if (possibleMovesRendered == true)
                {
                    e.Graphics.DrawRectangle(redPen, selectedPeiceIndicator);

                    for (int i = 0; i < possibleMoves.Count(); i++)
                    {
                        e.Graphics.FillEllipse(blueBrush, possibleMoves[i]);
                    }
                }

                bool whiteKingCaptured = true;
                bool blackKingCaptured = true;

                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        Rectangle renderedPeice = new Rectangle(j * squareSize, i * squareSize, squareSize, squareSize);

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

                        if (boardCoords[i, j] == "WK")
                        {
                            whiteKingCaptured = false;
                        }
                        else if (boardCoords[i, j] == "BK")
                        {
                            blackKingCaptured = false;
                        }
                        //Sees if kings have been captured yet
                    }

                }

                if (whiteKingCaptured == true)
                {
                    EndScreen('W');
                }
                else if (blackKingCaptured == true)
                {
                    EndScreen('B');
                }
            }
        }

        private void EndScreen(char colourKing)
        {
            gameEnd = true;
            gameStart = false;

            if (colourKing == 'W')
            {
                titleLabel.Text = "Black Wins!";
            }
            else
            {
                titleLabel.Text = "White Wins!";
            }

            subtitle.Text = "Press Space to play again. Press ESC to quit.";

            titleLabel.Visible = true;
            subtitle.Visible = true;

            Refresh();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (gameStart == true && gameEnd == false)
            {
                bool moved = false;

                for (int i = 0; i < possibleMoves.Count(); i++)
                {
                    if (e.X > possibleMoves[i].X && e.X < (possibleMoves[i].X + squareSize) && e.Y > possibleMoves[i].Y && e.Y < (possibleMoves[i].Y + squareSize))
                    {
                        char peice = boardCoords[(possibleMoves[i].Y / squareSize), (possibleMoves[i].X / squareSize)][1];
                        boardCoords[(possibleMoves[i].Y / squareSize), (possibleMoves[i].X / squareSize)] = boardCoords[selectedpieceY, selectedpieceX];
                        boardCoords[selectedpieceY, selectedpieceX] = "XX";

                        switch (peice) //Displays captured peices
                        {
                            case 'R':
                                if (whiteMove)
                                {
                                    whiteCollectedPeices.Text += "Rook ";
                                }
                                else
                                {
                                    blackCollectedPeices.Text += "Rook ";
                                }
                                break;
                            case 'B':
                                if (whiteMove)
                                {
                                    whiteCollectedPeices.Text += "Bishop ";
                                }
                                else
                                {
                                    blackCollectedPeices.Text += "Bishop ";
                                }
                                break;
                            case 'H':
                                if (whiteMove)
                                {
                                    whiteCollectedPeices.Text += "Knight ";
                                }
                                else
                                {
                                    blackCollectedPeices.Text += "Knight ";
                                }
                                break;
                            case 'Q':
                                if (whiteMove)
                                {
                                    whiteCollectedPeices.Text += "Queen ";
                                }
                                else
                                {
                                    blackCollectedPeices.Text += "Queen ";
                                }
                                break;
                            case 'K':
                                if (whiteMove)
                                {
                                    whiteCollectedPeices.Text += "King ";
                                }
                                else
                                {
                                    blackCollectedPeices.Text += "King ";
                                }
                                break;
                            case 'P':
                                if (whiteMove)
                                {
                                    whiteCollectedPeices.Text += "Pawn ";
                                }
                                else
                                {
                                    blackCollectedPeices.Text += "Pawn ";
                                }
                                break;
                            default:
                                break;
                        }

                        possibleMoves.Clear();
                        moved = true;
                        whiteMove = !whiteMove;

                        peicePutdownSound.Play();
                    }

                }
                if (moved == false)
                {
                    for (int i = 0; i < boardSize; i++)
                    {
                        for (int j = 0; j < boardSize; j++)
                        {
                            if (e.X > (j * squareSize) && e.X < ((1 + j) * squareSize) && e.Y > (i * squareSize) && e.Y < ((1 + i) * squareSize))
                            {
                                possibleMoves.Clear();

                                selectedPeiceIndicator.X = j * squareSize;
                                selectedPeiceIndicator.Y = i * squareSize;

                                char pieceColor = boardCoords[i, j][0];

                                // Check if it's white's turn and the piece is white
                                if (whiteMove && pieceColor == 'W')
                                {
                                    HandlePieceMoves(i, j, pieceColor);
                                }
                                // Check if it's black's turn and the piece is black
                                else if (!whiteMove && pieceColor == 'B')
                                {
                                    HandlePieceMoves(i, j, pieceColor);
                                }
                            }
                        }
                    }
                    peicePickupSound.Play();
                }
                Refresh();
            }
        }

        private void HandlePieceMoves(int i, int j, int peiceColour)
        {
            switch (boardCoords[i, j][1])
            {
                case 'R':
                    selectedpieceX = j;
                    selectedpieceY = i;

                    //Up
                    for (int columnUp = i; columnUp > -1; columnUp--)
                    {
                        if (boardCoords[columnUp, j] != "XX" && boardCoords[i, j][0] != boardCoords[columnUp, j][0])
                        {
                            possibleMoves.Add(new Rectangle(j * squareSize, columnUp * squareSize, squareSize, squareSize));
                            break;
                        }
                        else if (boardCoords[columnUp, j] == "XX")
                        {
                            possibleMoves.Add(new Rectangle(j * squareSize, columnUp * squareSize, squareSize, squareSize));
                        }
                        else if (boardCoords[columnUp, j][0] == boardCoords[i, j][0] && columnUp != i)
                        {
                            break; // Stop if the line has a friendly piece
                        }
                    }

                    //Down
                    for (int columnDown = i; columnDown < boardSize; columnDown++)
                    {
                        if (boardCoords[columnDown, j] != "XX" && boardCoords[columnDown, j][0] != boardCoords[i, j][0])
                        {
                            possibleMoves.Add(new Rectangle(j * squareSize, columnDown * squareSize, squareSize, squareSize));
                            break;
                        }
                        else if (boardCoords[columnDown, j] == "XX")
                        {
                            possibleMoves.Add(new Rectangle(j * squareSize, columnDown * squareSize, squareSize, squareSize));
                        }
                        else if (boardCoords[columnDown, j][0] == boardCoords[i, j][0] && columnDown != i)
                        {
                            break; // Stop if the line has a friendly piece
                        }
                    }

                    //Left
                    for (int rowLeft = j; rowLeft > -1; rowLeft--)
                    {
                        if (boardCoords[i, rowLeft] != "XX" && boardCoords[i, rowLeft][0] != boardCoords[i, j][0])
                        {
                            possibleMoves.Add(new Rectangle(rowLeft * squareSize, i * squareSize, squareSize, squareSize));
                            break;
                        }
                        else if (boardCoords[i, rowLeft] == "XX")
                        {
                            possibleMoves.Add(new Rectangle(rowLeft * squareSize, i * squareSize, squareSize, squareSize));
                        }
                        else if (boardCoords[i, rowLeft][0] == boardCoords[i, j][0] && rowLeft != j)
                        {
                            break; // Stop if the line has a friendly piece
                        }
                    }

                    //Right
                    for (int rowRight = j; rowRight < boardSize; rowRight++)
                    {
                        if (boardCoords[i, rowRight] != "XX" && boardCoords[i, rowRight][0] != boardCoords[i, j][0])
                        {
                            possibleMoves.Add(new Rectangle(rowRight * squareSize, i * squareSize, squareSize, squareSize));
                            break;
                        }
                        else if (boardCoords[i, rowRight] == "XX")
                        {
                            possibleMoves.Add(new Rectangle(rowRight * squareSize, i * squareSize, squareSize, squareSize));
                        }
                        else if (boardCoords[i, rowRight][0] == boardCoords[i, j][0] && rowRight != j)
                        {
                            break; // Stop if the line has a friendly piece
                        }
                    }
                    break;

                case 'H':
                    selectedpieceX = j;
                    selectedpieceY = i;

                    // Knight moves in an L-shape: 8 possible moves
                    int[,] knightMoves =
                    {
                        { -2, -1 }, { -1, -2 }, { 1, -2 }, { 2, -1 },
                        { 2, 1 }, { 1, 2 }, { -1, 2 }, { -2, 1 }
                    };

                    for (int k = 0; k < 8; k++)
                    {
                        int newX = j + knightMoves[k, 1];
                        int newY = i + knightMoves[k, 0];

                        if (newX >= 0 && newX < boardSize && newY >= 0 && newY < boardSize)
                        {
                            if (boardCoords[newY, newX] == "XX" || boardCoords[newY, newX][0] != boardCoords[i, j][0])
                            {
                                possibleMoves.Add(new Rectangle(newX * squareSize, newY * squareSize, squareSize, squareSize));
                            }
                        }
                    }
                    break;


                case 'B':
                    selectedpieceX = j;
                    selectedpieceY = i;

                    // Up-Left diagonal
                    for (int upLeft = 1; i - upLeft >= 0 && j - upLeft >= 0; upLeft++)
                    {
                        if (boardCoords[i - upLeft, j - upLeft] == "XX")
                        {
                            possibleMoves.Add(new Rectangle((j - upLeft) * squareSize, (i - upLeft) * squareSize, squareSize, squareSize));
                        }
                        else if (boardCoords[i - upLeft, j - upLeft][0] != boardCoords[i, j][0])
                        {
                            possibleMoves.Add(new Rectangle((j - upLeft) * squareSize, (i - upLeft) * squareSize, squareSize, squareSize));
                            break; // Stop if the diagonal has an opponent piece
                        }
                        else
                        {
                            break; // Stop if the diagonal has a friendly piece
                        }
                    }

                    // Up-Right diagonal
                    for (int upRight = 1; i - upRight >= 0 && j + upRight < boardSize; upRight++)
                    {
                        if (boardCoords[i - upRight, j + upRight] == "XX")
                        {
                            possibleMoves.Add(new Rectangle((j + upRight) * squareSize, (i - upRight) * squareSize, squareSize, squareSize));
                        }
                        else if (boardCoords[i - upRight, j + upRight][0] != boardCoords[i, j][0])
                        {
                            possibleMoves.Add(new Rectangle((j + upRight) * squareSize, (i - upRight) * squareSize, squareSize, squareSize));
                            break; // Stop if the diagonal has an opponent piece
                        }
                        else
                        {
                            break; // Stop if the diagonal has a friendly piece
                        }
                    }

                    // Down-Left diagonal
                    for (int downLeft = 1; i + downLeft < boardSize && j - downLeft >= 0; downLeft++)
                    {
                        if (boardCoords[i + downLeft, j - downLeft] == "XX")
                        {
                            possibleMoves.Add(new Rectangle((j - downLeft) * squareSize, (i + downLeft) * squareSize, squareSize, squareSize));
                        }
                        else if (boardCoords[i + downLeft, j - downLeft][0] != boardCoords[i, j][0])
                        {
                            possibleMoves.Add(new Rectangle((j - downLeft) * squareSize, (i + downLeft) * squareSize, squareSize, squareSize));
                            break; // Stop if the diagonal has an opponent piece
                        }
                        else
                        {
                            break; // Stop if the diagonal has a friendly piece
                        }
                    }

                    // Down-Right diagonal
                    for (int downRight = 1; i + downRight < boardSize && j + downRight < boardSize; downRight++)
                    {
                        if (boardCoords[i + downRight, j + downRight] == "XX")
                        {
                            possibleMoves.Add(new Rectangle((j + downRight) * squareSize, (i + downRight) * squareSize, squareSize, squareSize));
                        }
                        else if (boardCoords[i + downRight, j + downRight][0] != boardCoords[i, j][0])
                        {
                            possibleMoves.Add(new Rectangle((j + downRight) * squareSize, (i + downRight) * squareSize, squareSize, squareSize));
                            break; // Stop if the diagonal has an opponent piece
                        }
                        else
                        {
                            break; // Stop if the diagonal has a friendly piece
                        }
                    }
                    break;

                case 'P':
                    selectedpieceX = j;
                    selectedpieceY = i;

                    if (boardCoords[i, j][0] == 'W') // If pawn is white
                    {
                        // Move forward
                        if (i - 1 >= 0 && boardCoords[i - 1, j] == "XX")
                        {
                            possibleMoves.Add(new Rectangle(j * squareSize, (i - 1) * squareSize, squareSize, squareSize));

                            // Move two squares forward from starting position
                            if (i == boardSize - 2 && boardCoords[i - 2, j] == "XX")
                            {
                                possibleMoves.Add(new Rectangle(j * squareSize, (i - 2) * squareSize, squareSize, squareSize));
                            }
                        }

                        // Capture diagonally
                        if (i - 1 >= 0)
                        {
                            if (j - 1 >= 0 && boardCoords[i - 1, j - 1] != "XX" && boardCoords[i - 1, j - 1][0] == 'B')
                            {
                                possibleMoves.Add(new Rectangle((j - 1) * squareSize, (i - 1) * squareSize, squareSize, squareSize));
                            }
                            if (j + 1 < boardSize && boardCoords[i - 1, j + 1] != "XX" && boardCoords[i - 1, j + 1][0] == 'B')
                            {
                                possibleMoves.Add(new Rectangle((j + 1) * squareSize, (i - 1) * squareSize, squareSize, squareSize));
                            }
                        }
                    }
                    else // If pawn is black
                    {
                        // Move forward
                        if (i + 1 < boardSize && boardCoords[i + 1, j] == "XX")
                        {
                            possibleMoves.Add(new Rectangle(j * squareSize, (i + 1) * squareSize, squareSize, squareSize));

                            // Move two squares forward from starting position
                            if (i == 1 && boardCoords[i + 2, j] == "XX")
                            {
                                possibleMoves.Add(new Rectangle(j * squareSize, (i + 2) * squareSize, squareSize, squareSize));
                            }
                        }

                        // Capture diagonally
                        if (i + 1 < boardSize)
                        {
                            if (j - 1 >= 0 && boardCoords[i + 1, j - 1] != "XX" && boardCoords[i + 1, j - 1][0] == 'W')
                            {
                                possibleMoves.Add(new Rectangle((j - 1) * squareSize, (i + 1) * squareSize, squareSize, squareSize));
                            }
                            if (j + 1 < boardSize && boardCoords[i + 1, j + 1] != "XX" && boardCoords[i + 1, j + 1][0] == 'W')
                            {
                                possibleMoves.Add(new Rectangle((j + 1) * squareSize, (i + 1) * squareSize, squareSize, squareSize));
                            }
                        }
                    }
                    break;


                case 'Q':
                    selectedpieceX = j;
                    selectedpieceY = i;

                    // Rook-like moves (vertical and horizontal)
                    // Up
                    for (int columnUp = i - 1; columnUp >= 0; columnUp--)
                    {
                        if (boardCoords[columnUp, j] == "XX")
                        {
                            possibleMoves.Add(new Rectangle(j * squareSize, columnUp * squareSize, squareSize, squareSize));
                        }
                        else if (boardCoords[columnUp, j][0] != boardCoords[i, j][0])
                        {
                            possibleMoves.Add(new Rectangle(j * squareSize, columnUp * squareSize, squareSize, squareSize));
                            break; // Stop if there's an opponent's piece
                        }
                        else
                        {
                            break; // Stop if there's a friendly piece
                        }
                    }

                    // Down
                    for (int columnDown = i + 1; columnDown < boardSize; columnDown++)
                    {
                        if (boardCoords[columnDown, j] == "XX")
                        {
                            possibleMoves.Add(new Rectangle(j * squareSize, columnDown * squareSize, squareSize, squareSize));
                        }
                        else if (boardCoords[columnDown, j][0] != boardCoords[i, j][0])
                        {
                            possibleMoves.Add(new Rectangle(j * squareSize, columnDown * squareSize, squareSize, squareSize));
                            break; // Stop if there's an opponent's piece
                        }
                        else
                        {
                            break; // Stop if there's a friendly piece
                        }
                    }

                    // Left
                    for (int rowLeft = j - 1; rowLeft >= 0; rowLeft--)
                    {
                        if (boardCoords[i, rowLeft] == "XX")
                        {
                            possibleMoves.Add(new Rectangle(rowLeft * squareSize, i * squareSize, squareSize, squareSize));
                        }
                        else if (boardCoords[i, rowLeft][0] != boardCoords[i, j][0])
                        {
                            possibleMoves.Add(new Rectangle(rowLeft * squareSize, i * squareSize, squareSize, squareSize));
                            break; // Stop if there's an opponent's piece
                        }
                        else
                        {
                            break; // Stop if there's a friendly piece
                        }
                    }

                    // Right
                    for (int rowRight = j + 1; rowRight < boardSize; rowRight++)
                    {
                        if (boardCoords[i, rowRight] == "XX")
                        {
                            possibleMoves.Add(new Rectangle(rowRight * squareSize, i * squareSize, squareSize, squareSize));
                        }
                        else if (boardCoords[i, rowRight][0] != boardCoords[i, j][0])
                        {
                            possibleMoves.Add(new Rectangle(rowRight * squareSize, i * squareSize, squareSize, squareSize));
                            break; // Stop if there's an opponent's piece
                        }
                        else
                        {
                            break; // Stop if there's a friendly piece
                        }
                    }

                    // Bishop-like moves (diagonals)
                    // Up-Left diagonal
                    for (int upLeft = 1; i - upLeft >= 0 && j - upLeft >= 0; upLeft++)
                    {
                        if (boardCoords[i - upLeft, j - upLeft] == "XX")
                        {
                            possibleMoves.Add(new Rectangle((j - upLeft) * squareSize, (i - upLeft) * squareSize, squareSize, squareSize));
                        }
                        else if (boardCoords[i - upLeft, j - upLeft][0] != boardCoords[i, j][0])
                        {
                            possibleMoves.Add(new Rectangle((j - upLeft) * squareSize, (i - upLeft) * squareSize, squareSize, squareSize));
                            break; // Stop if there's an opponent's piece
                        }
                        else
                        {
                            break; // Stop if there's a friendly piece
                        }
                    }

                    // Up-Right diagonal
                    for (int upRight = 1; i - upRight >= 0 && j + upRight < boardSize; upRight++)
                    {
                        if (boardCoords[i - upRight, j + upRight] == "XX")
                        {
                            possibleMoves.Add(new Rectangle((j + upRight) * squareSize, (i - upRight) * squareSize, squareSize, squareSize));
                        }
                        else if (boardCoords[i - upRight, j + upRight][0] != boardCoords[i, j][0])
                        {
                            possibleMoves.Add(new Rectangle((j + upRight) * squareSize, (i - upRight) * squareSize, squareSize, squareSize));
                            break; // Stop if there's an opponent's piece
                        }
                        else
                        {
                            break; // Stop if there's a friendly piece
                        }
                    }

                    // Down-Left diagonal
                    for (int downLeft = 1; i + downLeft < boardSize && j - downLeft >= 0; downLeft++)
                    {
                        if (boardCoords[i + downLeft, j - downLeft] == "XX")
                        {
                            possibleMoves.Add(new Rectangle((j - downLeft) * squareSize, (i + downLeft) * squareSize, squareSize, squareSize));
                        }
                        else if (boardCoords[i + downLeft, j - downLeft][0] != boardCoords[i, j][0])
                        {
                            possibleMoves.Add(new Rectangle((j - downLeft) * squareSize, (i + downLeft) * squareSize, squareSize, squareSize));
                            break; // Stop if there's an opponent's piece
                        }
                        else
                        {
                            break; // Stop if there's a friendly piece
                        }
                    }

                    // Down-Right diagonal
                    for (int downRight = 1; i + downRight < boardSize && j + downRight < boardSize; downRight++)
                    {
                        if (boardCoords[i + downRight, j + downRight] == "XX")
                        {
                            possibleMoves.Add(new Rectangle((j + downRight) * squareSize, (i + downRight) * squareSize, squareSize, squareSize));
                        }
                        else if (boardCoords[i + downRight, j + downRight][0] != boardCoords[i, j][0])
                        {
                            possibleMoves.Add(new Rectangle((j + downRight) * squareSize, (i + downRight) * squareSize, squareSize, squareSize));
                            break; // Stop if there's an opponent's piece
                        }
                        else
                        {
                            break; // Stop if there's a friendly piece
                        }
                    }

                    break;

                case 'K':
                    selectedpieceX = j;
                    selectedpieceY = i;

                    // King can move one square in any direction

                    // Horizontal and Vertical moves
                    for (int xOffset = -1; xOffset <= 1; xOffset++)
                    {
                        for (int yOffset = -1; yOffset <= 1; yOffset++)
                        {
                            if (xOffset != 0 || yOffset != 0) // Ensure it's not the current position
                            {
                                int newX = j + xOffset;
                                int newY = i + yOffset;

                                if (newX >= 0 && newX < boardSize && newY >= 0 && newY < boardSize)
                                {
                                    if (boardCoords[newY, newX] == "XX" || boardCoords[newY, newX][0] != boardCoords[i, j][0])
                                    {
                                        possibleMoves.Add(new Rectangle(newX * squareSize, newY * squareSize, squareSize, squareSize));
                                    }
                                }
                            }
                        }
                    }

                    break;

            }
        }

        bool CanMoveWhitePiece(char pieceColor, int i, int j)
        {
            return whiteMove && pieceColor == 'W';
        }

        // Check if it's black's turn and the piece is black
        bool CanMoveBlackPiece(char pieceColor, int i, int j)
        {
            return !whiteMove && pieceColor == 'B';
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
                    if (gameStart == false || gameEnd == true) // start game
                    {
                        titleLabel.Visible = false;
                        subtitle.Visible = false;
                        gameStart = true;
                        gameEnd = false;
                        whiteMove = true;

                        whiteCollectedPeices.Text = null;
                        blackCollectedPeices.Text = null;

                        boardCoords = new string[8, 8]
                        {
                            { "BR", "BH", "BB", "BQ", "BK", "BB", "BH", "BR"},
                            { "BP", "BP", "BP", "BP", "BP", "BP", "BP", "BP"},
                            { "XX", "XX", "XX", "XX", "XX", "XX", "XX", "XX"},
                            { "XX", "XX", "XX", "XX", "XX", "XX", "XX", "XX"},
                            { "XX", "XX", "XX", "XX", "XX", "XX", "XX", "XX"},
                            { "XX", "XX", "XX", "XX", "XX", "XX", "XX", "XX"},
                            { "WP", "WP", "WP", "WP", "WP", "WP", "WP", "WP"},
                            { "WR", "WH", "WB", "WQ", "WK", "WB", "WH", "WR"},
                        };

                        Refresh();
                    }
                    break;
                case Keys.Escape:
                    if (gameEnd == true)
                    {
                        this.Close();
                    }
                    break;
            }
        }
    }
}