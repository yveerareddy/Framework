﻿using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace WPFPresentationCore
{
    [Export(typeof(IWaitCursor))]
    public class WaitCursor : IWaitCursor
    {
        /// <summary>
        ///   A value indicating whether the UI is currently busy
        /// </summary>
        private bool _isBusy;

        /// <summary>
        /// Sets the busystate as busy.
        /// </summary>
        void IWaitCursor.Show()
        {
            SetBusyState(true);
        }

        /// <summary>
        /// Sets the busystate to busy or not busy.
        /// </summary>
        /// <param name="busy">if set to <c>true</c> the application is now busy.</param>
        private void SetBusyState(bool busy)
        {
            if (busy != _isBusy)
            {
                _isBusy = busy;
                Mouse.OverrideCursor = busy ? Cursors.Wait : null;

                if (_isBusy)
                {
                    var obj = new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.ApplicationIdle,
                        dispatcherTimer_Tick, Application.Current.Dispatcher);
                }
            }
        }

        /// <summary>
        /// Handles the Tick event of the dispatcherTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var dispatcherTimer = sender as DispatcherTimer;
            if (dispatcherTimer != null)
            {
                SetBusyState(false);
                dispatcherTimer.Stop();
            }
        }
    }

    public interface IWaitCursor
    {
        void Show();
    }
}