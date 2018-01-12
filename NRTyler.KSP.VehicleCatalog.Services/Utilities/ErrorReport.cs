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

using NRTyler.KSP.VehicleCatalog.Services.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace NRTyler.KSP.VehicleCatalog.Services.Utilities
{
    public class ErrorReport : IErrorDialogService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorReport"/> class.
        /// </summary>
        /// <param name="display">Indicates whether the <see cref="MessageBox"/> will appear or not.</param>
        public ErrorReport(bool display)
        {
            Display = display;
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="MessageBox"/> will appear or not.
        /// </summary>
        private bool Display { get; }

        /// <summary>
        /// Gets or sets the message that the message box will contain.
        /// </summary>
        private string Message { get; set; } = "Something went wrong.";

        /// <summary>
        /// Gets or sets the message box's caption.
        /// </summary>
        private string Caption { get; set; } = "Error Report";

        /// <summary>
        /// Gets or sets the message box's buttons.
        /// </summary>
        private MessageBoxButtons Buttons { get; set; } = MessageBoxButtons.OK;

        /// <summary>
        /// Gets or sets the message box's icon.
        /// </summary>
        private MessageBoxIcon Icon { get; set; } = MessageBoxIcon.Error;

        /// <summary>
        /// Displays a <see cref="MessageBox"/> containing this classes default 
        /// values for the message, caption, buttons, and icon properties.
        /// </summary>
        /// <returns> Returns an OK <see cref="DialogResult"/>.</returns>
        public DialogResult Show()
        {
            if (!Display) return DialogResult.OK;

            return MessageBox.Show(Message, Caption, Buttons, Icon);
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> using the specified information.
        /// </summary>
        /// <param name="message">The message the <see cref="MessageBox"/> will contain.</param>
        /// <param name="caption">The <see cref="MessageBox"/>'s caption.</param>
        /// <param name="buttons">The buttons the <see cref="MessageBox"/> will use.</param>
        /// <param name="icon">The icon the <see cref="MessageBox"/> will use.</param>
        /// <returns>Returns a <see cref="DialogResult"/> based on the buttons used.</returns>
        [SuppressMessage("ReSharper", "ParameterHidesMember")]
        public DialogResult Show(string message, string caption = "Error Report",
                                    MessageBoxButtons buttons = MessageBoxButtons.OK, 
                                    MessageBoxIcon icon       = MessageBoxIcon.Error)
        {
            if (!Display) return DialogResult.OK;

            return MessageBox.Show(message, caption, buttons, icon);
        }
    }
}