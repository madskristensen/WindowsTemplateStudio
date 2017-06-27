﻿// ******************************************************************
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;

namespace Microsoft.Templates.Extension.Commands
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [ProvideAutoLoad("{f1536ef8-92ec-443c-9ed7-fdadf150da82}")]
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class RelayCommandPackage : Package
    {
        private RelayCommand addPageCommand;
        private RelayCommand addFeatureCommand;

        public RelayCommandPackage()
        {
        }

        protected override void Initialize()
        {
            InitializeAddPageCommand();
            InitializeAddFeatureCommand();

            base.Initialize();
        }

        private void InitializeAddPageCommand()
        {
            addPageCommand = new RelayCommand(this, PackageIds.AddPageCommand, PackageGuids.GuidRelayCommandPackageCmdSet,
                (sender, e) =>
                {
                    string message = string.Format(CultureInfo.CurrentCulture, "Inside PAGE {0}.MenuItemCallback()", this.GetType().FullName);
                    string title = "RelayMenuCommandCallback";

                    // Show a message box to prove we were here
                    VsShellUtilities.ShowMessageBox(
                            ServiceProvider.GlobalProvider,
                            message,
                            title,
                            OLEMSGICON.OLEMSGICON_INFO,
                            OLEMSGBUTTON.OLEMSGBUTTON_OK,
                            OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                },
                (sender, e) =>
                {
                    var cmd = (OleMenuCommand)sender;
                    cmd.Visible = true;
                });
        }

        private void InitializeAddFeatureCommand()
        {
            addFeatureCommand = new RelayCommand(this, PackageIds.AddFeatureCommand, PackageGuids.GuidRelayCommandPackageCmdSet,
                (sender, e) =>
                {
                    string message = string.Format(CultureInfo.CurrentCulture, "Inside FEATURE {0}.MenuItemCallback()", this.GetType().FullName);
                    string title = "RelayMenuCommandCallback";

                    // Show a message box to prove we were here
                    VsShellUtilities.ShowMessageBox(
                            ServiceProvider.GlobalProvider,
                            message,
                            title,
                            OLEMSGICON.OLEMSGICON_INFO,
                            OLEMSGBUTTON.OLEMSGBUTTON_OK,
                            OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                },
                (sender, e) =>
                {
                    var cmd = (OleMenuCommand)sender;
                    cmd.Visible = true;
                });
        }
    }
}
