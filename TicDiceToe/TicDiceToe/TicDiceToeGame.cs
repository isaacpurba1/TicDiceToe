using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media; // For sound effects  
using System.Windows.Forms;

using NAudio.Wave; // Import NAudio for MP3 playback  


namespace TicDiceToe
{
    public partial class TicDiceToeGame : Form
    {
        private int currentPlayer; // 1 for Player X, 2 for Player O  
        private HashSet<int> rolledNumbers; // Track rolled numbers  
        private Random random = new Random(); // For random number generation  
        private int currentSpinNumber = 0; // Store the current spin number  
        private bool isSpinning = false; // Flag for active spinning  
        private int scorePlayer1 = 0; // Score for Player 1  
        private int scorePlayer2 = 0; // Score for Player 2  
        private IWavePlayer musicPlayer; // For background music using NAudio  
        private AudioFileReader audioFileReader; // To read the MP3 file  
        private bool isMusicPlaying = false; // Flag for music state  
        private SoundPlayer spinSound; // For spin sound effect  

        public TicDiceToeGame()
        {
            InitializeComponent();
            InitializeGame();
            spinSound = new SoundPlayer(@"D:\Programming\C#\TicDiceToeEdit1\TicDiceToe\Resource\spin.wav"); // Specify your spin sound path  
        }

        private void InitializeGame()
        {
            currentPlayer = 1; // Start with Player 1  
            rolledNumbers = new HashSet<int>();
            ResetBoard();
            UpdatePlayerTurnLabel();
            UpdateScoreLabels(); // Update score labels at the start of the game  
        }

        private void ResetBoard()
        {
            for (int i = 1; i <= 9; i++)
            {
                Label label = this.Controls.Find($"label{i}", true)[0] as Label;
                if (label != null)
                {
                    label.Text = "";
                    label.BackColor = Color.CornflowerBlue;
                }
            }

            lblResultSpin.Text = "0"; // Reset the spin result display  
            currentSpinNumber = 0; // Reset spin number  
            rolledNumbers.Clear(); // Reset rolled numbers  
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            // Simulate spinning by changing the number rapidly  
            currentSpinNumber = GetNextSpinNumber(); // Get a valid number that hasn't been rolled yet  
            lblResultSpin.Text = currentSpinNumber.ToString();
        }

        private int GetNextSpinNumber()
        {
            // Generate a number between 1 and 9 that hasn't been rolled yet  
            int number;
            do
            {
                number = random.Next(1, 10); // Generate a number between 1 and 9  
            } while (rolledNumbers.Contains(number)); // Repeat until we find a number that hasn't been rolled  

            return number;
        }

        private void BtnSpin_Click(object sender, EventArgs e)
        {
            if (!isSpinning)
            {
                // Start spinning  
                Timer1.Enabled = true;
                isSpinning = true;
                BtnSpin.Text = "Stop";
                PlaySpinSound(); // Play spin sound when starting to spin  
            }
            else
            {
                // Stop spinning  
                Timer1.Enabled = false;
                isSpinning = false;
                BtnSpin.Text = "Spin";

                // Stop the spin sound if it's playing  
                spinSound.Stop();

                // Find the corresponding label  
                Label label = this.Controls.Find($"label{currentSpinNumber}", true)[0] as Label;

                if (label != null)
                {
                    // Check if the label is empty  
                    if (string.IsNullOrEmpty(label.Text))
                    {
                        // Fill the label with X or O based on current player  
                        label.Text = currentPlayer == 1 ? "X" : "O";
                        label.BackColor = currentPlayer == 1 ? Color.LightGreen : Color.LightCoral;

                        // Mark the number as rolled  
                        rolledNumbers.Add(currentSpinNumber);

                        // Check for winner  
                        if (CheckWinner())
                        {
                            MessageBox.Show($"Player {(currentPlayer == 1 ? "X" : "O")} wins!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Update score  
                            if (currentPlayer == 1)
                            {
                                scorePlayer1++;
                            }
                            else
                            {
                                scorePlayer2++;
                            }
                            UpdateScoreLabels(); // Update the score labels  

                            InitializeGame();
                            return;
                        }

                        // Check for draw  
                        if (IsBoardFull())
                        {
                            MessageBox.Show("It's a draw!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            InitializeGame();
                            return;
                        }
                    }
                }

                // Switch players regardless of whether the move was valid  
                currentPlayer = currentPlayer == 1 ? 2 : 1;
                UpdatePlayerTurnLabel();
            }
        }

        private void PlaySpinSound()
        {
            // Stop any currently playing sound and play the spin sound effect  
            spinSound.Stop(); // Stop the sound if it was already playing  
            spinSound.Play(); // Play the sound effect  
        }

        private void UpdatePlayerTurnLabel()
        {
            labelPlayerTurn.Text = $"Player {(currentPlayer == 1 ? "1" : "2")} Turn";
        }

        private void UpdateScoreLabels()
        {
            lblscoreplayer1.Text = $"{scorePlayer1}";
            lblscoreplayer2.Text = $"{scorePlayer2}";
        }

        private bool CheckWinner()
        {
            // Winning combinations  
            int[][] winningCombos = new int[][]
            {
                new int[] { 1, 2, 3 },
                new int[] { 4, 5, 6 },
                new int[] { 7, 8, 9 },
                new int[] { 1, 4, 7 },
                new int[] { 2, 5, 8 },
                new int[] { 3, 6, 9 },
                new int[] { 1, 5, 9 },
                new int[] { 3, 5, 7 }
            };

            foreach (var combo in winningCombos)
            {
                Label label1 = this.Controls.Find($"label{combo[0]}", true)[0] as Label;
                Label label2 = this.Controls.Find($"label{combo[1]}", true)[0] as Label;
                Label label3 = this.Controls.Find($"label{combo[2]}", true)[0] as Label;

                if (!string.IsNullOrEmpty(label1.Text) &&
                    label1.Text == label2.Text &&
                    label1.Text == label3.Text)
                {
                    return true; // A winner is found  
                }
            }
            return false; // No winner found  
        }

        private bool IsBoardFull()
        {
            for (int i = 1; i <= 9; i++)
            {
                Label label = this.Controls.Find($"label{i}", true)[0] as Label;
                if (string.IsNullOrEmpty(label.Text))
                {
                    return false; // There are still empty squares  
                }
            }
            return true; // The board is full  
        }

        private void BtnNewGame_Click(object sender, EventArgs e)
        {
            PlaySoundEffect(@"D:\Programming\C#\TicDiceToeEdit1\TicDiceToe\Resource\mixkit-interface-device-click-2577.wav"); // Add your new game sound path  
            // Reset scores and start a new game  
            scorePlayer1 = 0;
            scorePlayer2 = 0;
            InitializeGame();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            // Play the exit sound effect  
            PlaySoundEffect(@"D:\Programming\C#\TicDiceToeEdit1\TicDiceToe\Resource\mixkit-interface-device-click-2577.wav");

            // Ask for confirmation before exiting  
            var result = MessageBox.Show("Are you sure you want to Close this game?",
                                          "Exit Game",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Hide the current TicDiceToe form  
                this.Close();

            }
        }



        private void BtnMusic_Click(object sender, EventArgs e)
        {
            // Toggle music playback  
            if (!isMusicPlaying)
            {
                PlayMusic(@"D:\Programming\C#\TicDiceToeEdit1\TicDiceToe\Resource\backsound.mp3"); // Specify your MP3 path  
                BtnMusic.Text = "StopMusic"; // Change button text  
            }
            else
            {
                StopMusic();
                BtnMusic.Text = "PlayMusic"; // Change button text back  
            }
        }

        private void PlayMusic(string filePath)
        {
            try
            {
                if (musicPlayer != null)
                {
                    musicPlayer.Stop();
                    musicPlayer.Dispose();
                }

                musicPlayer = new WaveOutEvent();
                audioFileReader = new AudioFileReader(filePath);
                musicPlayer.Init(audioFileReader);
                musicPlayer.Play();
                isMusicPlaying = true;

                musicPlayer.PlaybackStopped += (s, e) =>
                {
                    StopMusic(); // Stop music when playback ends  
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing music: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StopMusic()
        {
            if (musicPlayer != null)
            {
                musicPlayer.Stop();
                musicPlayer.Dispose();
                musicPlayer = null;

                // Dispose audio file reader only if it was initialized  
                if (audioFileReader != null)
                {
                    audioFileReader.Dispose();
                    audioFileReader = null;
                }

                isMusicPlaying = false;
            }
        }

        private void PlaySoundEffect(string soundFilePath)
        {
            try
            {
                using (SoundPlayer soundEffect = new SoundPlayer(soundFilePath))
                {
                    soundEffect.Play(); // Play the sound effect  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing sound effect: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}