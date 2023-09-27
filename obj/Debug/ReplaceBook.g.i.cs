﻿#pragma checksum "..\..\ReplaceBook.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "16D73B1753570DF49EEC85846F210D34B71DB009F133112BAA00D62A372583C1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DeweyDirectory;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using XamlAnimatedGif;


namespace DeweyDirectory {
    
    
    /// <summary>
    /// ReplaceBook
    /// </summary>
    public partial class ReplaceBook : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 18 "..\..\ReplaceBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\ReplaceBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox unsortedLb;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\ReplaceBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox sortedLb;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\ReplaceBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock timerText;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\ReplaceBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label playerScoreText;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\ReplaceBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock scoreText;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\ReplaceBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button startBtn;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\ReplaceBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button checkBtn;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\ReplaceBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button homeBtn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DeweyDirectory;component/replacebook.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ReplaceBook.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.image = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.unsortedLb = ((System.Windows.Controls.ListBox)(target));
            return;
            case 4:
            this.sortedLb = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.timerText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.playerScoreText = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.scoreText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.startBtn = ((System.Windows.Controls.Button)(target));
            
            #line 95 "..\..\ReplaceBook.xaml"
            this.startBtn.Click += new System.Windows.RoutedEventHandler(this.startBtn_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.checkBtn = ((System.Windows.Controls.Button)(target));
            
            #line 99 "..\..\ReplaceBook.xaml"
            this.checkBtn.Click += new System.Windows.RoutedEventHandler(this.checkBtn_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.homeBtn = ((System.Windows.Controls.Button)(target));
            
            #line 102 "..\..\ReplaceBook.xaml"
            this.homeBtn.Click += new System.Windows.RoutedEventHandler(this.homeBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 3:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.PreviewMouseMoveEvent;
            
            #line 40 "..\..\ReplaceBook.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseEventHandler(this.ListBoxItem_PreviewMouseMove);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.DropEvent;
            
            #line 43 "..\..\ReplaceBook.xaml"
            eventSetter.Handler = new System.Windows.DragEventHandler(this.ListBoxItem_Drop);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}

