﻿

#pragma checksum "D:\Users\资料\game\guess\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E93E718C9A8F2BB27A55334A3ED64E91"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace guess
{
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 10 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).PointerPressed += this.g_PointerPressed;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 36 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.button1_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 32 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Button_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


