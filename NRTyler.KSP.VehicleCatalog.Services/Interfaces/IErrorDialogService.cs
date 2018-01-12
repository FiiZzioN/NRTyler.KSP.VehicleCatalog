// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Services
// 
// Author           : Nicholas Tyler
// Created          : 01-05-2018
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-05-2018
// 
// License          : MIT License
// ***********************************************************************

using System.Windows.Forms;

namespace NRTyler.KSP.VehicleCatalog.Services.Interfaces
{
    public interface IErrorDialogService
    {
        /// <summary>
        /// Displays a <see cref="MessageBox" /> using the specified information.
        /// </summary>
        /// <returns> Returns an OK <see cref="DialogResult"/>.</returns>
        DialogResult Show();

        /// <summary>
        /// Displays a <see cref="MessageBox" /> using the specified information.
        /// </summary>
        /// <param name="message">The message the <see cref="MessageBox" /> will contain.</param>
        /// <param name="caption">The <see cref="MessageBox" />'s caption.</param>
        /// <param name="buttons">The buttons the <see cref="MessageBox" /> will use.</param>
        /// <param name="icon">The icon the <see cref="MessageBox" /> will use.</param>
        /// <returns>Returns a <see cref="DialogResult" /> based on the buttons used.</returns>
        DialogResult Show(string message, string caption = "Error Report",
            MessageBoxButtons buttons = MessageBoxButtons.OK, 
            MessageBoxIcon icon       = MessageBoxIcon.Error);
    }
}