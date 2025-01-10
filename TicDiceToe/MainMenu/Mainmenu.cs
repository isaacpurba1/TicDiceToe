using System;
using System.Media;
using System.Windows.Forms;
using TicDiceToe; // Still using the same namespace  

namespace MainMenu
{
    public partial class Mainmenu : Form
    {

       
        public Mainmenu()
        {
            InitializeComponent();
        }

        

        private void btnplay_Click(object sender, EventArgs e)
        {
            // Use the new class name here  
            TicDiceToeGame ticDiceToeForm = new TicDiceToeGame(); // Updated to new class name  

            PlaySoundEffect(@"D:\Programming\C#\TicDiceToeEdit1\TicDiceToe\Resource\mixkit-interface-device-click-2577.wav");
            // Hide the MainMenu  
            this.Hide();

            // Show the TicDiceToe form  
            ticDiceToeForm.Show();

            // Ensure the MainMenu closes when TicDiceToe is closed  
            ticDiceToeForm.FormClosed += (s, args) => this.Show();
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