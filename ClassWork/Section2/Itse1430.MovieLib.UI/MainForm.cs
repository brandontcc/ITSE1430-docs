﻿using System;
using System.Windows.Forms;

namespace Itse1430.MovieLib.UI
{
    public partial class MainForm : Form
    {
        #region Construction

        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        private void MainForm_Load( object sender, EventArgs e )
        {
            _listMovies.DisplayMember = "Name";
            RefreshMovies();
        }

        #region Event Handlers

        private void OnFileExit( object sender, EventArgs e )
        {
            if (MessageBox.Show("Are you sure you want to exit?",
                        "Close", MessageBoxButtons.YesNo) == DialogResult.No)
                return;           

            Close();
        }

        private void OnHelpAbout( object sender, EventArgs e )
        {
            //aboutToolStripMenuItem.
            MessageBox.Show(this, "Sorry", "Help", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void OnMovieAdd( object sender, EventArgs e )
        {
            var form = new MovieForm();
            if (form.ShowDialog(this) == DialogResult.Cancel)
                return;

            //Add to database and refresh
            _database.Add(form.Movie);            
            RefreshMovies();
        }
                
        private void OnMovieDelete( object sender, EventArgs e )
        {
            //Get selected movie, if any
            var item = GetSelectedMovie();
            if (item == null)
                return;

            //Remove from database and refresh
            _database.Remove(item.Name);
            RefreshMovies();
        }

        private void OnMovieEdit( object sender, EventArgs e )
        {
            //Get selected movie, if any
            var item = GetSelectedMovie();
            if (item == null)
                return;

            //Show form with selected movie
            var form = new MovieForm();
            form.Movie = item;
            if (form.ShowDialog(this) == DialogResult.Cancel)
                return;

            //Update database and refresh
            _database.Edit(item.Name, form.Movie);
            RefreshMovies();
        }

        #endregion

        #region Private Members

        private void RefreshMovies ()
        {
            var movies = _database.GetAll();

            _listMovies.Items.Clear();
            _listMovies.Items.AddRange(movies);
        }

        private Movie GetSelectedMovie ()
        {
            return _listMovies.SelectedItem as Movie;
        }

        private MovieDatabase _database = new MovieDatabase();

        #endregion
    }
}
