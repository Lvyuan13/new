﻿#pragma checksum "..\..\..\..\page\PartialLoadSelected.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4AFEBD73EBA43E1A177A7BE32EB7CDB5550B505C90BB720E47030CE620472C2D"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace WpfApplication2 {
    
    
    /// <summary>
    /// PartialLoadSelected
    /// </summary>
    public partial class PartialLoadSelected : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\page\PartialLoadSelected.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rb25;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\page\PartialLoadSelected.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rb50;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\page\PartialLoadSelected.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rb75;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\page\PartialLoadSelected.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rb100;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\page\PartialLoadSelected.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btConfirm;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApplication2;component/page/partialloadselected.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\page\PartialLoadSelected.xaml"
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
            
            #line 4 "..\..\..\..\page\PartialLoadSelected.xaml"
            ((WpfApplication2.PartialLoadSelected)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.rb25 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 16 "..\..\..\..\page\PartialLoadSelected.xaml"
            this.rb25.Checked += new System.Windows.RoutedEventHandler(this.rb25_Checked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.rb50 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 17 "..\..\..\..\page\PartialLoadSelected.xaml"
            this.rb50.Checked += new System.Windows.RoutedEventHandler(this.rb50_Checked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.rb75 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 18 "..\..\..\..\page\PartialLoadSelected.xaml"
            this.rb75.Checked += new System.Windows.RoutedEventHandler(this.rb75_Checked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.rb100 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 19 "..\..\..\..\page\PartialLoadSelected.xaml"
            this.rb100.Checked += new System.Windows.RoutedEventHandler(this.rb100_Checked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btConfirm = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\..\page\PartialLoadSelected.xaml"
            this.btConfirm.Click += new System.Windows.RoutedEventHandler(this.btConfirm_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

