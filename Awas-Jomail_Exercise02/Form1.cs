using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Awas_Jomail_Exercise02
{
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        // Entity Framework DbContext
        private BaseballDatabase.BaseballEntities dbcontext = new BaseballDatabase.BaseballEntities();

        private void BaseBallPlayers_Load(object sender, EventArgs e) {
            //load players into memory
            dbcontext.Players.Load();
            //populate the data binding with the players ordered by player id
            playerBindingSource.DataSource = dbcontext.Players.Local.OrderBy(player => player.PlayerID);

            //populating player with highest batting avg
            var highestBattingAvgPlayer = dbcontext.Players.Local.OrderByDescending(player => player.BattingAverage).FirstOrDefault();
            tbHiestBattingAvg.Text = "Player with the highest batting average is:" + Environment.NewLine +
                "Player Name: " + highestBattingAvgPlayer.FirstName + " " + highestBattingAvgPlayer.LastName + Environment.NewLine +
                "Batting Average: " + highestBattingAvgPlayer.BattingAverage;
            //populating all player average
            decimal averageBattingAvg = dbcontext.Players.Local.Average(player => player.BattingAverage);
            tbTotalAvg.Text = "Batting average of all the players:" + Environment.NewLine +
            "Batting average: " + averageBattingAvg.ToString("0.000"); // Display the average with three decimal places
        }
        private void btnSearchLastName_Click(object sender, EventArgs e) {
            try {
                string searchInput = tbSearchLastName.Text;
                playerBindingSource.DataSource = dbcontext.Players.Local.First(player => player.LastName.ToUpper() == searchInput.ToUpper());

            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnReset_Click(object sender, EventArgs e) {
            playerBindingSource.DataSource = dbcontext.Players.Local.OrderBy(player => player.PlayerID);
        }

        private void btnSearchPlayerID_Click(object sender, EventArgs e) {
            try {
                try {
                    int searchInput = Convert.ToInt32(tbSearchID.Text);
                    playerBindingSource.DataSource = dbcontext.Players.Local.First(player => player.PlayerID == searchInput);

                }
                catch (Exception ex) {
                    MessageBox.Show(ex.ToString());
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_Avg_Click(object sender, EventArgs e) {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("List of all players and batting average");
            sb.AppendLine();

            foreach (var player in dbcontext.Players.Local) {
                sb.AppendLine("Player Name: " + player.FirstName + " " + player.LastName);
                sb.AppendLine("Batting Avg: " + player.BattingAverage.ToString("0.000")); // Display batting average with three decimal places
                sb.AppendLine(); // Add an empty line between players
            }


            tbListPattingAvg.Text = sb.ToString();

        }
    }
}
