﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EffixReportSystem.Helper.Classes;
using EffixReportSystem.Views.Publication.ViewModels;
using Telerik.Windows.Controls;

namespace EffixReportSystem.Views.Publication.Views
{
    /// <summary>
    /// Interaction logic for EditPublicationView.xaml
    /// </summary>
    public partial class EditPublicationView : UserControl
    {
        public EditPublicationView()
        {
            InitializeComponent();
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as EditPublicationViewModel;
            ctx.SaveCurrentPublication();
            (ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel = (ctx.ParentViewModel as PublicationViewModel).PageViewModels[0];
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var ctx = DataContext as EditPublicationViewModel;
            ctx.RestoreDefaultVlues();
            (ctx.ParentViewModel as PublicationViewModel).CurrentPageViewModel = (ctx.ParentViewModel as PublicationViewModel).PageViewModels[0];
        }

        private void Image_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var ctx = DataContext as EditPublicationViewModel;
            var parent = DataHelper.FindAncestor(sender as Image,typeof(RadTileViewItem));
            var parentCtx = (parent as RadTileViewItem).DataContext as DataHelper.ImageTile;
            ctx.OpenInPaint(parentCtx);
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var tile = DataHelper.FindAncestor(sender as Image, typeof(RadTileViewItem));
            (tile as RadTileViewItem).TileState=TileViewItemState.Maximized;
        }
    }
}
