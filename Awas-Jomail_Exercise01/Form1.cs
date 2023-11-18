using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Awas_Jomail_Exercise01
{
    public partial class Form1 : Form
    {
        public Form1() {
            InitializeComponent();
        }

        private void JoiningTableData_Load(object sender, EventArgs e) {
            // Entity Framework DbContext
            var dbcontext = new BooksDatabase.BooksEntities();

            var titleAuthorCount =
                from title in dbcontext.Titles
                orderby title.Title1
                select new {
                    Title = title.Title1,
                    AuthorCount = title.Authors.Count()
                };

            outputTextBox.AppendText("\r\n\r\n\r\nTitle and Author Count");
            outputTextBox.AppendText($"\r\n\t{"Title",-60} {"Author Count",-15}");

            foreach (var item in titleAuthorCount) {
                outputTextBox.AppendText($"\r\n\t{item.Title,-60} {item.AuthorCount,-15}");
            }

            var titlesGroupedByAuthor = (
                from author in dbcontext.Authors
                orderby author.LastName, author.FirstName
                select new {
                    AuthorName = author.LastName + ", " + author.FirstName,
                    Titles = from title in author.Titles
                             orderby title.Title1
                             select title.Title1
                }
            );

            outputTextBox.AppendText("\r\n\r\n\r\nTitles Grouped by Author");

            foreach (var item in titlesGroupedByAuthor) {
                outputTextBox.AppendText($"\r\n\t{item.AuthorName,-60}"); // Display author name

                string titles = string.Join(Environment.NewLine + new string(' ', 60), item.Titles);
                outputTextBox.AppendText(titles); // Display titles on new lines, further to the right
            }


        }
    }
}
